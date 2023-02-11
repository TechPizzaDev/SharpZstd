using NUnit.Framework;
using System;
using System.Runtime.InteropServices;

namespace SharpZstd.Interop.UnitTests
{
    /// <summary>Provides validation of the <see cref="ZSTD_outBuffer" /> struct.</summary>
    public static unsafe partial class ZSTD_outBufferTests
    {
        /// <summary>Validates that the <see cref="ZSTD_outBuffer" /> struct is blittable.</summary>
        [Test]
        public static void IsBlittableTest()
        {
            Assert.That(Marshal.SizeOf<ZSTD_outBuffer>(), Is.EqualTo(sizeof(ZSTD_outBuffer)));
        }

        /// <summary>Validates that the <see cref="ZSTD_outBuffer" /> struct has the right <see cref="LayoutKind" />.</summary>
        [Test]
        public static void IsLayoutSequentialTest()
        {
            Assert.That(typeof(ZSTD_outBuffer).IsLayoutSequential, Is.True);
        }

        /// <summary>Validates that the <see cref="ZSTD_outBuffer" /> struct has the correct size.</summary>
        [Test]
        public static void SizeOfTest()
        {
            if (Environment.Is64BitProcess)
            {
                Assert.That(sizeof(ZSTD_outBuffer), Is.EqualTo(24));
            }
            else
            {
                Assert.That(sizeof(ZSTD_outBuffer), Is.EqualTo(12));
            }
        }
    }
}
