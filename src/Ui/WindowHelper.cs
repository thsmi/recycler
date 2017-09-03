using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace Recycler.Ui
{
    public class WindowHelper
    {
        internal static class NativeMethods
        {
            public const int GWL_EXSTYLE = -20;
            public const int WS_EX_DLGMODALFRAME = 0x0001;
            public const int SWP_NOSIZE = 0x0001;
            public const int SWP_NOMOVE = 0x0002;
            public const int SWP_NOZORDER = 0x0004;
            public const int SWP_FRAMECHANGED = 0x0020;
            public const uint WM_SETICON = 0x0080;

            [DllImport("user32.dll")]
            public static extern int GetWindowLong(IntPtr hwnd, int index);

            [DllImport("user32.dll")]
            public static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

            [DllImport("user32.dll")]
            public static extern bool SetWindowPos(
                IntPtr hwnd, 
                IntPtr hwndInsertAfter,
                int x, int y, int width, int height, 
                uint flags);

            [DllImport("user32.dll")]
            public static extern IntPtr SendMessage(
                IntPtr hwnd, 
                uint msg,
                IntPtr wParam, IntPtr lParam);

        }

        public void RemoveIcon(Window window)
        {
            IntPtr hwnd = new WindowInteropHelper(window).Handle;

            // Change the extended window style to not show a window icon
            int extendedStyle = NativeMethods.GetWindowLong(hwnd, NativeMethods.GWL_EXSTYLE);
            NativeMethods.SetWindowLong(hwnd, NativeMethods.GWL_EXSTYLE, extendedStyle | NativeMethods.WS_EX_DLGMODALFRAME);

            // Update the window's non-client area to reflect the changes
            NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, NativeMethods.SWP_NOMOVE |
                  NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOZORDER | NativeMethods.SWP_FRAMECHANGED);
        }
    }
}
