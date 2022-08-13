using JetBrains.Annotations;
using SevenZip;
using SevenZip.Compression.LZMA;

namespace InnoSetupReaderDotNet;

[PublicAPI]
public sealed class InnoSetupReader
{
    private readonly string _filePath;
    private readonly SetupLoaderOffsetTable _offsetTable;

    public InnoSetupReader(string filePath)
    {
        _filePath = filePath;
        
        using var loaderReader = new InnoSetupLoaderReader(_filePath);

        _offsetTable = loaderReader.GetOffsetTable();
    }

    public async Task GetSomething(CancellationToken cancellationToken)
    {
        await using var stream = GetStream();
        
        // seek to the file offset 0
        stream.Seek(_offsetTable.Offset0, SeekOrigin.Begin);

        // TODO: better name
        var buffer = new byte[64];

        if (await stream.ReadAsync(buffer.AsMemory(), cancellationToken) != buffer.Length)
            throw new InvalidOperationException();
        
        // TODO: Log this? Also do we need to interpret (u) for unicode
        var @string = System.Text.Encoding.ASCII.GetString(buffer).TrimEnd('\0');
        
        // read block header
        // crc32
        using var binaryReader = new BinaryReader(stream);
        var headerCrc32 = binaryReader.ReadBytes(4);
        var storedSize = binaryReader.ReadInt32();
        var compressed = binaryReader.ReadBoolean();

        var x = new Decoder();

        // TODO: better name, lzma properties?
        var decoderProperties = new byte[5];
        if (await stream.ReadAsync(decoderProperties.AsMemory(), cancellationToken) != decoderProperties.Length)
            throw new InvalidOperationException();

        x.SetDecoderProperties(decoderProperties);

        long outSize = 0;

        for (int i = 0; i < 8; i++)
        {
            int v = stream.ReadByte();
            if (v < 0)
                throw (new Exception("Can't Read 1"));
            outSize |= ((long)(byte)v) << (8 * i);
        }

        using var memoryStream = new MemoryStream();

        try
        {
            x.Code(stream, memoryStream, 1024, 2048, NullCodeProgress.Instance);
        }
        catch (Exception exception)
        {
            ;
        }
    }

    private Stream GetStream()
    {
        return new FileStream(_filePath,
            FileMode.Open, FileAccess.Read, FileShare.Read,
            bufferSize: 4096, useAsync: true);
    }

    private class NullCodeProgress : ICodeProgress
    {
        public void SetProgress(long inSize, long outSize)
        {
        }

        public static NullCodeProgress Instance = new();
    }
}