cmake_minimum_required(VERSION 3.15)
project(fullvm-pc)

# Copied from https://github.com/raysan5/raylib/blob/master/projects/CMake/CMakeLists.txt

set(RAYLIB_VERSION 5.0)
find_package(raylib ${RAYLIB_VERSION} QUIET) # QUIET or REQUIRED
if (NOT raylib_FOUND) # If there's none, fetch and build raylib
  include(FetchContent)
  FetchContent_Declare(
    raylib
    DOWNLOAD_EXTRACT_TIMESTAMP OFF
    URL https://github.com/raysan5/raylib/archive/refs/tags/${RAYLIB_VERSION}.tar.gz
  )
  FetchContent_GetProperties(raylib)
  if (NOT raylib_POPULATED) # Have we downloaded raylib yet?
    set(FETCHCONTENT_QUIET NO)
    FetchContent_MakeAvailable(raylib)
    set(BUILD_EXAMPLES OFF CACHE BOOL "" FORCE) # don't build the supplied examples
  endif()
endif()

add_executable(${PROJECT_NAME} ../../main.c ../../win32/minimal_posix.c ../../video/raylib/videoMode.c
 ../../../generic/base.c ../../../generic/RLEImage.c ../../../../core/base.c)

target_link_libraries(${PROJECT_NAME} raylib)
