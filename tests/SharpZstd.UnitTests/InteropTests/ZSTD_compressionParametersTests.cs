using NUnit.Framework;
using System.Runtime.InteropServices;

namespace SharpZstd.Interop.UnitTests
{
    /// <summary>Provides validation of the <see cref="ZSTD_compressionParameters" /> struct.</summary>
    public static unsafe partial class ZSTD_compressionParametersTests
    {
        /// <summary>Validates that the <see cref="ZSTD_compressionParameters" /> struct is blittable.</summary>
        [Test]
        public static void IsBlittableTest()
        {
            Assert.That(Marshal.SizeOf<ZSTD_compressionParameters>(), Is.EqualTo(sizeof(ZSTD_compressionParameters)));
        }

        /// <summary>Validates that the <see cref="ZSTD_compressionParameters" /> struct has the right <see cref="LayoutKind" />.</summary>
        [Test]
        public static void IsLayoutSequentialTest()
        {
            Assert.That(typeof(ZSTD_compressionParameters).IsLayoutSequential, Is.True);
        }

        /// <summary>Validates that the <see cref="ZSTD_compressionParameters" /> struct has the correct size.</summary>
        [Test]
        public static void SizeOfTest()
        {
            Assert.That(sizeof(ZSTD_compressionParameters), Is.EqualTo(28));
        }
    }
}
