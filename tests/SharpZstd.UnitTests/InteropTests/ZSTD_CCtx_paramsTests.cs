using NUnit.Framework;
using System.Runtime.InteropServices;

namespace SharpZstd.Interop.UnitTests
{
    /// <summary>Provides validation of the <see cref="ZSTD_CCtx_params" /> struct.</summary>
    public static unsafe partial class ZSTD_CCtx_paramsTests
    {
        /// <summary>Validates that the <see cref="ZSTD_CCtx_params" /> struct is blittable.</summary>
        [Test]
        public static void IsBlittableTest()
        {
            Assert.That(Marshal.SizeOf<ZSTD_CCtx_params>(), Is.EqualTo(sizeof(ZSTD_CCtx_params)));
        }

        /// <summary>Validates that the <see cref="ZSTD_CCtx_params" /> struct has the right <see cref="LayoutKind" />.</summary>
        [Test]
        public static void IsLayoutSequentialTest()
        {
            Assert.That(typeof(ZSTD_CCtx_params).IsLayoutSequential, Is.True);
        }

        /// <summary>Validates that the <see cref="ZSTD_CCtx_params" /> struct has the correct size.</summary>
        [Test]
        public static void SizeOfTest()
        {
            Assert.That(sizeof(ZSTD_CCtx_params), Is.EqualTo(1));
        }
    }
}
