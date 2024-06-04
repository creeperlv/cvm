#!/bin/sh

if [ -z "$CC" ];
then
	CC="cc"
fi

if [ -z "$BIN" ];
then
	BIN="./bin"
fi

if [ -z "$SRC" ];
then
	SRC="."
fi

if [ -z "$C_OPT" ];
then
	C_OPT="-O3 -s -fno-asynchronous-unwind-tables -fno-ident "
fi

C_OPT="$C_OPT $ADDITIONAL_C_OPT"

INDEX=0
mkdir -p $BIN
if [ -z "$SKIP_MINIVM" ]
then
	COMPILE="$CC $C_OPT $SRC/core/*.c $SRC/minivm/*.c -o $BIN/minivm"
	echo "$COMPILE"
	$COMPILE
fi

if [ -z "$SKIP_FULLVM" ]
then

	if [ -z "$LIBS" ]
	then
		LIBS="-lraylib"
	fi
	COMPILE="$CC $C_OPT $SRC/core/*.c $SRC/fullvm/*.c -o $BIN/fullvm $LIBS"
	echo "$COMPILE"
	$COMPILE
fi
