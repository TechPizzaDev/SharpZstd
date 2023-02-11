namespace SharpZstd.Interop
{
    public partial struct ZSTD_bounds
    {
        [NativeTypeName("size_t")]
        public nuint error;

        public int lowerBound;

        public int upperBound;
    }
}
