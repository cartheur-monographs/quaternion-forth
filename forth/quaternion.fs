\ Quaternion arithmetic scaffold for polyForth-style systems.
\ Representation: a quaternion occupies four consecutive cells:
\   q[0] = scalar part a
\   q[1] = i component b
\   q[2] = j component c
\   q[3] = k component d
\
\ Operations use addresses rather than leaving 4-tuples on the data stack.
\ That is easier to port to small Forth systems and easier to benchmark.

decimal

variable q-left
variable q-right
variable q-out
variable q-tmp1
variable q-tmp2

: q.a ( q -- addr ) ;
: q.b ( q -- addr ) 1 cells + ;
: q.c ( q -- addr ) 2 cells + ;
: q.d ( q -- addr ) 3 cells + ;

: q! ( a b c d q -- )
  dup >r
  q.d !
  r@ q.c !
  r@ q.b !
  r> q.a ! ;

: q@ ( q -- a b c d )
  dup >r
  q.a @
  r@ q.b @
  r@ q.c @
  r> q.d @ ;

: qzero ( q -- )
  dup q.a 0 swap !
  dup q.b 0 swap !
  dup q.c 0 swap !
  q.d 0 swap ! ;

: qcopy ( src dst -- )
  q-out ! q-left !
  q-left @ q.a @ q-out @ q.a !
  q-left @ q.b @ q-out @ q.b !
  q-left @ q.c @ q-out @ q.c !
  q-left @ q.d @ q-out @ q.d ! ;

: qconj ( src dst -- )
  q-out ! q-left !
  q-left @ q.a @ q-out @ q.a !
  q-left @ q.b @ negate q-out @ q.b !
  q-left @ q.c @ negate q-out @ q.c !
  q-left @ q.d @ negate q-out @ q.d ! ;

: q+ ( q1 q2 q3 -- )
  q-out ! q-right ! q-left !
  q-left @ q.a @ q-right @ q.a @ + q-out @ q.a !
  q-left @ q.b @ q-right @ q.b @ + q-out @ q.b !
  q-left @ q.c @ q-right @ q.c @ + q-out @ q.c !
  q-left @ q.d @ q-right @ q.d @ + q-out @ q.d ! ;

: q- ( q1 q2 q3 -- )
  q-out ! q-right ! q-left !
  q-left @ q.a @ q-right @ q.a @ - q-out @ q.a !
  q-left @ q.b @ q-right @ q.b @ - q-out @ q.b !
  q-left @ q.c @ q-right @ q.c @ - q-out @ q.c !
  q-left @ q.d @ q-right @ q.d @ - q-out @ q.d ! ;

: qscale ( q1 n q2 -- )
  q-out ! q-right ! q-left !
  q-left @ q.a @ q-right @ * q-out @ q.a !
  q-left @ q.b @ q-right @ * q-out @ q.b !
  q-left @ q.c @ q-right @ * q-out @ q.c !
  q-left @ q.d @ q-right @ * q-out @ q.d ! ;

: qnorm2 ( q -- n )
  dup q.a @ dup *
  over q.b @ dup * +
  over q.c @ dup * +
  swap q.d @ dup * + ;

\ Hamilton product:
\ (a + bi + cj + dk)(e + fi + gj + hk)
\ = (ae - bf - cg - dh)
\ + (af + be + ch - dg)i
\ + (ag - bh + ce + df)j
\ + (ah + bg - cf + de)k
: q* ( q1 q2 q3 -- )
  q-out ! q-right ! q-left !
  q-left @ q.a @ q-right @ q.a @ *
  q-left @ q.b @ q-right @ q.b @ * -
  q-left @ q.c @ q-right @ q.c @ * -
  q-left @ q.d @ q-right @ q.d @ * -
  q-out @ q.a !
  q-left @ q.a @ q-right @ q.b @ *
  q-left @ q.b @ q-right @ q.a @ * +
  q-left @ q.c @ q-right @ q.d @ * +
  q-left @ q.d @ q-right @ q.c @ * -
  q-out @ q.b !
  q-left @ q.a @ q-right @ q.c @ *
  q-left @ q.b @ q-right @ q.d @ * -
  q-left @ q.c @ q-right @ q.a @ * +
  q-left @ q.d @ q-right @ q.b @ * +
  q-out @ q.c !
  q-left @ q.a @ q-right @ q.d @ *
  q-left @ q.b @ q-right @ q.c @ * +
  q-left @ q.c @ q-right @ q.b @ * -
  q-left @ q.d @ q-right @ q.a @ * +
  q-out @ q.d ! ;

\ Build a pure-vector quaternion with zero scalar part.
: v>q ( x y z q -- )
  >r
  r@ q.d !
  r@ q.c !
  r@ q.b !
  r> q.a 0 swap ! ;

\ Extract vector part of a quaternion.
: q>v ( q -- x y z )
  dup >r
  q.b @
  r@ q.c @
  r> q.d @ ;

\ Rotate vector v by unit quaternion q using q * v * conjugate(q).
\ Inputs:
\   qrot   address of rotation quaternion
\   qvec   address of vector-as-quaternion
\   qtmp1  scratch quaternion
\   qtmp2  scratch quaternion
\   qout   result quaternion
: qrotate ( qrot qvec qtmp1 qtmp2 qout -- )
  q-out ! q-tmp2 ! q-tmp1 ! q-right ! q-left !
  q-left @ q-right @ q-tmp1 @ q*
  q-left @ q-tmp2 @ qconj
  q-tmp1 @ q-tmp2 @ q-out @ q* ;
