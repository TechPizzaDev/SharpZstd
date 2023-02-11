namespace SharpZstd.Interop
{
    public unsafe partial struct ZSTD_inBuffer
    {
        [NativeTypeName("const void *")]
        public void* src;

        [NativeTypeName("size_t")]
        public nuint size;

        [NativeTypeName("size_t")]
        public nuint pos;
    }
}
