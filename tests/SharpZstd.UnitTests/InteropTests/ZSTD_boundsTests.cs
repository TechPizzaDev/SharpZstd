using NUnit.Framework;
using System;
using System.Runtime.InteropServices;

namespace SharpZstd.Interop.UnitTests
{
    /// <summary>Provides validation of the <see cref="ZSTD_bounds" /> struct.</summary>
    public static unsafe partial class ZSTD_boundsTests
    {
        /// <summary>Validates that the <see cref="ZSTD_bounds" /> struct is blittable.</summary>
        [Test]
        public static void IsBlittableTest()
        {
            Assert.That(Marshal.SizeOf<ZSTD_bounds>(), Is.EqualTo(sizeof(ZSTD_bounds)));
        }

        /// <summary>Validates that the <see cref="ZSTD_bounds" /> struct has the right <see cref="LayoutKind" />.</summary>
        [Test]
        public static void IsLayoutSequentialTest()
        {
            Assert.That(typeof(ZSTD_bounds).IsLayoutSequential, Is.True);
        }

        /// <summary>Validates that the <see cref="ZSTD_bounds" /> struct has the correct size.</summary>
        [Test]
        public static void SizeOfTest()
        {
            if (Environment.Is64BitProcess)
            {
                Assert.That(sizeof(ZSTD_bounds), Is.EqualTo(16));
            }
            else
            {
                Assert.That(sizeof(ZSTD_bounds), Is.EqualTo(12));
            }
        }
    }
}
