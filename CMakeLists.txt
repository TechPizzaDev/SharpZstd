# CMakeLists.txt

cmake_minimum_required(VERSION 3.0)

add_compile_definitions(ZSTD_STATIC_LINKING_ONLY)

project(Dummy)

set(CMAKE_FIND_DEBUG_MODE TRUE)

find_package(zstd CONFIG REQUIRED)
