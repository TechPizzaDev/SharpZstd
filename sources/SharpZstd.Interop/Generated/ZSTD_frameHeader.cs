namespace SharpZstd.Interop
{
    public partial struct ZSTD_frameHeader
    {
        [NativeTypeName("unsigned long long")]
        public ulong frameContentSize;

        [NativeTypeName("unsigned long long")]
        public ulong windowSize;

        [NativeTypeName("unsigned int")]
        public uint blockSizeMax;

        public ZSTD_frameType_e frameType;

        [NativeTypeName("unsigned int")]
        public uint headerSize;

        [NativeTypeName("unsigned int")]
        public uint dictID;

        [NativeTypeName("unsigned int")]
        public uint checksumFlag;

        [NativeTypeName("unsigned int")]
        public uint _reserved1;

        [NativeTypeName("unsigned int")]
        public uint _reserved2;
    }
}
