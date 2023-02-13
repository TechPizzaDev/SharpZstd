using NUnit.Framework;
using System;
using System.Runtime.InteropServices;

namespace SharpZstd.Interop.UnitTests
{
    /// <summary>Provides validation of the <see cref="ZSTD_customMem" /> struct.</summary>
    public static unsafe partial class ZSTD_customMemTests
    {
        /// <summary>Validates that the <see cref="ZSTD_customMem" /> struct is blittable.</summary>
        [Test]
        public static void IsBlittableTest()
        {
            Assert.That(Marshal.SizeOf<ZSTD_customMem>(), Is.EqualTo(sizeof(ZSTD_customMem)));
        }

        /// <summary>Validates that the <see cref="ZSTD_customMem" /> struct has the right <see cref="LayoutKind" />.</summary>
        [Test]
        public static void IsLayoutSequentialTest()
        {
            Assert.That(typeof(ZSTD_customMem).IsLayoutSequential, Is.True);
        }

        /// <summary>Validates that the <see cref="ZSTD_customMem" /> struct has the correct size.</summary>
        [Test]
        public static void SizeOfTest()
        {
            if (Environment.Is64BitProcess)
            {
                Assert.That(sizeof(ZSTD_customMem), Is.EqualTo(24));
            }
            else
            {
                Assert.That(sizeof(ZSTD_customMem), Is.EqualTo(12));
            }
        }
    }
}
