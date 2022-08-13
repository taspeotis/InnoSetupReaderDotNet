using JetBrains.Annotations;

namespace InnoSetupReaderDotNet;

/// <see href="https://github.com/jrsoftware/issrc/blob/main/Projects/Struct.pas" />
[PublicAPI]
public sealed class SetupLoaderOffsetTable
{
    public SetupLoaderOffsetTable(byte[] buffer)
    {
        using var memoryStream = new MemoryStream(buffer);
        var binaryReader = new BinaryReader(memoryStream);

        Id = binaryReader.ReadBytes(12);
        Version = binaryReader.ReadInt32();
        TotalSize = binaryReader.ReadInt32();
        OffsetExe = binaryReader.ReadInt32();
        UncompressedSizeExe = binaryReader.ReadInt32();
        CrcExe = binaryReader.ReadBytes(4);
        Offset0 = binaryReader.ReadInt32();
        Offset1 = binaryReader.ReadInt32();
        TableCrc = binaryReader.ReadBytes(4);
    }
    
    /// <summary><c>SetupLdrOffsetTableID</c></summary>
    public byte[] Id { get; }
    
    /// <summary><c>SetupLdrOffsetTableVersion</c></summary>
    public int Version { get; }
    
    /// <summary>"Minimum expected size of setup.exe"</summary>
    public int TotalSize { get; }
    
    /// <summary>"Offset of compressed setup.e32"</summary>
    public int OffsetExe { get; }
    
    /// <summary>"Size of setup.e32 before compression"</summary>
    public int UncompressedSizeExe { get; }
    
    /// <summary>"CRC of setup.e32 before compression"</summary>
    public IReadOnlyCollection<byte> CrcExe { get; }
    
    /// <summary>"Offset of embedded setup-0.bin data"</summary>
    public int Offset0 { get; }
    
    /// <summary>"Offset of embedded setup-1.bin data, or 0 when DiskSpanning=yes"</summary>
    public int Offset1 { get; }
    
    /// <summary>"CRC of all prior fields in this record"</summary>
    public IReadOnlyCollection<byte> TableCrc { get; }
}