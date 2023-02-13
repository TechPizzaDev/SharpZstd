namespace SharpZstd.Interop
{
    public partial struct ZSTD_Sequence
    {
        [NativeTypeName("unsigned int")]
        public uint offset;

        [NativeTypeName("unsigned int")]
        public uint litLength;

        [NativeTypeName("unsigned int")]
        public uint matchLength;

        [NativeTypeName("unsigned int")]
        public uint rep;
    }
}
