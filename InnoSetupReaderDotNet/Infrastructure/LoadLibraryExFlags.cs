using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace InnoSetupReaderDotNet.Infrastructure;

/// <see href="https://docs.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-loadlibraryexw" />
[SuppressMessage("ReSharper", "InconsistentNaming")]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
internal enum LoadLibraryExFlags
{
    LOAD_LIBRARY_AS_DATAFILE = 0x00000002
}