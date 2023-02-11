dotnet tool install --global ClangSharpPInvokeGenerator --version 15.0.2

ClangSharpPInvokeGenerator --file-directory vcpkg_installed/x64-windows/include "@scripts/GenerateZstd.rsp"
ClangSharpPInvokeGenerator --file-directory vcpkg_installed/x64-windows/include "@scripts/GenerateZdict.rsp"