\ Matrix rotation scaffold for polyForth-style systems.
\ Representation: a 3x3 matrix occupies nine consecutive cells in row-major order:
\   m[0] m[1] m[2]
\   m[3] m[4] m[5]
\   m[6] m[7] m[8]
\
\ Vectors occupy three consecutive cells:
\   v[0] = x
\   v[1] = y
\   v[2] = z

decimal

variable m-left
variable m-right
variable m-out
variable v-in
variable v-out

: m.00 ( m -- addr ) ;
: m.01 ( m -- addr ) 1 cells + ;
: m.02 ( m -- addr ) 2 cells + ;
: m.10 ( m -- addr ) 3 cells + ;
: m.11 ( m -- addr ) 4 cells + ;
: m.12 ( m -- addr ) 5 cells + ;
: m.20 ( m -- addr ) 6 cells + ;
: m.21 ( m -- addr ) 7 cells + ;
: m.22 ( m -- addr ) 8 cells + ;

: v.x ( v -- addr ) ;
: v.y ( v -- addr ) 1 cells + ;
: v.z ( v -- addr ) 2 cells + ;

: m! ( m00 m01 m02 m10 m11 m12 m20 m21 m22 m -- )
  dup >r
  m.22 !
  r@ m.21 !
  r@ m.20 !
  r@ m.12 !
  r@ m.11 !
  r@ m.10 !
  r@ m.02 !
  r@ m.01 !
  r> m.00 ! ;

: mzero ( m -- )
  dup m.00 0 swap !
  dup m.01 0 swap !
  dup m.02 0 swap !
  dup m.10 0 swap !
  dup m.11 0 swap !
  dup m.12 0 swap !
  dup m.20 0 swap !
  dup m.21 0 swap !
  m.22 0 swap ! ;

: v! ( x y z v -- )
  dup >r
  v.z !
  r@ v.y !
  r> v.x ! ;

: vzero ( v -- )
  dup v.x 0 swap !
  dup v.y 0 swap !
  v.z 0 swap ! ;

: m3*v ( m v-src v-dst -- )
  v-out ! v-in ! m-left !
  m-left @ m.00 @ v-in @ v.x @ *
  m-left @ m.01 @ v-in @ v.y @ * +
  m-left @ m.02 @ v-in @ v.z @ * +
  v-out @ v.x !
  m-left @ m.10 @ v-in @ v.x @ *
  m-left @ m.11 @ v-in @ v.y @ * +
  m-left @ m.12 @ v-in @ v.z @ * +
  v-out @ v.y !
  m-left @ m.20 @ v-in @ v.x @ *
  m-left @ m.21 @ v-in @ v.y @ * +
  m-left @ m.22 @ v-in @ v.z @ * +
  v-out @ v.z ! ;

: m3* ( m1 m2 mout -- )
  m-out ! m-right ! m-left !
  m-left @ m.00 @ m-right @ m.00 @ *
  m-left @ m.01 @ m-right @ m.10 @ * +
  m-left @ m.02 @ m-right @ m.20 @ * +
  m-out @ m.00 !
  m-left @ m.00 @ m-right @ m.01 @ *
  m-left @ m.01 @ m-right @ m.11 @ * +
  m-left @ m.02 @ m-right @ m.21 @ * +
  m-out @ m.01 !
  m-left @ m.00 @ m-right @ m.02 @ *
  m-left @ m.01 @ m-right @ m.12 @ * +
  m-left @ m.02 @ m-right @ m.22 @ * +
  m-out @ m.02 !
  m-left @ m.10 @ m-right @ m.00 @ *
  m-left @ m.11 @ m-right @ m.10 @ * +
  m-left @ m.12 @ m-right @ m.20 @ * +
  m-out @ m.10 !
  m-left @ m.10 @ m-right @ m.01 @ *
  m-left @ m.11 @ m-right @ m.11 @ * +
  m-left @ m.12 @ m-right @ m.21 @ * +
  m-out @ m.11 !
  m-left @ m.10 @ m-right @ m.02 @ *
  m-left @ m.11 @ m-right @ m.12 @ * +
  m-left @ m.12 @ m-right @ m.22 @ * +
  m-out @ m.12 !
  m-left @ m.20 @ m-right @ m.00 @ *
  m-left @ m.21 @ m-right @ m.10 @ * +
  m-left @ m.22 @ m-right @ m.20 @ * +
  m-out @ m.20 !
  m-left @ m.20 @ m-right @ m.01 @ *
  m-left @ m.21 @ m-right @ m.11 @ * +
  m-left @ m.22 @ m-right @ m.21 @ * +
  m-out @ m.21 !
  m-left @ m.20 @ m-right @ m.02 @ *
  m-left @ m.21 @ m-right @ m.12 @ * +
  m-left @ m.22 @ m-right @ m.22 @ * +
  m-out @ m.22 ! ;
