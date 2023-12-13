dotnet tool update --global ClangSharpPInvokeGenerator --version 17.0.0

ClangSharpPInvokeGenerator --file-directory vcpkg_installed/x64-windows/include "@scripts/GenerateZstd.rsp"
ClangSharpPInvokeGenerator --file-directory vcpkg_installed/x64-windows/include "@scripts/GenerateZdict.rsp"