#!/usr/bin/env tclsh

proc usage {} {
    puts "Usage: tclsh tools/volatco_upload.tcl source ?options?"
    puts ""
    puts "Options:"
    puts "  --port PORT              Serial port, default COM3"
    puts "  --baud BAUD              Baud rate, default 115200"
    puts "  --line-delay SECONDS     Delay after each line, default 0.05"
    puts "  --char-delay SECONDS     Delay between characters, default 0"
    puts "  --startup-delay SECONDS  Delay after opening port, default 0.5"
    puts "  --read-timeout SECONDS   Poll timeout, default 0.2"
    puts "  --prompt TEXT            Prompt substring, default ok"
    puts "  --wait-for-prompt        Wait for prompt after each line"
    puts "  --strip-comments         Skip blank lines and backslash comments"
    puts "  --append LINE            Extra line to send; may repeat"
    puts "  --dry-run                Print lines without opening serial port"
    puts "  --help                   Show this help"
}

proc normalize_port {port} {
    if {[regexp -nocase {^com([0-9]+)$} $port -> n]} {
        if {$n >= 10} {
            return "\\\\.\\$port"
        }
    }
    return $port
}

proc slurp_lines {path strip_comments} {
    set f [open $path r]
    fconfigure $f -encoding utf-8 -translation lf
    set data [read $f]
    close $f

    set out {}
    foreach raw [split $data "\n"] {
        set line [string trimright $raw "\r"]
        if {$strip_comments} {
            set trimmed [string trimleft $line]
            if {$trimmed eq ""} {
                continue
            }
            if {[string index $trimmed 0] eq "\\"} {
                continue
            }
        }
        lappend out $line
    }
    return $out
}

proc sleep_seconds {seconds} {
    after [expr {int($seconds * 1000)}]
}

proc read_available {chan timeout} {
    set deadline [expr {[clock milliseconds] + int($timeout * 1000)}]
    set out ""
    while {[clock milliseconds] <= $deadline} {
        set chunk [read $chan]
        if {$chunk ne ""} {
            append out $chunk
            continue
        }
        sleep_seconds 0.02
    }
    return $out
}

proc wait_for_prompt {chan prompt timeout} {
    set deadline [expr {[clock milliseconds] + int($timeout * 1000)}]
    set out ""
    while {[clock milliseconds] <= $deadline} {
        set chunk [read $chan]
        if {$chunk ne ""} {
            append out $chunk
            if {[string first $prompt $out] >= 0} {
                return $out
            }
        } else {
            sleep_seconds 0.02
        }
    }
    error "prompt '$prompt' not seen within $timeout seconds"
}

proc send_line {chan line char_delay line_delay} {
    set payload "${line}\r"
    if {$char_delay > 0} {
        foreach ch [split $payload ""] {
            puts -nonewline $chan $ch
            flush $chan
            sleep_seconds $char_delay
        }
    } else {
        puts -nonewline $chan $payload
        flush $chan
    }
    sleep_seconds $line_delay
}

set port "COM3"
set baud 115200
set line_delay 0.05
set char_delay 0.0
set startup_delay 0.5
set read_timeout 0.2
set prompt "ok"
set wait_for_prompt_flag 0
set strip_comments 0
set dry_run 0
set append_lines {}
set source ""

set argv_copy $argv
while {[llength $argv_copy] > 0} {
    set arg [lindex $argv_copy 0]
    set argv_copy [lrange $argv_copy 1 end]
    switch -- $arg {
        --help - -h {
            usage
            exit 0
        }
        --port {
            set port [lindex $argv_copy 0]
            set argv_copy [lrange $argv_copy 1 end]
        }
        --baud {
            set baud [lindex $argv_copy 0]
            set argv_copy [lrange $argv_copy 1 end]
        }
        --line-delay {
            set line_delay [lindex $argv_copy 0]
            set argv_copy [lrange $argv_copy 1 end]
        }
        --char-delay {
            set char_delay [lindex $argv_copy 0]
            set argv_copy [lrange $argv_copy 1 end]
        }
        --startup-delay {
            set startup_delay [lindex $argv_copy 0]
            set argv_copy [lrange $argv_copy 1 end]
        }
        --read-timeout {
            set read_timeout [lindex $argv_copy 0]
            set argv_copy [lrange $argv_copy 1 end]
        }
        --prompt {
            set prompt [lindex $argv_copy 0]
            set argv_copy [lrange $argv_copy 1 end]
        }
        --wait-for-prompt {
            set wait_for_prompt_flag 1
        }
        --strip-comments {
            set strip_comments 1
        }
        --append {
            lappend append_lines [lindex $argv_copy 0]
            set argv_copy [lrange $argv_copy 1 end]
        }
        --dry-run {
            set dry_run 1
        }
        default {
            if {[string match "-*" $arg]} {
                puts stderr "Unknown option: $arg"
                usage
                exit 2
            }
            if {$source ne ""} {
                puts stderr "Only one source file may be specified."
                exit 2
            }
            set source $arg
        }
    }
}

if {$source eq ""} {
    usage
    exit 2
}

set lines [slurp_lines $source $strip_comments]
foreach extra $append_lines {
    lappend lines $extra
}

if {$dry_run} {
    foreach line $lines {
        puts $line
    }
    exit 0
}

set normalized_port [normalize_port $port]
set chan [open $normalized_port r+]
fconfigure $chan \
    -mode "${baud},n,8,1" \
    -blocking 0 \
    -buffering none \
    -translation binary \
    -encoding utf-8 \
    -handshake none

sleep_seconds $startup_delay
set banner [read_available $chan $read_timeout]
if {$banner ne ""} {
    puts -nonewline $banner
}

set total [llength $lines]
set index 0
foreach line $lines {
    incr index
    puts "\[$index/$total] $line"
    send_line $chan $line $char_delay $line_delay
    set response [read_available $chan $read_timeout]
    if {$response ne ""} {
        puts -nonewline $response
    }
    if {$wait_for_prompt_flag} {
        set prompt_response [wait_for_prompt $chan $prompt [expr {max(1.0, $read_timeout)}]]
        if {$prompt_response ne ""} {
            puts -nonewline $prompt_response
        }
    }
}

close $chan
