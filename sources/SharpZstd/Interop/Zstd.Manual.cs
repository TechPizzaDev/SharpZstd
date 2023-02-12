namespace SharpZstd.Interop
{
    public static unsafe partial class Zstd
    {
        public const string DllName = ZstdImportResolver.DllName;

        public static nuint ZSTD_COMPRESSBOUND(nuint srcSize)
        {
            return ((srcSize) + ((srcSize) >> 8) + (((srcSize) < (128 << 10)) ? (((128 << 10) - (srcSize)) >> 11) /* margin, from 64 to 0 */ : 0)); /* this formula ensures that bound(A) + bound(B) <= bound(A+B) as long as A and B >= 128 KB */
        }

        public static nuint ZSTD_FRAMEHEADERSIZE_PREFIX(ZSTD_format_e format)
        {
            return ((format) == ZSTD_format_e.ZSTD_f_zstd1 ? 5u : 1); /* minimum input size required to query frame header size */
        }

        public static nuint ZSTD_FRAMEHEADERSIZE_MIN(ZSTD_format_e format)
        {
            return ((format) == ZSTD_format_e.ZSTD_f_zstd1 ? 6u : 2);
        }
    }
}