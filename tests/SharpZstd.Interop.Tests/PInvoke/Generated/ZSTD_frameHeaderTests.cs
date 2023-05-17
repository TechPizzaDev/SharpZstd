using NUnit.Framework;
using System.Runtime.InteropServices;

namespace SharpZstd.Interop.UnitTests
{
    /// <summary>Provides validation of the <see cref="ZSTD_frameHeader" /> struct.</summary>
    public static unsafe partial class ZSTD_frameHeaderTests
    {
        /// <summary>Validates that the <see cref="ZSTD_frameHeader" /> struct is blittable.</summary>
        [Test]
        public static void IsBlittableTest()
        {
            Assert.That(Marshal.SizeOf<ZSTD_frameHeader>(), Is.EqualTo(sizeof(ZSTD_frameHeader)));
        }

        /// <summary>Validates that the <see cref="ZSTD_frameHeader" /> struct has the right <see cref="LayoutKind" />.</summary>
        [Test]
        public static void IsLayoutSequentialTest()
        {
            Assert.That(typeof(ZSTD_frameHeader).IsLayoutSequential, Is.True);
        }

        /// <summary>Validates that the <see cref="ZSTD_frameHeader" /> struct has the correct size.</summary>
        [Test]
        public static void SizeOfTest()
        {
            Assert.That(sizeof(ZSTD_frameHeader), Is.EqualTo(48));
        }
    }
}
