using System.Runtime.InteropServices;

namespace InnoSetupReaderDotNet;

internal static class NativeMethods
{
    private const string Kernel32 = "kernel32.dll";

    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-findresourcew" />
    [DllImport(Kernel32, SetLastError = true)]
    public static extern IntPtr FindResource(IntPtr moduleHandle, IntPtr name, FindResourceType type);

    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-freelibrary" />
    [DllImport(Kernel32, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool FreeLibrary(IntPtr moduleHandle);

    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-loadlibraryexw" />
    [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern IntPtr LoadLibraryEx(string filePath, IntPtr _, LoadLibraryExFlags flags);
    
    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-lockresource" />
    [DllImport(Kernel32)]
    public static extern IntPtr LockResource(IntPtr resourceHandle);

    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-loadresource" />
    [DllImport(Kernel32, SetLastError = true)]
    public static extern IntPtr LoadResource(IntPtr moduleHandle, IntPtr resourceHandle);

    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-sizeofresource" />
    [DllImport(Kernel32, SetLastError = true)]
    public static extern int SizeofResource(IntPtr moduleHandle, IntPtr resourceHandle);
}