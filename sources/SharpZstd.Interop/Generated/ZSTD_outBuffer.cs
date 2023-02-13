namespace SharpZstd.Interop
{
    public unsafe partial struct ZSTD_outBuffer
    {
        public void* dst;

        [NativeTypeName("size_t")]
        public nuint size;

        [NativeTypeName("size_t")]
        public nuint pos;
    }
}
