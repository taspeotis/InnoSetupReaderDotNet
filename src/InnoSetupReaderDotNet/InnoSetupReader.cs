using System.ComponentModel;
using JetBrains.Annotations;

namespace InnoSetupReaderDotNet;

[PublicAPI]
public sealed class InnoSetupReader : IDisposable
{
    private readonly IntPtr _module;

    public InnoSetupReader(string filePath)
    {
        _module = NativeMethods.LoadLibraryEx(filePath, default, LoadLibraryExFlags.LOAD_LIBRARY_AS_DATAFILE);

        if (_module == default)
            throw new Win32Exception();
    }

    // GetInnoSetupReader();
    // find the resource - 11111 - rt_rcdata
    // sizeof resource - check the right size, load resource, lock resource

    void IDisposable.Dispose()
    {
        if (!NativeMethods.FreeLibrary(_module))
            throw new Win32Exception();
    }
}