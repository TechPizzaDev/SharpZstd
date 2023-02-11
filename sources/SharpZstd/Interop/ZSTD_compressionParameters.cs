namespace SharpZstd.Interop
{
    public partial struct ZSTD_compressionParameters
    {
        [NativeTypeName("unsigned int")]
        public uint windowLog;

        [NativeTypeName("unsigned int")]
        public uint chainLog;

        [NativeTypeName("unsigned int")]
        public uint hashLog;

        [NativeTypeName("unsigned int")]
        public uint searchLog;

        [NativeTypeName("unsigned int")]
        public uint minMatch;

        [NativeTypeName("unsigned int")]
        public uint targetLength;

        public ZSTD_strategy strategy;
    }
}
