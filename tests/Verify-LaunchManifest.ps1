param([string]$BuildDirectory = (Join-Path $PSScriptRoot '..\Skrinshoter.Decompiled\bin\Release\net48'))
$ErrorActionPreference = 'Stop'

# Inspect the embedded PE resource, not only the source manifest: packaging must
# ship executables that CreateProcess can start from a per-user installation.
if (-not ('LaunchManifestResources' -as [type])) {
    Add-Type -TypeDefinition @"
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
public static class LaunchManifestResources {
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr LoadLibraryEx(string path, IntPtr file, uint flags);
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr FindResource(IntPtr module, IntPtr name, IntPtr type);
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern uint SizeofResource(IntPtr module, IntPtr resource);
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr LoadResource(IntPtr module, IntPtr resource);
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr LockResource(IntPtr resource);
    [DllImport("kernel32.dll")]
    private static extern bool FreeLibrary(IntPtr module);
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct StartupInfo {
        public int cb;
        public string reserved, desktop, title;
        public uint x, y, width, height, charsX, charsY, fill, flags;
        public ushort show, reservedSize;
        public IntPtr reservedData, input, output, error;
    }
    [StructLayout(LayoutKind.Sequential)]
    private struct ProcessInfo {
        public IntPtr process, thread;
        public uint processId, threadId;
    }
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern bool CreateProcess(string app, string command, IntPtr processAttributes,
        IntPtr threadAttributes, bool inheritHandles, uint flags, IntPtr environment,
        string directory, ref StartupInfo startup, out ProcessInfo process);
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool TerminateProcess(IntPtr process, uint code);
    [DllImport("kernel32.dll")]
    private static extern uint WaitForSingleObject(IntPtr handle, uint milliseconds);
    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr handle);
    public static void VerifyCreateProcess(string path) {
        var startup = new StartupInfo { cb = Marshal.SizeOf(typeof(StartupInfo)) };
        ProcessInfo process;
        // CREATE_SUSPENDED: validate the installer's launch API without running
        // application code, opening windows or touching the user's profile.
        if (!CreateProcess(path, null, IntPtr.Zero, IntPtr.Zero, false, 4,
            IntPtr.Zero, System.IO.Path.GetDirectoryName(path), ref startup, out process))
            throw new Win32Exception(Marshal.GetLastWin32Error(), "CreateProcess failed for " + path);
        try {
            if (!TerminateProcess(process.process, 0)) throw new Win32Exception(Marshal.GetLastWin32Error());
            if (WaitForSingleObject(process.process, 10000) != 0)
                throw new TimeoutException("Suspended launch probe did not terminate.");
        } finally { CloseHandle(process.thread); CloseHandle(process.process); }
    }
    public static string Read(string path) {
        IntPtr module = LoadLibraryEx(path, IntPtr.Zero, 2);
        if (module == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error());
        try {
            IntPtr resource = FindResource(module, new IntPtr(1), new IntPtr(24));
            if (resource == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error());
            uint size = SizeofResource(module, resource);
            IntPtr data = LockResource(LoadResource(module, resource));
            if (data == IntPtr.Zero || size == 0) throw new InvalidOperationException("Empty executable manifest.");
            byte[] bytes = new byte[checked((int)size)];
            Marshal.Copy(data, bytes, 0, bytes.Length);
            return Encoding.UTF8.GetString(bytes).Trim('\uFEFF', '\0');
        } finally { FreeLibrary(module); }
    }
}
"@
}

foreach ($relativePath in @('Skrinshoter.exe', 'scrrec\scrrec.exe')) {
    $path = [IO.Path]::GetFullPath((Join-Path $BuildDirectory $relativePath))
    [xml]$manifest = [LaunchManifestResources]::Read($path)
    $level = $manifest.SelectSingleNode("//*[local-name()='requestedExecutionLevel']")
    if ($null -eq $level -or $level.GetAttribute('level') -ne 'asInvoker' -or $level.GetAttribute('uiAccess') -ne 'false') {
        throw "$relativePath must declare asInvoker/uiAccess=false for installation and startup without elevation."
    }
    [LaunchManifestResources]::VerifyCreateProcess($path)
}
Write-Host 'PASS: application and recorder embedded manifests and suspended CreateProcess launch probes.'
