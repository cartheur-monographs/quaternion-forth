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
  0 10 20 30 q2 q!
  q3 qzero
  q4 qzero
  q5 qzero
  1 0 0 0 1 0 0 0 1 m0 m!
  2 1 0 0 2 1 1 0 2 m1 m!
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
    q0 q2 q3 q4 q5 qrotate
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
