using NUnit.Framework;
using System.Runtime.InteropServices;

namespace SharpZstd.Interop.UnitTests
{
    /// <summary>Provides validation of the <see cref="ZSTD_parameters" /> struct.</summary>
    public static unsafe partial class ZSTD_parametersTests
    {
        /// <summary>Validates that the <see cref="ZSTD_parameters" /> struct is blittable.</summary>
        [Test]
        public static void IsBlittableTest()
        {
            Assert.That(Marshal.SizeOf<ZSTD_parameters>(), Is.EqualTo(sizeof(ZSTD_parameters)));
        }

        /// <summary>Validates that the <see cref="ZSTD_parameters" /> struct has the right <see cref="LayoutKind" />.</summary>
        [Test]
        public static void IsLayoutSequentialTest()
        {
            Assert.That(typeof(ZSTD_parameters).IsLayoutSequential, Is.True);
        }

        /// <summary>Validates that the <see cref="ZSTD_parameters" /> struct has the correct size.</summary>
        [Test]
        public static void SizeOfTest()
        {
            Assert.That(sizeof(ZSTD_parameters), Is.EqualTo(40));
        }
    }
}
