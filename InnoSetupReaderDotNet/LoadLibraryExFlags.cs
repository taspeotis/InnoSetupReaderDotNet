using System.Diagnostics.CodeAnalysis;

namespace InnoSetupReaderDotNet;

/// <see href="https://docs.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-loadlibraryexw" />
[SuppressMessage("ReSharper", "InconsistentNaming")]
internal enum LoadLibraryExFlags
{
    LOAD_LIBRARY_AS_DATAFILE = 0x00000002
}