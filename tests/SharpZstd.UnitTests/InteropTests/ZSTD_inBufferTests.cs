using NUnit.Framework;
using System;
using System.Runtime.InteropServices;

namespace SharpZstd.Interop.UnitTests
{
    /// <summary>Provides validation of the <see cref="ZSTD_inBuffer" /> struct.</summary>
    public static unsafe partial class ZSTD_inBufferTests
    {
        /// <summary>Validates that the <see cref="ZSTD_inBuffer" /> struct is blittable.</summary>
        [Test]
        public static void IsBlittableTest()
        {
            Assert.That(Marshal.SizeOf<ZSTD_inBuffer>(), Is.EqualTo(sizeof(ZSTD_inBuffer)));
        }

        /// <summary>Validates that the <see cref="ZSTD_inBuffer" /> struct has the right <see cref="LayoutKind" />.</summary>
        [Test]
        public static void IsLayoutSequentialTest()
        {
            Assert.That(typeof(ZSTD_inBuffer).IsLayoutSequential, Is.True);
        }

        /// <summary>Validates that the <see cref="ZSTD_inBuffer" /> struct has the correct size.</summary>
        [Test]
        public static void SizeOfTest()
        {
            if (Environment.Is64BitProcess)
            {
                Assert.That(sizeof(ZSTD_inBuffer), Is.EqualTo(24));
            }
            else
            {
                Assert.That(sizeof(ZSTD_inBuffer), Is.EqualTo(12));
            }
        }
    }
}
