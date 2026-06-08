\ Benchmark scaffold for quaternion work on Volatco/polyForth.
\ These definitions provide deterministic workloads and fixed storage.
\ Target-specific timing words should wrap these kernels.

include quaternion.fs
include matrix.fs

create q0 4 cells allot
create q1 4 cells allot
create q2 4 cells allot
create q3 4 cells allot
create q4 4 cells allot
create q5 4 cells allot
create q6 4 cells allot

create m0 9 cells allot
create m1 9 cells allot
create m2 9 cells allot

create v0 3 cells allot
create v1 3 cells allot

variable bench-iterations
1000 bench-iterations !

: bench-set-iterations ( n -- )
  bench-iterations ! ;

: bench-init ( -- )
  1 2 3 4 q0 q!
  5 6 7 8 q1 q!
  \ Unit quaternion for 180-degree rotation about the x-axis.
  \ This keeps the benchmark integer-only while giving a real rotation.
  0 1 0 0 q2 q!
  \ Pure-vector quaternion matching v0.
  0 10 20 30 q3 q!
  q4 qzero
  q5 qzero
  q6 qzero
  1 0 0 0 1 0 0 0 1 m0 m!
  \ Matrix equivalent of q2:
  \ [ 1  0  0 ]
  \ [ 0 -1  0 ]
  \ [ 0  0 -1 ]
  1 0 0 0 -1 0 0 0 -1 m1 m!
  m2 mzero
  10 20 30 v0 v!
  v1 vzero ;

: bench-q+ ( -- )
  bench-init
  bench-iterations @ 0 do
    q0 q1 q3 q+
  loop ;

: bench-q* ( -- )
  bench-init
  bench-iterations @ 0 do
    q0 q1 q3 q*
  loop ;

: bench-qnorm2 ( -- n )
  bench-init
  0
  bench-iterations @ 0 do
    q0 qnorm2 +
  loop ;

\ Representative rotation workload for control-oriented use.
: bench-qrotate ( -- )
  bench-init
  bench-iterations @ 0 do
    q2 q3 q4 q5 q6 qrotate
  loop ;

: bench-mrotate ( -- )
  bench-init
  bench-iterations @ 0 do
    m1 v0 v1 m3*v
  loop ;

: bench-mmultiply ( -- )
  bench-init
  bench-iterations @ 0 do
    m0 m1 m2 m3*
  loop ;

\ Expected rotated vector for the aligned rotation fixtures.
: bench-expected-rot ( -- x y z )
  10 -20 -30 ;

: bench-check-rotation ( -- )
  bench-init
  q2 q3 q4 q5 q6 qrotate
  m1 v0 v1 m3*v ;

: bench-qrotate-vector ( -- x y z )
  bench-init
  q2 q3 q4 q5 q6 qrotate
  q6 q>v ;

: bench-mrotate-vector ( -- x y z )
  bench-init
  m1 v0 v1 m3*v
  v1 v.x @ v1 v.y @ v1 v.z @ ;

: bench-qrotate-full ( -- a b c d )
  bench-init
  q2 q3 q4 q5 q6 qrotate
  q6 q@ ;

: bench-qrotate-report ( -- )
  bench-init
  q2 q3 q4 q5 q6 qrotate
  q6 q.b @ .
  q6 q.c @ .
  q6 q.d @ . ;

: bench-mrotate-report ( -- )
  bench-init
  m1 v0 v1 m3*v
  v1 v.x @ .
  v1 v.y @ .
  v1 v.z @ . ;
