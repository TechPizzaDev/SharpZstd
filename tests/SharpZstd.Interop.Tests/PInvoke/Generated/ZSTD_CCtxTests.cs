using NUnit.Framework;
using System.Runtime.InteropServices;

namespace SharpZstd.Interop.UnitTests
{
    /// <summary>Provides validation of the <see cref="ZSTD_CCtx" /> struct.</summary>
    public static unsafe partial class ZSTD_CCtxTests
    {
        /// <summary>Validates that the <see cref="ZSTD_CCtx" /> struct is blittable.</summary>
        [Test]
        public static void IsBlittableTest()
        {
            Assert.That(Marshal.SizeOf<ZSTD_CCtx>(), Is.EqualTo(sizeof(ZSTD_CCtx)));
        }

        /// <summary>Validates that the <see cref="ZSTD_CCtx" /> struct has the right <see cref="LayoutKind" />.</summary>
        [Test]
        public static void IsLayoutSequentialTest()
        {
            Assert.That(typeof(ZSTD_CCtx).IsLayoutSequential, Is.True);
        }

        /// <summary>Validates that the <see cref="ZSTD_CCtx" /> struct has the correct size.</summary>
        [Test]
        public static void SizeOfTest()
        {
            Assert.That(sizeof(ZSTD_CCtx), Is.EqualTo(1));
        }
    }
}
