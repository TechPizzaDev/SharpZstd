using System;
using System.Runtime.InteropServices;
using static SharpZstd.Interop.ZSTD_cParameter;
using static SharpZstd.Interop.ZSTD_dParameter;
using static SharpZstd.Interop.ZSTD_strategy;

namespace SharpZstd.Interop
{
    public static unsafe partial class Zstd
    {
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned int")]
        public static extern uint ZSTD_versionNumber();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ZSTD_versionString();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_compress(void* dst, [NativeTypeName("size_t")] nuint dstCapacity, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize, int compressionLevel);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_decompress(void* dst, [NativeTypeName("size_t")] nuint dstCapacity, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint compressedSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned long long")]
        public static extern ulong ZSTD_getFrameContentSize([NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned long long")]
        [Obsolete("Replaced by ZSTD_getFrameContentSize")]
        public static extern ulong ZSTD_getDecompressedSize([NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_findFrameCompressedSize([NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_compressBound([NativeTypeName("size_t")] nuint srcSize);

        [SuppressGCTransition]
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned int")]
        public static extern uint ZSTD_isError([NativeTypeName("size_t")] nuint code);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ZSTD_getErrorName([NativeTypeName("size_t")] nuint code);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ZSTD_minCLevel();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ZSTD_maxCLevel();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ZSTD_defaultCLevel();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_CCtx* ZSTD_createCCtx();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_freeCCtx(ZSTD_CCtx* cctx);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_compressCCtx(ZSTD_CCtx* cctx, void* dst, [NativeTypeName("size_t")] nuint dstCapacity, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize, int compressionLevel);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_DCtx* ZSTD_createDCtx();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_freeDCtx(ZSTD_DCtx* dctx);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_decompressDCtx(ZSTD_DCtx* dctx, void* dst, [NativeTypeName("size_t")] nuint dstCapacity, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_bounds ZSTD_cParam_getBounds(ZSTD_cParameter cParam);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CCtx_setParameter(ZSTD_CCtx* cctx, ZSTD_cParameter param1, int value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CCtx_setPledgedSrcSize(ZSTD_CCtx* cctx, [NativeTypeName("unsigned long long")] ulong pledgedSrcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CCtx_reset(ZSTD_CCtx* cctx, ZSTD_ResetDirective reset);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_compress2(ZSTD_CCtx* cctx, void* dst, [NativeTypeName("size_t")] nuint dstCapacity, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_bounds ZSTD_dParam_getBounds(ZSTD_dParameter dParam);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_DCtx_setParameter(ZSTD_DCtx* dctx, ZSTD_dParameter param1, int value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_DCtx_reset(ZSTD_DCtx* dctx, ZSTD_ResetDirective reset);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ZSTD_CStream *")]
        public static extern ZSTD_CCtx* ZSTD_createCStream();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_freeCStream([NativeTypeName("ZSTD_CStream *")] ZSTD_CCtx* zcs);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_compressStream2(ZSTD_CCtx* cctx, ZSTD_outBuffer* output, ZSTD_inBuffer* input, ZSTD_EndDirective endOp);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CStreamInSize();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CStreamOutSize();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_initCStream([NativeTypeName("ZSTD_CStream *")] ZSTD_CCtx* zcs, int compressionLevel);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_compressStream([NativeTypeName("ZSTD_CStream *")] ZSTD_CCtx* zcs, ZSTD_outBuffer* output, ZSTD_inBuffer* input);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_flushStream([NativeTypeName("ZSTD_CStream *")] ZSTD_CCtx* zcs, ZSTD_outBuffer* output);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_endStream([NativeTypeName("ZSTD_CStream *")] ZSTD_CCtx* zcs, ZSTD_outBuffer* output);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ZSTD_DStream *")]
        public static extern ZSTD_DCtx* ZSTD_createDStream();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_freeDStream([NativeTypeName("ZSTD_DStream *")] ZSTD_DCtx* zds);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_initDStream([NativeTypeName("ZSTD_DStream *")] ZSTD_DCtx* zds);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_decompressStream([NativeTypeName("ZSTD_DStream *")] ZSTD_DCtx* zds, ZSTD_outBuffer* output, ZSTD_inBuffer* input);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_DStreamInSize();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_DStreamOutSize();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_compress_usingDict(ZSTD_CCtx* ctx, void* dst, [NativeTypeName("size_t")] nuint dstCapacity, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize, [NativeTypeName("const void *")] void* dict, [NativeTypeName("size_t")] nuint dictSize, int compressionLevel);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_decompress_usingDict(ZSTD_DCtx* dctx, void* dst, [NativeTypeName("size_t")] nuint dstCapacity, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize, [NativeTypeName("const void *")] void* dict, [NativeTypeName("size_t")] nuint dictSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_CDict* ZSTD_createCDict([NativeTypeName("const void *")] void* dictBuffer, [NativeTypeName("size_t")] nuint dictSize, int compressionLevel);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_freeCDict(ZSTD_CDict* CDict);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_compress_usingCDict(ZSTD_CCtx* cctx, void* dst, [NativeTypeName("size_t")] nuint dstCapacity, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize, [NativeTypeName("const ZSTD_CDict *")] ZSTD_CDict* cdict);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_DDict* ZSTD_createDDict([NativeTypeName("const void *")] void* dictBuffer, [NativeTypeName("size_t")] nuint dictSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_freeDDict(ZSTD_DDict* ddict);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_decompress_usingDDict(ZSTD_DCtx* dctx, void* dst, [NativeTypeName("size_t")] nuint dstCapacity, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize, [NativeTypeName("const ZSTD_DDict *")] ZSTD_DDict* ddict);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned int")]
        public static extern uint ZSTD_getDictID_fromDict([NativeTypeName("const void *")] void* dict, [NativeTypeName("size_t")] nuint dictSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned int")]
        public static extern uint ZSTD_getDictID_fromCDict([NativeTypeName("const ZSTD_CDict *")] ZSTD_CDict* cdict);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned int")]
        public static extern uint ZSTD_getDictID_fromDDict([NativeTypeName("const ZSTD_DDict *")] ZSTD_DDict* ddict);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned int")]
        public static extern uint ZSTD_getDictID_fromFrame([NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CCtx_loadDictionary(ZSTD_CCtx* cctx, [NativeTypeName("const void *")] void* dict, [NativeTypeName("size_t")] nuint dictSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CCtx_refCDict(ZSTD_CCtx* cctx, [NativeTypeName("const ZSTD_CDict *")] ZSTD_CDict* cdict);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CCtx_refPrefix(ZSTD_CCtx* cctx, [NativeTypeName("const void *")] void* prefix, [NativeTypeName("size_t")] nuint prefixSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_DCtx_loadDictionary(ZSTD_DCtx* dctx, [NativeTypeName("const void *")] void* dict, [NativeTypeName("size_t")] nuint dictSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_DCtx_refDDict(ZSTD_DCtx* dctx, [NativeTypeName("const ZSTD_DDict *")] ZSTD_DDict* ddict);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_DCtx_refPrefix(ZSTD_DCtx* dctx, [NativeTypeName("const void *")] void* prefix, [NativeTypeName("size_t")] nuint prefixSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_sizeof_CCtx([NativeTypeName("const ZSTD_CCtx *")] ZSTD_CCtx* cctx);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_sizeof_DCtx([NativeTypeName("const ZSTD_DCtx *")] ZSTD_DCtx* dctx);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_sizeof_CStream([NativeTypeName("const ZSTD_CStream *")] ZSTD_CCtx* zcs);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_sizeof_DStream([NativeTypeName("const ZSTD_DStream *")] ZSTD_DCtx* zds);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_sizeof_CDict([NativeTypeName("const ZSTD_CDict *")] ZSTD_CDict* cdict);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_sizeof_DDict([NativeTypeName("const ZSTD_DDict *")] ZSTD_DDict* ddict);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned long long")]
        public static extern ulong ZSTD_findDecompressedSize([NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned long long")]
        public static extern ulong ZSTD_decompressBound([NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_frameHeaderSize([NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_getFrameHeader(ZSTD_frameHeader* zfhPtr, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_getFrameHeader_advanced(ZSTD_frameHeader* zfhPtr, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize, ZSTD_format_e format);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_decompressionMargin([NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_sequenceBound([NativeTypeName("size_t")] nuint srcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("For debugging only, will be replaced by ZSTD_extractSequences()")]
        public static extern nuint ZSTD_generateSequences(ZSTD_CCtx* zc, ZSTD_Sequence* outSeqs, [NativeTypeName("size_t")] nuint outSeqsSize, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_mergeBlockDelimiters(ZSTD_Sequence* sequences, [NativeTypeName("size_t")] nuint seqsSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_compressSequences(ZSTD_CCtx* cctx, void* dst, [NativeTypeName("size_t")] nuint dstSize, [NativeTypeName("const ZSTD_Sequence *")] ZSTD_Sequence* inSeqs, [NativeTypeName("size_t")] nuint inSeqsSize, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_writeSkippableFrame(void* dst, [NativeTypeName("size_t")] nuint dstCapacity, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize, [NativeTypeName("unsigned int")] uint magicVariant);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_readSkippableFrame(void* dst, [NativeTypeName("size_t")] nuint dstCapacity, [NativeTypeName("unsigned int *")] uint* magicVariant, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned int")]
        public static extern uint ZSTD_isSkippableFrame([NativeTypeName("const void *")] void* buffer, [NativeTypeName("size_t")] nuint size);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_estimateCCtxSize(int maxCompressionLevel);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_estimateCCtxSize_usingCParams(ZSTD_compressionParameters cParams);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_estimateCCtxSize_usingCCtxParams([NativeTypeName("const ZSTD_CCtx_params *")] ZSTD_CCtx_params* @params);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_estimateDCtxSize();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_estimateCStreamSize(int maxCompressionLevel);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_estimateCStreamSize_usingCParams(ZSTD_compressionParameters cParams);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_estimateCStreamSize_usingCCtxParams([NativeTypeName("const ZSTD_CCtx_params *")] ZSTD_CCtx_params* @params);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_estimateDStreamSize([NativeTypeName("size_t")] nuint maxWindowSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_estimateDStreamSize_fromFrame([NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_estimateCDictSize([NativeTypeName("size_t")] nuint dictSize, int compressionLevel);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_estimateCDictSize_advanced([NativeTypeName("size_t")] nuint dictSize, ZSTD_compressionParameters cParams, ZSTD_dictLoadMethod_e dictLoadMethod);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_estimateDDictSize([NativeTypeName("size_t")] nuint dictSize, ZSTD_dictLoadMethod_e dictLoadMethod);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_CCtx* ZSTD_initStaticCCtx(void* workspace, [NativeTypeName("size_t")] nuint workspaceSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ZSTD_CStream *")]
        public static extern ZSTD_CCtx* ZSTD_initStaticCStream(void* workspace, [NativeTypeName("size_t")] nuint workspaceSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_DCtx* ZSTD_initStaticDCtx(void* workspace, [NativeTypeName("size_t")] nuint workspaceSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ZSTD_DStream *")]
        public static extern ZSTD_DCtx* ZSTD_initStaticDStream(void* workspace, [NativeTypeName("size_t")] nuint workspaceSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const ZSTD_CDict *")]
        public static extern ZSTD_CDict* ZSTD_initStaticCDict(void* workspace, [NativeTypeName("size_t")] nuint workspaceSize, [NativeTypeName("const void *")] void* dict, [NativeTypeName("size_t")] nuint dictSize, ZSTD_dictLoadMethod_e dictLoadMethod, ZSTD_dictContentType_e dictContentType, ZSTD_compressionParameters cParams);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const ZSTD_DDict *")]
        public static extern ZSTD_DDict* ZSTD_initStaticDDict(void* workspace, [NativeTypeName("size_t")] nuint workspaceSize, [NativeTypeName("const void *")] void* dict, [NativeTypeName("size_t")] nuint dictSize, ZSTD_dictLoadMethod_e dictLoadMethod, ZSTD_dictContentType_e dictContentType);

        [NativeTypeName("const ZSTD_customMem")]
        public static readonly ZSTD_customMem ZSTD_defaultCMem = new ZSTD_customMem
        {
            customAlloc = null,
            customFree = null,
            opaque = null,
        };

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_CCtx* ZSTD_createCCtx_advanced(ZSTD_customMem customMem);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ZSTD_CStream *")]
        public static extern ZSTD_CCtx* ZSTD_createCStream_advanced(ZSTD_customMem customMem);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_DCtx* ZSTD_createDCtx_advanced(ZSTD_customMem customMem);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("ZSTD_DStream *")]
        public static extern ZSTD_DCtx* ZSTD_createDStream_advanced(ZSTD_customMem customMem);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_CDict* ZSTD_createCDict_advanced([NativeTypeName("const void *")] void* dict, [NativeTypeName("size_t")] nuint dictSize, ZSTD_dictLoadMethod_e dictLoadMethod, ZSTD_dictContentType_e dictContentType, ZSTD_compressionParameters cParams, ZSTD_customMem customMem);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_threadPool* ZSTD_createThreadPool([NativeTypeName("size_t")] nuint numThreads);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ZSTD_freeThreadPool(ZSTD_threadPool* pool);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CCtx_refThreadPool(ZSTD_CCtx* cctx, ZSTD_threadPool* pool);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_CDict* ZSTD_createCDict_advanced2([NativeTypeName("const void *")] void* dict, [NativeTypeName("size_t")] nuint dictSize, ZSTD_dictLoadMethod_e dictLoadMethod, ZSTD_dictContentType_e dictContentType, [NativeTypeName("const ZSTD_CCtx_params *")] ZSTD_CCtx_params* cctxParams, ZSTD_customMem customMem);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_DDict* ZSTD_createDDict_advanced([NativeTypeName("const void *")] void* dict, [NativeTypeName("size_t")] nuint dictSize, ZSTD_dictLoadMethod_e dictLoadMethod, ZSTD_dictContentType_e dictContentType, ZSTD_customMem customMem);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_CDict* ZSTD_createCDict_byReference([NativeTypeName("const void *")] void* dictBuffer, [NativeTypeName("size_t")] nuint dictSize, int compressionLevel);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_compressionParameters ZSTD_getCParams(int compressionLevel, [NativeTypeName("unsigned long long")] ulong estimatedSrcSize, [NativeTypeName("size_t")] nuint dictSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_parameters ZSTD_getParams(int compressionLevel, [NativeTypeName("unsigned long long")] ulong estimatedSrcSize, [NativeTypeName("size_t")] nuint dictSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_checkCParams(ZSTD_compressionParameters @params);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_compressionParameters ZSTD_adjustCParams(ZSTD_compressionParameters cPar, [NativeTypeName("unsigned long long")] ulong srcSize, [NativeTypeName("size_t")] nuint dictSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CCtx_setCParams(ZSTD_CCtx* cctx, ZSTD_compressionParameters cparams);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CCtx_setFParams(ZSTD_CCtx* cctx, ZSTD_frameParameters fparams);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CCtx_setParams(ZSTD_CCtx* cctx, ZSTD_parameters @params);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("use ZSTD_compress2")]
        public static extern nuint ZSTD_compress_advanced(ZSTD_CCtx* cctx, void* dst, [NativeTypeName("size_t")] nuint dstCapacity, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize, [NativeTypeName("const void *")] void* dict, [NativeTypeName("size_t")] nuint dictSize, ZSTD_parameters @params);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("use ZSTD_compress2 with ZSTD_CCtx_loadDictionary")]
        public static extern nuint ZSTD_compress_usingCDict_advanced(ZSTD_CCtx* cctx, void* dst, [NativeTypeName("size_t")] nuint dstCapacity, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize, [NativeTypeName("const ZSTD_CDict *")] ZSTD_CDict* cdict, ZSTD_frameParameters fParams);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CCtx_loadDictionary_byReference(ZSTD_CCtx* cctx, [NativeTypeName("const void *")] void* dict, [NativeTypeName("size_t")] nuint dictSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CCtx_loadDictionary_advanced(ZSTD_CCtx* cctx, [NativeTypeName("const void *")] void* dict, [NativeTypeName("size_t")] nuint dictSize, ZSTD_dictLoadMethod_e dictLoadMethod, ZSTD_dictContentType_e dictContentType);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CCtx_refPrefix_advanced(ZSTD_CCtx* cctx, [NativeTypeName("const void *")] void* prefix, [NativeTypeName("size_t")] nuint prefixSize, ZSTD_dictContentType_e dictContentType);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CCtx_getParameter([NativeTypeName("const ZSTD_CCtx *")] ZSTD_CCtx* cctx, ZSTD_cParameter param1, int* value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_CCtx_params* ZSTD_createCCtxParams();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_freeCCtxParams(ZSTD_CCtx_params* @params);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CCtxParams_reset(ZSTD_CCtx_params* @params);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CCtxParams_init(ZSTD_CCtx_params* cctxParams, int compressionLevel);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CCtxParams_init_advanced(ZSTD_CCtx_params* cctxParams, ZSTD_parameters @params);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CCtxParams_setParameter(ZSTD_CCtx_params* @params, ZSTD_cParameter param1, int value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CCtxParams_getParameter([NativeTypeName("const ZSTD_CCtx_params *")] ZSTD_CCtx_params* @params, ZSTD_cParameter param1, int* value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_CCtx_setParametersUsingCCtxParams(ZSTD_CCtx* cctx, [NativeTypeName("const ZSTD_CCtx_params *")] ZSTD_CCtx_params* @params);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_compressStream2_simpleArgs(ZSTD_CCtx* cctx, void* dst, [NativeTypeName("size_t")] nuint dstCapacity, [NativeTypeName("size_t *")] nuint* dstPos, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize, [NativeTypeName("size_t *")] nuint* srcPos, ZSTD_EndDirective endOp);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("unsigned int")]
        public static extern uint ZSTD_isFrame([NativeTypeName("const void *")] void* buffer, [NativeTypeName("size_t")] nuint size);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_DDict* ZSTD_createDDict_byReference([NativeTypeName("const void *")] void* dictBuffer, [NativeTypeName("size_t")] nuint dictSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_DCtx_loadDictionary_byReference(ZSTD_DCtx* dctx, [NativeTypeName("const void *")] void* dict, [NativeTypeName("size_t")] nuint dictSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_DCtx_loadDictionary_advanced(ZSTD_DCtx* dctx, [NativeTypeName("const void *")] void* dict, [NativeTypeName("size_t")] nuint dictSize, ZSTD_dictLoadMethod_e dictLoadMethod, ZSTD_dictContentType_e dictContentType);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_DCtx_refPrefix_advanced(ZSTD_DCtx* dctx, [NativeTypeName("const void *")] void* prefix, [NativeTypeName("size_t")] nuint prefixSize, ZSTD_dictContentType_e dictContentType);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_DCtx_setMaxWindowSize(ZSTD_DCtx* dctx, [NativeTypeName("size_t")] nuint maxWindowSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_DCtx_getParameter(ZSTD_DCtx* dctx, ZSTD_dParameter param1, int* value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("use ZSTD_DCtx_setParameter() instead")]
        public static extern nuint ZSTD_DCtx_setFormat(ZSTD_DCtx* dctx, ZSTD_format_e format);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_decompressStream_simpleArgs(ZSTD_DCtx* dctx, void* dst, [NativeTypeName("size_t")] nuint dstCapacity, [NativeTypeName("size_t *")] nuint* dstPos, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize, [NativeTypeName("size_t *")] nuint* srcPos);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("use ZSTD_CCtx_reset, see zstd.h for detailed instructions")]
        public static extern nuint ZSTD_initCStream_srcSize([NativeTypeName("ZSTD_CStream *")] ZSTD_CCtx* zcs, int compressionLevel, [NativeTypeName("unsigned long long")] ulong pledgedSrcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("use ZSTD_CCtx_reset, see zstd.h for detailed instructions")]
        public static extern nuint ZSTD_initCStream_usingDict([NativeTypeName("ZSTD_CStream *")] ZSTD_CCtx* zcs, [NativeTypeName("const void *")] void* dict, [NativeTypeName("size_t")] nuint dictSize, int compressionLevel);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("use ZSTD_CCtx_reset, see zstd.h for detailed instructions")]
        public static extern nuint ZSTD_initCStream_advanced([NativeTypeName("ZSTD_CStream *")] ZSTD_CCtx* zcs, [NativeTypeName("const void *")] void* dict, [NativeTypeName("size_t")] nuint dictSize, ZSTD_parameters @params, [NativeTypeName("unsigned long long")] ulong pledgedSrcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("use ZSTD_CCtx_reset and ZSTD_CCtx_refCDict, see zstd.h for detailed instructions")]
        public static extern nuint ZSTD_initCStream_usingCDict([NativeTypeName("ZSTD_CStream *")] ZSTD_CCtx* zcs, [NativeTypeName("const ZSTD_CDict *")] ZSTD_CDict* cdict);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("use ZSTD_CCtx_reset and ZSTD_CCtx_refCDict, see zstd.h for detailed instructions")]
        public static extern nuint ZSTD_initCStream_usingCDict_advanced([NativeTypeName("ZSTD_CStream *")] ZSTD_CCtx* zcs, [NativeTypeName("const ZSTD_CDict *")] ZSTD_CDict* cdict, ZSTD_frameParameters fParams, [NativeTypeName("unsigned long long")] ulong pledgedSrcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("use ZSTD_CCtx_reset, see zstd.h for detailed instructions")]
        public static extern nuint ZSTD_resetCStream([NativeTypeName("ZSTD_CStream *")] ZSTD_CCtx* zcs, [NativeTypeName("unsigned long long")] ulong pledgedSrcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_frameProgression ZSTD_getFrameProgression([NativeTypeName("const ZSTD_CCtx *")] ZSTD_CCtx* cctx);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_toFlushNow(ZSTD_CCtx* cctx);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("use ZSTD_DCtx_reset + ZSTD_DCtx_loadDictionary, see zstd.h for detailed instructions")]
        public static extern nuint ZSTD_initDStream_usingDict([NativeTypeName("ZSTD_DStream *")] ZSTD_DCtx* zds, [NativeTypeName("const void *")] void* dict, [NativeTypeName("size_t")] nuint dictSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("use ZSTD_DCtx_reset + ZSTD_DCtx_refDDict, see zstd.h for detailed instructions")]
        public static extern nuint ZSTD_initDStream_usingDDict([NativeTypeName("ZSTD_DStream *")] ZSTD_DCtx* zds, [NativeTypeName("const ZSTD_DDict *")] ZSTD_DDict* ddict);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("use ZSTD_DCtx_reset, see zstd.h for detailed instructions")]
        public static extern nuint ZSTD_resetDStream([NativeTypeName("ZSTD_DStream *")] ZSTD_DCtx* zds);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ZSTD_registerSequenceProducer(ZSTD_CCtx* cctx, void* sequenceProducerState, [NativeTypeName("ZSTD_sequenceProducer_F")] delegate* unmanaged[Cdecl]<void*, ZSTD_Sequence*, nuint, void*, nuint, void*, nuint, int, nuint, nuint> sequenceProducer);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void ZSTD_CCtxParams_registerSequenceProducer(ZSTD_CCtx_params* @params, void* sequenceProducerState, [NativeTypeName("ZSTD_sequenceProducer_F")] delegate* unmanaged[Cdecl]<void*, ZSTD_Sequence*, nuint, void*, nuint, void*, nuint, int, nuint, nuint> sequenceProducer);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("The buffer-less API is deprecated in favor of the normal streaming API. See docs.")]
        public static extern nuint ZSTD_compressBegin(ZSTD_CCtx* cctx, int compressionLevel);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("The buffer-less API is deprecated in favor of the normal streaming API. See docs.")]
        public static extern nuint ZSTD_compressBegin_usingDict(ZSTD_CCtx* cctx, [NativeTypeName("const void *")] void* dict, [NativeTypeName("size_t")] nuint dictSize, int compressionLevel);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("The buffer-less API is deprecated in favor of the normal streaming API. See docs.")]
        public static extern nuint ZSTD_compressBegin_usingCDict(ZSTD_CCtx* cctx, [NativeTypeName("const ZSTD_CDict *")] ZSTD_CDict* cdict);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("This function will likely be removed in a future release. It is misleading and has very limited utility.")]
        public static extern nuint ZSTD_copyCCtx(ZSTD_CCtx* cctx, [NativeTypeName("const ZSTD_CCtx *")] ZSTD_CCtx* preparedCCtx, [NativeTypeName("unsigned long long")] ulong pledgedSrcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("The buffer-less API is deprecated in favor of the normal streaming API. See docs.")]
        public static extern nuint ZSTD_compressContinue(ZSTD_CCtx* cctx, void* dst, [NativeTypeName("size_t")] nuint dstCapacity, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("The buffer-less API is deprecated in favor of the normal streaming API. See docs.")]
        public static extern nuint ZSTD_compressEnd(ZSTD_CCtx* cctx, void* dst, [NativeTypeName("size_t")] nuint dstCapacity, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("use advanced API to access custom parameters")]
        public static extern nuint ZSTD_compressBegin_advanced(ZSTD_CCtx* cctx, [NativeTypeName("const void *")] void* dict, [NativeTypeName("size_t")] nuint dictSize, ZSTD_parameters @params, [NativeTypeName("unsigned long long")] ulong pledgedSrcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("use advanced API to access custom parameters")]
        public static extern nuint ZSTD_compressBegin_usingCDict_advanced(ZSTD_CCtx* cctx, [NativeTypeName("const ZSTD_CDict *const")] ZSTD_CDict* cdict, [NativeTypeName("const ZSTD_frameParameters")] ZSTD_frameParameters fParams, [NativeTypeName("const unsigned long long")] ulong pledgedSrcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_decodingBufferSize_min([NativeTypeName("unsigned long long")] ulong windowSize, [NativeTypeName("unsigned long long")] ulong frameContentSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_decompressBegin(ZSTD_DCtx* dctx);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_decompressBegin_usingDict(ZSTD_DCtx* dctx, [NativeTypeName("const void *")] void* dict, [NativeTypeName("size_t")] nuint dictSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_decompressBegin_usingDDict(ZSTD_DCtx* dctx, [NativeTypeName("const ZSTD_DDict *")] ZSTD_DDict* ddict);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_nextSrcSizeToDecompress(ZSTD_DCtx* dctx);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint ZSTD_decompressContinue(ZSTD_DCtx* dctx, void* dst, [NativeTypeName("size_t")] nuint dstCapacity, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [Obsolete("This function will likely be removed in the next minor release. It is misleading and has very limited utility.")]
        public static extern void ZSTD_copyDCtx(ZSTD_DCtx* dctx, [NativeTypeName("const ZSTD_DCtx *")] ZSTD_DCtx* preparedDCtx);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_nextInputType_e ZSTD_nextInputType(ZSTD_DCtx* dctx);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("The block API is deprecated in favor of the normal compression API. See docs.")]
        public static extern nuint ZSTD_getBlockSize([NativeTypeName("const ZSTD_CCtx *")] ZSTD_CCtx* cctx);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("The block API is deprecated in favor of the normal compression API. See docs.")]
        public static extern nuint ZSTD_compressBlock(ZSTD_CCtx* cctx, void* dst, [NativeTypeName("size_t")] nuint dstCapacity, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("The block API is deprecated in favor of the normal compression API. See docs.")]
        public static extern nuint ZSTD_decompressBlock(ZSTD_DCtx* dctx, void* dst, [NativeTypeName("size_t")] nuint dstCapacity, [NativeTypeName("const void *")] void* src, [NativeTypeName("size_t")] nuint srcSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("size_t")]
        [Obsolete("The block API is deprecated in favor of the normal compression API. See docs.")]
        public static extern nuint ZSTD_insertBlock(ZSTD_DCtx* dctx, [NativeTypeName("const void *")] void* blockStart, [NativeTypeName("size_t")] nuint blockSize);

        [NativeTypeName("#define ZSTD_VERSION_MAJOR 1")]
        public const int ZSTD_VERSION_MAJOR = 1;

        [NativeTypeName("#define ZSTD_VERSION_MINOR 5")]
        public const int ZSTD_VERSION_MINOR = 5;

        [NativeTypeName("#define ZSTD_VERSION_RELEASE 6")]
        public const int ZSTD_VERSION_RELEASE = 6;

        [NativeTypeName("#define ZSTD_VERSION_NUMBER (ZSTD_VERSION_MAJOR *100*100 + ZSTD_VERSION_MINOR *100 + ZSTD_VERSION_RELEASE)")]
        public const int ZSTD_VERSION_NUMBER = (1 * 100 * 100 + 5 * 100 + 6);

        [NativeTypeName("#define ZSTD_VERSION_STRING ZSTD_EXPAND_AND_QUOTE(ZSTD_LIB_VERSION)")]
        public static ReadOnlySpan<byte> ZSTD_VERSION_STRING => "1.5.6"u8;

        [NativeTypeName("#define ZSTD_CLEVEL_DEFAULT 3")]
        public const int ZSTD_CLEVEL_DEFAULT = 3;

        [NativeTypeName("#define ZSTD_MAGICNUMBER 0xFD2FB528")]
        public const uint ZSTD_MAGICNUMBER = 0xFD2FB528;

        [NativeTypeName("#define ZSTD_MAGIC_DICTIONARY 0xEC30A437")]
        public const uint ZSTD_MAGIC_DICTIONARY = 0xEC30A437;

        [NativeTypeName("#define ZSTD_MAGIC_SKIPPABLE_START 0x184D2A50")]
        public const int ZSTD_MAGIC_SKIPPABLE_START = 0x184D2A50;

        [NativeTypeName("#define ZSTD_MAGIC_SKIPPABLE_MASK 0xFFFFFFF0")]
        public const uint ZSTD_MAGIC_SKIPPABLE_MASK = 0xFFFFFFF0;

        [NativeTypeName("#define ZSTD_BLOCKSIZELOG_MAX 17")]
        public const int ZSTD_BLOCKSIZELOG_MAX = 17;

        [NativeTypeName("#define ZSTD_BLOCKSIZE_MAX (1<<ZSTD_BLOCKSIZELOG_MAX)")]
        public const int ZSTD_BLOCKSIZE_MAX = (1 << 17);

        [NativeTypeName("#define ZSTD_CONTENTSIZE_UNKNOWN (0ULL - 1)")]
        public const ulong ZSTD_CONTENTSIZE_UNKNOWN = unchecked(0UL - 1);

        [NativeTypeName("#define ZSTD_CONTENTSIZE_ERROR (0ULL - 2)")]
        public const ulong ZSTD_CONTENTSIZE_ERROR = unchecked(0UL - 2);

        [NativeTypeName("#define ZSTD_MAX_INPUT_SIZE ((sizeof(size_t)==8) ? 0xFF00FF00FF00FF00ULL : 0xFF00FF00U)")]
        public static readonly ulong ZSTD_MAX_INPUT_SIZE = ((sizeof(nuint) == 8) ? 0xFF00FF00FF00FF00UL : 0xFF00FF00U);

        [NativeTypeName("#define ZSTD_FRAMEHEADERSIZE_MAX 18")]
        public const int ZSTD_FRAMEHEADERSIZE_MAX = 18;

        [NativeTypeName("#define ZSTD_SKIPPABLEHEADERSIZE 8")]
        public const int ZSTD_SKIPPABLEHEADERSIZE = 8;

        [NativeTypeName("#define ZSTD_WINDOWLOG_MAX_32 30")]
        public const int ZSTD_WINDOWLOG_MAX_32 = 30;

        [NativeTypeName("#define ZSTD_WINDOWLOG_MAX_64 31")]
        public const int ZSTD_WINDOWLOG_MAX_64 = 31;

        [NativeTypeName("#define ZSTD_WINDOWLOG_MAX ((int)(sizeof(size_t) == 4 ? ZSTD_WINDOWLOG_MAX_32 : ZSTD_WINDOWLOG_MAX_64))")]
        public static readonly int ZSTD_WINDOWLOG_MAX = ((int)(sizeof(nuint) == 4 ? 30 : 31));

        [NativeTypeName("#define ZSTD_WINDOWLOG_MIN 10")]
        public const int ZSTD_WINDOWLOG_MIN = 10;

        [NativeTypeName("#define ZSTD_HASHLOG_MAX ((ZSTD_WINDOWLOG_MAX < 30) ? ZSTD_WINDOWLOG_MAX : 30)")]
        public static readonly int ZSTD_HASHLOG_MAX = ((((int)(sizeof(nuint) == 4 ? 30 : 31)) < 30) ? ((int)(sizeof(nuint) == 4 ? 30 : 31)) : 30);

        [NativeTypeName("#define ZSTD_HASHLOG_MIN 6")]
        public const int ZSTD_HASHLOG_MIN = 6;

        [NativeTypeName("#define ZSTD_CHAINLOG_MAX_32 29")]
        public const int ZSTD_CHAINLOG_MAX_32 = 29;

        [NativeTypeName("#define ZSTD_CHAINLOG_MAX_64 30")]
        public const int ZSTD_CHAINLOG_MAX_64 = 30;

        [NativeTypeName("#define ZSTD_CHAINLOG_MAX ((int)(sizeof(size_t) == 4 ? ZSTD_CHAINLOG_MAX_32 : ZSTD_CHAINLOG_MAX_64))")]
        public static readonly int ZSTD_CHAINLOG_MAX = ((int)(sizeof(nuint) == 4 ? 29 : 30));

        [NativeTypeName("#define ZSTD_CHAINLOG_MIN ZSTD_HASHLOG_MIN")]
        public const int ZSTD_CHAINLOG_MIN = 6;

        [NativeTypeName("#define ZSTD_SEARCHLOG_MAX (ZSTD_WINDOWLOG_MAX-1)")]
        public static readonly int ZSTD_SEARCHLOG_MAX = (((int)(sizeof(nuint) == 4 ? 30 : 31)) - 1);

        [NativeTypeName("#define ZSTD_SEARCHLOG_MIN 1")]
        public const int ZSTD_SEARCHLOG_MIN = 1;

        [NativeTypeName("#define ZSTD_MINMATCH_MAX 7")]
        public const int ZSTD_MINMATCH_MAX = 7;

        [NativeTypeName("#define ZSTD_MINMATCH_MIN 3")]
        public const int ZSTD_MINMATCH_MIN = 3;

        [NativeTypeName("#define ZSTD_TARGETLENGTH_MAX ZSTD_BLOCKSIZE_MAX")]
        public const int ZSTD_TARGETLENGTH_MAX = (1 << 17);

        [NativeTypeName("#define ZSTD_TARGETLENGTH_MIN 0")]
        public const int ZSTD_TARGETLENGTH_MIN = 0;

        [NativeTypeName("#define ZSTD_STRATEGY_MIN ZSTD_fast")]
        public const ZSTD_strategy ZSTD_STRATEGY_MIN = ZSTD_fast;

        [NativeTypeName("#define ZSTD_STRATEGY_MAX ZSTD_btultra2")]
        public const ZSTD_strategy ZSTD_STRATEGY_MAX = ZSTD_btultra2;

        [NativeTypeName("#define ZSTD_BLOCKSIZE_MAX_MIN (1 << 10)")]
        public const int ZSTD_BLOCKSIZE_MAX_MIN = (1 << 10);

        [NativeTypeName("#define ZSTD_OVERLAPLOG_MIN 0")]
        public const int ZSTD_OVERLAPLOG_MIN = 0;

        [NativeTypeName("#define ZSTD_OVERLAPLOG_MAX 9")]
        public const int ZSTD_OVERLAPLOG_MAX = 9;

        [NativeTypeName("#define ZSTD_WINDOWLOG_LIMIT_DEFAULT 27")]
        public const int ZSTD_WINDOWLOG_LIMIT_DEFAULT = 27;

        [NativeTypeName("#define ZSTD_LDM_HASHLOG_MIN ZSTD_HASHLOG_MIN")]
        public const int ZSTD_LDM_HASHLOG_MIN = 6;

        [NativeTypeName("#define ZSTD_LDM_HASHLOG_MAX ZSTD_HASHLOG_MAX")]
        public static readonly int ZSTD_LDM_HASHLOG_MAX = ((((int)(sizeof(nuint) == 4 ? 30 : 31)) < 30) ? ((int)(sizeof(nuint) == 4 ? 30 : 31)) : 30);

        [NativeTypeName("#define ZSTD_LDM_MINMATCH_MIN 4")]
        public const int ZSTD_LDM_MINMATCH_MIN = 4;

        [NativeTypeName("#define ZSTD_LDM_MINMATCH_MAX 4096")]
        public const int ZSTD_LDM_MINMATCH_MAX = 4096;

        [NativeTypeName("#define ZSTD_LDM_BUCKETSIZELOG_MIN 1")]
        public const int ZSTD_LDM_BUCKETSIZELOG_MIN = 1;

        [NativeTypeName("#define ZSTD_LDM_BUCKETSIZELOG_MAX 8")]
        public const int ZSTD_LDM_BUCKETSIZELOG_MAX = 8;

        [NativeTypeName("#define ZSTD_LDM_HASHRATELOG_MIN 0")]
        public const int ZSTD_LDM_HASHRATELOG_MIN = 0;

        [NativeTypeName("#define ZSTD_LDM_HASHRATELOG_MAX (ZSTD_WINDOWLOG_MAX - ZSTD_HASHLOG_MIN)")]
        public static readonly int ZSTD_LDM_HASHRATELOG_MAX = (((int)(sizeof(nuint) == 4 ? 30 : 31)) - 6);

        [NativeTypeName("#define ZSTD_TARGETCBLOCKSIZE_MIN 1340")]
        public const int ZSTD_TARGETCBLOCKSIZE_MIN = 1340;

        [NativeTypeName("#define ZSTD_TARGETCBLOCKSIZE_MAX ZSTD_BLOCKSIZE_MAX")]
        public const int ZSTD_TARGETCBLOCKSIZE_MAX = (1 << 17);

        [NativeTypeName("#define ZSTD_SRCSIZEHINT_MIN 0")]
        public const int ZSTD_SRCSIZEHINT_MIN = 0;

        [NativeTypeName("#define ZSTD_SRCSIZEHINT_MAX INT_MAX")]
        public const int ZSTD_SRCSIZEHINT_MAX = 2147483647;

        [NativeTypeName("#define ZSTD_c_rsyncable ZSTD_c_experimentalParam1")]
        public const ZSTD_cParameter ZSTD_c_rsyncable = ZSTD_c_experimentalParam1;

        [NativeTypeName("#define ZSTD_c_format ZSTD_c_experimentalParam2")]
        public const ZSTD_cParameter ZSTD_c_format = ZSTD_c_experimentalParam2;

        [NativeTypeName("#define ZSTD_c_forceMaxWindow ZSTD_c_experimentalParam3")]
        public const ZSTD_cParameter ZSTD_c_forceMaxWindow = ZSTD_c_experimentalParam3;

        [NativeTypeName("#define ZSTD_c_forceAttachDict ZSTD_c_experimentalParam4")]
        public const ZSTD_cParameter ZSTD_c_forceAttachDict = ZSTD_c_experimentalParam4;

        [NativeTypeName("#define ZSTD_c_literalCompressionMode ZSTD_c_experimentalParam5")]
        public const ZSTD_cParameter ZSTD_c_literalCompressionMode = ZSTD_c_experimentalParam5;

        [NativeTypeName("#define ZSTD_c_srcSizeHint ZSTD_c_experimentalParam7")]
        public const ZSTD_cParameter ZSTD_c_srcSizeHint = ZSTD_c_experimentalParam7;

        [NativeTypeName("#define ZSTD_c_enableDedicatedDictSearch ZSTD_c_experimentalParam8")]
        public const ZSTD_cParameter ZSTD_c_enableDedicatedDictSearch = ZSTD_c_experimentalParam8;

        [NativeTypeName("#define ZSTD_c_stableInBuffer ZSTD_c_experimentalParam9")]
        public const ZSTD_cParameter ZSTD_c_stableInBuffer = ZSTD_c_experimentalParam9;

        [NativeTypeName("#define ZSTD_c_stableOutBuffer ZSTD_c_experimentalParam10")]
        public const ZSTD_cParameter ZSTD_c_stableOutBuffer = ZSTD_c_experimentalParam10;

        [NativeTypeName("#define ZSTD_c_blockDelimiters ZSTD_c_experimentalParam11")]
        public const ZSTD_cParameter ZSTD_c_blockDelimiters = ZSTD_c_experimentalParam11;

        [NativeTypeName("#define ZSTD_c_validateSequences ZSTD_c_experimentalParam12")]
        public const ZSTD_cParameter ZSTD_c_validateSequences = ZSTD_c_experimentalParam12;

        [NativeTypeName("#define ZSTD_c_useBlockSplitter ZSTD_c_experimentalParam13")]
        public const ZSTD_cParameter ZSTD_c_useBlockSplitter = ZSTD_c_experimentalParam13;

        [NativeTypeName("#define ZSTD_c_useRowMatchFinder ZSTD_c_experimentalParam14")]
        public const ZSTD_cParameter ZSTD_c_useRowMatchFinder = ZSTD_c_experimentalParam14;

        [NativeTypeName("#define ZSTD_c_deterministicRefPrefix ZSTD_c_experimentalParam15")]
        public const ZSTD_cParameter ZSTD_c_deterministicRefPrefix = ZSTD_c_experimentalParam15;

        [NativeTypeName("#define ZSTD_c_prefetchCDictTables ZSTD_c_experimentalParam16")]
        public const ZSTD_cParameter ZSTD_c_prefetchCDictTables = ZSTD_c_experimentalParam16;

        [NativeTypeName("#define ZSTD_c_enableSeqProducerFallback ZSTD_c_experimentalParam17")]
        public const ZSTD_cParameter ZSTD_c_enableSeqProducerFallback = ZSTD_c_experimentalParam17;

        [NativeTypeName("#define ZSTD_c_maxBlockSize ZSTD_c_experimentalParam18")]
        public const ZSTD_cParameter ZSTD_c_maxBlockSize = ZSTD_c_experimentalParam18;

        [NativeTypeName("#define ZSTD_c_searchForExternalRepcodes ZSTD_c_experimentalParam19")]
        public const ZSTD_cParameter ZSTD_c_searchForExternalRepcodes = ZSTD_c_experimentalParam19;

        [NativeTypeName("#define ZSTD_d_format ZSTD_d_experimentalParam1")]
        public const ZSTD_dParameter ZSTD_d_format = ZSTD_d_experimentalParam1;

        [NativeTypeName("#define ZSTD_d_stableOutBuffer ZSTD_d_experimentalParam2")]
        public const ZSTD_dParameter ZSTD_d_stableOutBuffer = ZSTD_d_experimentalParam2;

        [NativeTypeName("#define ZSTD_d_forceIgnoreChecksum ZSTD_d_experimentalParam3")]
        public const ZSTD_dParameter ZSTD_d_forceIgnoreChecksum = ZSTD_d_experimentalParam3;

        [NativeTypeName("#define ZSTD_d_refMultipleDDicts ZSTD_d_experimentalParam4")]
        public const ZSTD_dParameter ZSTD_d_refMultipleDDicts = ZSTD_d_experimentalParam4;

        [NativeTypeName("#define ZSTD_d_disableHuffmanAssembly ZSTD_d_experimentalParam5")]
        public const ZSTD_dParameter ZSTD_d_disableHuffmanAssembly = ZSTD_d_experimentalParam5;

        [NativeTypeName("#define ZSTD_d_maxBlockSize ZSTD_d_experimentalParam6")]
        public const ZSTD_dParameter ZSTD_d_maxBlockSize = ZSTD_d_experimentalParam6;

        [NativeTypeName("#define ZSTD_SEQUENCE_PRODUCER_ERROR ((size_t)(-1))")]
        public static readonly nuint ZSTD_SEQUENCE_PRODUCER_ERROR = unchecked((nuint)(-1));
        
        [SuppressGCTransition]
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ZSTD_ErrorCode ZSTD_getErrorCode([NativeTypeName("size_t")] nuint functionResult);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* ZSTD_getErrorString(ZSTD_ErrorCode code);
    }
}
