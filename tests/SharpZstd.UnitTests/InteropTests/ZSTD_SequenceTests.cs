using NUnit.Framework;
using System.Runtime.InteropServices;

namespace SharpZstd.Interop.UnitTests
{
    /// <summary>Provides validation of the <see cref="ZSTD_Sequence" /> struct.</summary>
    public static unsafe partial class ZSTD_SequenceTests
    {
        /// <summary>Validates that the <see cref="ZSTD_Sequence" /> struct is blittable.</summary>
        [Test]
        public static void IsBlittableTest()
        {
            Assert.That(Marshal.SizeOf<ZSTD_Sequence>(), Is.EqualTo(sizeof(ZSTD_Sequence)));
        }

        /// <summary>Validates that the <see cref="ZSTD_Sequence" /> struct has the right <see cref="LayoutKind" />.</summary>
        [Test]
        public static void IsLayoutSequentialTest()
        {
            Assert.That(typeof(ZSTD_Sequence).IsLayoutSequential, Is.True);
        }

        /// <summary>Validates that the <see cref="ZSTD_Sequence" /> struct has the correct size.</summary>
        [Test]
        public static void SizeOfTest()
        {
            Assert.That(sizeof(ZSTD_Sequence), Is.EqualTo(16));
        }
    }
}
