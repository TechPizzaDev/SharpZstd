using NUnit.Framework;
using SharpZstd.Interop;

namespace SharpZstd.Tests;

public unsafe class Tests
{
    [Test]
    public void VersionTest()
    {
        uint version = Zstd.ZSTD_versionNumber();
        Assert.That(version, Is.EqualTo(10506));
    }
}