using System.ComponentModel;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace InnoSetupReaderDotNet;

// TODO: Is this really InnoSetupLoaderReader?
[PublicAPI]
public sealed class InnoSetupReader : IDisposable
{
    private static readonly IntPtr OffsetTableResourceName = new(11111);

    private readonly IntPtr _moduleHandle;

    public InnoSetupReader(string filePath)
    {
        _moduleHandle = NativeMethods.LoadLibraryEx(filePath, default, LoadLibraryExFlags.LOAD_LIBRARY_AS_DATAFILE);

        if (_moduleHandle == default)
            throw new Win32Exception();
    }

    public bool GetHeader()
    {
        var resourceHandle = NativeMethods.FindResource(
            _moduleHandle, OffsetTableResourceName, FindResourceType.RT_RCDATA);

        if (resourceHandle != default)
        {
            var resourceSize = NativeMethods.SizeofResource(_moduleHandle, resourceHandle);

            if (resourceSize != default)
            {
                resourceHandle = NativeMethods.LoadResource(_moduleHandle, resourceHandle);

                if (resourceHandle != default)
                {
                    var resourceData = NativeMethods.LockResource(resourceHandle);

                    if (resourceData == default)
                        throw new InvalidOperationException();

                    var buffer = new byte[resourceSize];
                    Marshal.Copy(resourceData, buffer, 0, resourceSize);

                    var offsetTable = new SetupLoaderOffsetTable(buffer);
                }
            }

            return resourceHandle != default;
        }

        throw new Win32Exception();
    }

    // GetInnoSetupReader();
    // find the resource - 11111 - rt_rcdata
    // sizeof resource - check the right size, load resource, lock resource

    void IDisposable.Dispose()
    {
        if (!NativeMethods.FreeLibrary(_moduleHandle))
            throw new Win32Exception();
    }
}