namespace InnoSetupReaderDotNet.UnitTests;

public class InnoSetupReaderTests
{
    [Fact]
    public void InnoSetupReader_Reads_InnoSetup()
    {
        using var reader = new InnoSetupReader("C:/Temp/Setup.exe");
        
        
    }
}