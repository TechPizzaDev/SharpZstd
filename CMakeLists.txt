# CMakeLists.txt

cmake_minimum_required(VERSION 3.0)

add_compile_definitions(ZSTD_STATIC_LINKING_ONLY)

project(Dummy)

find_package(zstd CONFIG REQUIRED)
