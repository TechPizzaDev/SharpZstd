namespace SharpZstd.Interop
{
    public static unsafe partial class Zdict
    {
        public const string DllName = ZstdImportResolver.DllName;

        static Zdict()
        {
            ZstdImportResolver.Initialize();
        }
    }
}