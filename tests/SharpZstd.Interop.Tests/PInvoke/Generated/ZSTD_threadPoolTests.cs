using NUnit.Framework;
using System.Runtime.InteropServices;

namespace SharpZstd.Interop.UnitTests
{
    /// <summary>Provides validation of the <see cref="ZSTD_threadPool" /> struct.</summary>
    public static unsafe partial class ZSTD_threadPoolTests
    {
        /// <summary>Validates that the <see cref="ZSTD_threadPool" /> struct is blittable.</summary>
        [Test]
        public static void IsBlittableTest()
        {
            Assert.That(Marshal.SizeOf<ZSTD_threadPool>(), Is.EqualTo(sizeof(ZSTD_threadPool)));
        }

        /// <summary>Validates that the <see cref="ZSTD_threadPool" /> struct has the right <see cref="LayoutKind" />.</summary>
        [Test]
        public static void IsLayoutSequentialTest()
        {
            Assert.That(typeof(ZSTD_threadPool).IsLayoutSequential, Is.True);
        }

        /// <summary>Validates that the <see cref="ZSTD_threadPool" /> struct has the correct size.</summary>
        [Test]
        public static void SizeOfTest()
        {
            Assert.That(sizeof(ZSTD_threadPool), Is.EqualTo(1));
        }
    }
}
