using Xunit;

namespace InnoSetupReaderDotNet.UnitTests;

public sealed class InnoSetupReaderTests
{
    [Fact]
    public async Task Whatever()
    {
        var loader = new InnoSetupReader("C:/Temp/Setup.exe");

        await loader.GetSomething(default);
    }
}