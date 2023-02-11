using NUnit.Framework;
using System.Runtime.InteropServices;

namespace SharpZstd.Interop.UnitTests
{
    /// <summary>Provides validation of the <see cref="ZSTD_frameProgression" /> struct.</summary>
    public static unsafe partial class ZSTD_frameProgressionTests
    {
        /// <summary>Validates that the <see cref="ZSTD_frameProgression" /> struct is blittable.</summary>
        [Test]
        public static void IsBlittableTest()
        {
            Assert.That(Marshal.SizeOf<ZSTD_frameProgression>(), Is.EqualTo(sizeof(ZSTD_frameProgression)));
        }

        /// <summary>Validates that the <see cref="ZSTD_frameProgression" /> struct has the right <see cref="LayoutKind" />.</summary>
        [Test]
        public static void IsLayoutSequentialTest()
        {
            Assert.That(typeof(ZSTD_frameProgression).IsLayoutSequential, Is.True);
        }

        /// <summary>Validates that the <see cref="ZSTD_frameProgression" /> struct has the correct size.</summary>
        [Test]
        public static void SizeOfTest()
        {
            Assert.That(sizeof(ZSTD_frameProgression), Is.EqualTo(40));
        }
    }
}
