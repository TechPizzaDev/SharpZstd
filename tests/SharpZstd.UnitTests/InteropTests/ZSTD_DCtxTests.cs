using NUnit.Framework;
using System.Runtime.InteropServices;

namespace SharpZstd.Interop.UnitTests
{
    /// <summary>Provides validation of the <see cref="ZSTD_DCtx" /> struct.</summary>
    public static unsafe partial class ZSTD_DCtxTests
    {
        /// <summary>Validates that the <see cref="ZSTD_DCtx" /> struct is blittable.</summary>
        [Test]
        public static void IsBlittableTest()
        {
            Assert.That(Marshal.SizeOf<ZSTD_DCtx>(), Is.EqualTo(sizeof(ZSTD_DCtx)));
        }

        /// <summary>Validates that the <see cref="ZSTD_DCtx" /> struct has the right <see cref="LayoutKind" />.</summary>
        [Test]
        public static void IsLayoutSequentialTest()
        {
            Assert.That(typeof(ZSTD_DCtx).IsLayoutSequential, Is.True);
        }

        /// <summary>Validates that the <see cref="ZSTD_DCtx" /> struct has the correct size.</summary>
        [Test]
        public static void SizeOfTest()
        {
            Assert.That(sizeof(ZSTD_DCtx), Is.EqualTo(1));
        }
    }
}
