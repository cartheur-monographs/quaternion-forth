#!/usr/bin/env python3
"""Upload polyForth source to Volatco over a serial TTY.

This uploader intentionally avoids external dependencies such as pyserial.
It uses POSIX termios so it should work on Linux/macOS hosts with a serial
device such as /dev/ttyUSB0 or /dev/ttyACM0.
"""

from __future__ import annotations

import argparse
import os
import select
import sys
import termios
import time
import tty
from pathlib import Path


BAUD_MAP = {
    1200: termios.B1200,
    2400: termios.B2400,
    4800: termios.B4800,
    9600: termios.B9600,
    19200: termios.B19200,
    38400: termios.B38400,
    57600: termios.B57600,
    115200: termios.B115200,
    230400: termios.B230400,
}


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(
        description="Upload a .pf source file to Volatco over serial."
    )
    parser.add_argument(
        "source",
        type=Path,
        help="Path to the .pf source file to upload.",
    )
    parser.add_argument(
        "--port",
        default="/dev/ttyUSB0",
        help="Serial device path, for example /dev/ttyUSB0.",
    )
    parser.add_argument(
        "--baud",
        type=int,
        default=115200,
        choices=sorted(BAUD_MAP),
        help="Serial baud rate.",
    )
    parser.add_argument(
        "--line-delay",
        type=float,
        default=0.05,
        help="Delay in seconds after each transmitted line.",
    )
    parser.add_argument(
        "--char-delay",
        type=float,
        default=0.0,
        help="Delay in seconds between transmitted characters.",
    )
    parser.add_argument(
        "--startup-delay",
        type=float,
        default=0.5,
        help="Delay after opening the port before transmitting data.",
    )
    parser.add_argument(
        "--read-timeout",
        type=float,
        default=0.2,
        help="Seconds to wait when polling for device output.",
    )
    parser.add_argument(
        "--prompt",
        default="ok",
        help="Optional prompt substring to wait for after each line.",
    )
    parser.add_argument(
        "--wait-for-prompt",
        action="store_true",
        help="Wait for the prompt substring after each line.",
    )
    parser.add_argument(
        "--strip-comments",
        action="store_true",
        help="Skip blank lines and lines beginning with backslash comments.",
    )
    parser.add_argument(
        "--append",
        action="append",
        default=[],
        help="Extra line to send after the source file. May be repeated.",
    )
    parser.add_argument(
        "--dry-run",
        action="store_true",
        help="Print the lines that would be sent without opening the port.",
    )
    return parser.parse_args()


def load_lines(path: Path, strip_comments: bool) -> list[str]:
    text = path.read_text(encoding="utf-8")
    lines: list[str] = []
    for raw in text.splitlines():
        line = raw.rstrip()
        if strip_comments and (not line or line.lstrip().startswith("\\")):
            continue
        lines.append(line)
    return lines


def configure_port(fd: int, baud: int) -> None:
    attrs = termios.tcgetattr(fd)
    attrs[0] = 0
    attrs[1] = 0
    attrs[2] = termios.CREAD | termios.CLOCAL | termios.CS8
    attrs[3] = 0
    attrs[4] = BAUD_MAP[baud]
    attrs[5] = BAUD_MAP[baud]
    attrs[6][termios.VMIN] = 0
    attrs[6][termios.VTIME] = 0
    termios.tcsetattr(fd, termios.TCSANOW, attrs)
    tty.setraw(fd)
    termios.tcflush(fd, termios.TCIOFLUSH)


def read_available(fd: int, timeout: float) -> str:
    chunks: list[bytes] = []
    while True:
        ready, _, _ = select.select([fd], [], [], timeout)
        if not ready:
            break
        data = os.read(fd, 4096)
        if not data:
            break
        chunks.append(data)
        timeout = 0
    return b"".join(chunks).decode("utf-8", errors="replace")


def send_line(fd: int, line: str, char_delay: float, line_delay: float) -> None:
    payload = (line + "\r").encode("utf-8")
    if char_delay > 0:
        for byte in payload:
            os.write(fd, bytes([byte]))
            time.sleep(char_delay)
    else:
        os.write(fd, payload)
    time.sleep(line_delay)


def wait_for_prompt(fd: int, prompt: str, timeout: float) -> str:
    deadline = time.monotonic() + timeout
    transcript = ""
    while time.monotonic() < deadline:
        chunk = read_available(fd, 0.1)
        if chunk:
            transcript += chunk
            if prompt in transcript:
                return transcript
    raise TimeoutError(f"prompt {prompt!r} not seen within {timeout:.2f}s")


def main() -> int:
    args = parse_args()
    lines = load_lines(args.source, args.strip_comments)
    lines.extend(args.append)

    if args.dry_run:
        for line in lines:
            print(line)
        return 0

    fd = os.open(args.port, os.O_RDWR | os.O_NOCTTY | os.O_NONBLOCK)
    try:
        configure_port(fd, args.baud)
        time.sleep(args.startup_delay)
        banner = read_available(fd, args.read_timeout)
        if banner:
            sys.stdout.write(banner)
            sys.stdout.flush()

        total = len(lines)
        for index, line in enumerate(lines, start=1):
            print(f"[{index}/{total}] {line}")
            send_line(fd, line, args.char_delay, args.line_delay)
            response = read_available(fd, args.read_timeout)
            if response:
                sys.stdout.write(response)
                sys.stdout.flush()
            if args.wait_for_prompt:
                response = wait_for_prompt(fd, args.prompt, max(1.0, args.read_timeout))
                if response:
                    sys.stdout.write(response)
                    sys.stdout.flush()
    finally:
        os.close(fd)
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
