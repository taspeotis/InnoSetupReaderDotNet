using Xunit;

namespace InnoSetupReaderDotNet.UnitTests;

public sealed class InnoSetupLoaderReaderTests
{
    [Fact]
    public void InnoSetupLoaderReader_Reads_InnoSetupLoader()
    {
        using var reader = new InnoSetupLoaderReader("C:/Temp/Setup.exe");

        reader.GetOffsetTable();
        
        // assert some stuff
    }
}