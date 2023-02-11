using NUnit.Framework;
using System.Runtime.InteropServices;

namespace SharpZstd.Interop.UnitTests
{
    /// <summary>Provides validation of the <see cref="ZDICT_params_t" /> struct.</summary>
    public static unsafe partial class ZDICT_params_tTests
    {
        /// <summary>Validates that the <see cref="ZDICT_params_t" /> struct is blittable.</summary>
        [Test]
        public static void IsBlittableTest()
        {
            Assert.That(Marshal.SizeOf<ZDICT_params_t>(), Is.EqualTo(sizeof(ZDICT_params_t)));
        }

        /// <summary>Validates that the <see cref="ZDICT_params_t" /> struct has the right <see cref="LayoutKind" />.</summary>
        [Test]
        public static void IsLayoutSequentialTest()
        {
            Assert.That(typeof(ZDICT_params_t).IsLayoutSequential, Is.True);
        }

        /// <summary>Validates that the <see cref="ZDICT_params_t" /> struct has the correct size.</summary>
        [Test]
        public static void SizeOfTest()
        {
            Assert.That(sizeof(ZDICT_params_t), Is.EqualTo(12));
        }
    }
}
