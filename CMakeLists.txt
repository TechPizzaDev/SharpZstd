# CMakeLists.txt

cmake_minimum_required(VERSION 3.0)

add_compile_definitions(ZSTD_STATIC_LINKING_ONLY)
add_compile_definitions(ZSTD_LEGACY_SUPPORT=0)

project(Dummy)

find_package(zstd CONFIG REQUIRED)
