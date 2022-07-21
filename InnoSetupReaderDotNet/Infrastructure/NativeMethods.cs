using System.Runtime.InteropServices;

namespace InnoSetupReaderDotNet.Infrastructure;

internal static class NativeMethods
{
    private const string Kernel32 = "kernel32.dll";

    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-loadlibraryexw" />
    [DllImport(Kernel32, SetLastError = true)]
    public static extern IntPtr LoadLibraryEx(string fileName, IntPtr _, LoadLibraryExFlags flags);

    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-freelibrary" />
    [DllImport(Kernel32, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool FreeLibrary(IntPtr module);
}