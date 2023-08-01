using System.Runtime.InteropServices;

namespace Savanna.ConsoleApp
{
    public static class ConsoleUtils
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        private static extern bool GetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool bMaximumWindow, ref CONSOLE_FONT_INFO_EX lpConsoleCurrentFontEx);

        [DllImport("kernel32.dll")]
        private static extern bool SetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool bMaximumWindow, ref CONSOLE_FONT_INFO_EX lpConsoleCurrentFontEx);

        private const int SW_MAXIMIZE = 3;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct CONSOLE_FONT_INFO_EX
        {
            public int cbSize;
            public int nFont;
            public COORD dwFontSize;
            public int FontFamily;
            public int FontWeight;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string FaceName;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct COORD
        {
            public short X;
            public short Y;
        }

        private const int STD_OUTPUT_HANDLE = -11;
        private const int TMPF_TRUETYPE = 4;
        private const int LF_FACESIZE = 32;

        public static void MaximizeConsoleWindow()
        {
            IntPtr hWnd = GetConsoleWindow();
            if (hWnd != IntPtr.Zero)
            {
                ShowWindow(hWnd, SW_MAXIMIZE);
            }
        }

        public static void SetConsoleFontSize(int fontSize)
        {
            IntPtr hConsoleOutput = GetStdHandle(STD_OUTPUT_HANDLE);
            CONSOLE_FONT_INFO_EX consoleFontInfo = new CONSOLE_FONT_INFO_EX();
            consoleFontInfo.cbSize = Marshal.SizeOf(consoleFontInfo);
            bool success = GetCurrentConsoleFontEx(hConsoleOutput, false, ref consoleFontInfo);

            if (!success)
            {
                throw new InvalidOperationException("Failed to get console font information.");
            }

            consoleFontInfo.dwFontSize = new COORD { X = 0, Y = (short)fontSize };
            consoleFontInfo.FontFamily = TMPF_TRUETYPE;
            consoleFontInfo.FaceName = "Consolas";

            success = SetCurrentConsoleFontEx(hConsoleOutput, false, ref consoleFontInfo);

            if (!success)
            {
                throw new InvalidOperationException("Failed to set console font size.");
            }
        }
    }
}
