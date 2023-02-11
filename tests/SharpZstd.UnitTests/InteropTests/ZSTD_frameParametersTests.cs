using NUnit.Framework;
using System.Runtime.InteropServices;

namespace SharpZstd.Interop.UnitTests
{
    /// <summary>Provides validation of the <see cref="ZSTD_frameParameters" /> struct.</summary>
    public static unsafe partial class ZSTD_frameParametersTests
    {
        /// <summary>Validates that the <see cref="ZSTD_frameParameters" /> struct is blittable.</summary>
        [Test]
        public static void IsBlittableTest()
        {
            Assert.That(Marshal.SizeOf<ZSTD_frameParameters>(), Is.EqualTo(sizeof(ZSTD_frameParameters)));
        }

        /// <summary>Validates that the <see cref="ZSTD_frameParameters" /> struct has the right <see cref="LayoutKind" />.</summary>
        [Test]
        public static void IsLayoutSequentialTest()
        {
            Assert.That(typeof(ZSTD_frameParameters).IsLayoutSequential, Is.True);
        }

        /// <summary>Validates that the <see cref="ZSTD_frameParameters" /> struct has the correct size.</summary>
        [Test]
        public static void SizeOfTest()
        {
            Assert.That(sizeof(ZSTD_frameParameters), Is.EqualTo(12));
        }
    }
}
