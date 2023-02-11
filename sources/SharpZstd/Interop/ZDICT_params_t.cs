namespace SharpZstd.Interop
{
    public partial struct ZDICT_params_t
    {
        public int compressionLevel;

        [NativeTypeName("unsigned int")]
        public uint notificationLevel;

        [NativeTypeName("unsigned int")]
        public uint dictID;
    }
}
