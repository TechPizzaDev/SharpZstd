namespace SharpZstd.Interop
{
    public partial struct ZSTD_frameProgression
    {
        [NativeTypeName("unsigned long long")]
        public ulong ingested;

        [NativeTypeName("unsigned long long")]
        public ulong consumed;

        [NativeTypeName("unsigned long long")]
        public ulong produced;

        [NativeTypeName("unsigned long long")]
        public ulong flushed;

        [NativeTypeName("unsigned int")]
        public uint currentJobID;

        [NativeTypeName("unsigned int")]
        public uint nbActiveWorkers;
    }
}
