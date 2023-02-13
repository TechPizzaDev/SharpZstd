using System.Runtime.InteropServices;

namespace SharpZstd.Interop
{
    public static unsafe partial class Zdict
    {
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZDICT_trainFromBuffer(void* dictBuffer, [NativeTypeName("size_t")] nuint dictBufferCapacity, [NativeTypeName("const void *")] void* samplesBuffer, [NativeTypeName("const size_t *")] nuint* samplesSizes, [NativeTypeName("unsigned int")] uint nbSamples);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZDICT_finalizeDictionary(void* dstDictBuffer, [NativeTypeName("size_t")] nuint maxDictSize, [NativeTypeName("const void *")] void* dictContent, [NativeTypeName("size_t")] nuint dictContentSize, [NativeTypeName("const void *")] void* samplesBuffer, [NativeTypeName("const size_t *")] nuint* samplesSizes, [NativeTypeName("unsigned int")] uint nbSamples, ZDICT_params_t parameters);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned int")]
        public static extern uint ZDICT_getDictID([NativeTypeName("const void *")] void* dictBuffer, [NativeTypeName("size_t")] nuint dictSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZDICT_getDictHeaderSize([NativeTypeName("const void *")] void* dictBuffer, [NativeTypeName("size_t")] nuint dictSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned int")]
        public static extern uint ZDICT_isError([NativeTypeName("size_t")] nuint errorCode);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ZDICT_getErrorName([NativeTypeName("size_t")] nuint errorCode);
    }
}
