if [ -z "$CC" ]
then
	CC=gcc
fi
if [ -z "$BIN"]
then
    BIN="./bin"
fi
if [ -z "$SRC" ]
then
    SRC="."
fi
if [ -z "$PREFIX" ]
then
    PREFIX="."
fi
SOURCE="$SRC/*.c $SRC/../generic/*.c $SRC/video/sdl1/*.c $SRC/storage/*.c $SRC/../../core/*.c"
ABIN="$PREFIX/$BIN"
mkdir -p ABIN
COMPILE="$CC -O3 $C_OPT $SOURCE -o $ABIN/fullvm-pc-sdl -lSDL_image  -ggdb -lSDL -lSDL_ttf -lm"
echo "$COMPILE"
$COMPILE
