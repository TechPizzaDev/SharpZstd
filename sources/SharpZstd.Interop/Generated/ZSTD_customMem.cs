namespace SharpZstd.Interop
{
    public unsafe partial struct ZSTD_customMem
    {
        [NativeTypeName("ZSTD_allocFunction")]
        public delegate* unmanaged[Cdecl]<void*, nuint, void*> customAlloc;

        [NativeTypeName("ZSTD_freeFunction")]
        public delegate* unmanaged[Cdecl]<void*, void*, void> customFree;

        public void* opaque;
    }
}
