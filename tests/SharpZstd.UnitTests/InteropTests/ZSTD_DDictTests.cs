using NUnit.Framework;
using System.Runtime.InteropServices;

namespace SharpZstd.Interop.UnitTests
{
    /// <summary>Provides validation of the <see cref="ZSTD_DDict" /> struct.</summary>
    public static unsafe partial class ZSTD_DDictTests
    {
        /// <summary>Validates that the <see cref="ZSTD_DDict" /> struct is blittable.</summary>
        [Test]
        public static void IsBlittableTest()
        {
            Assert.That(Marshal.SizeOf<ZSTD_DDict>(), Is.EqualTo(sizeof(ZSTD_DDict)));
        }

        /// <summary>Validates that the <see cref="ZSTD_DDict" /> struct has the right <see cref="LayoutKind" />.</summary>
        [Test]
        public static void IsLayoutSequentialTest()
        {
            Assert.That(typeof(ZSTD_DDict).IsLayoutSequential, Is.True);
        }

        /// <summary>Validates that the <see cref="ZSTD_DDict" /> struct has the correct size.</summary>
        [Test]
        public static void SizeOfTest()
        {
            Assert.That(sizeof(ZSTD_DDict), Is.EqualTo(1));
        }
    }
}
