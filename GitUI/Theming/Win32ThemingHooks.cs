using System;
using System.Drawing;
using System.Runtime.InteropServices;
using EasyHook;
using GitExtUtils.GitUI.Theming;

namespace GitUI.Theming
{
    internal static class Win32ThemingHooks
    {
        private static Theme _theme;
        private static ColorDelegate _colorBypass;
        private static BrushDelegate _brushBypass;
        private static MessageBoxADelegate _messageBoxABypass;
        private static MessageBoxWDelegate _messageBoxWBypass;
        private static LocalHook _colorHook;
        private static LocalHook _brushHook;
        private static LocalHook _messageBoxAHook;
        private static LocalHook _messageBoxWHook;
        private static bool _showingMessageBox;

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        private delegate int ColorDelegate(int nIndex);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        private delegate IntPtr BrushDelegate(int nIndex);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Ansi)]
        private delegate int MessageBoxADelegate(IntPtr hwnd, string test, string caption, int type);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Unicode)]
        private delegate int MessageBoxWDelegate(IntPtr hwnd, string test, string caption, int type);

        public static void InstallColorHooks(Theme theme)
        {
            (_brushHook, _brushBypass) = InstallHook<BrushDelegate>(
                "user32.dll",
                "GetSysColorBrush",
                BrushHook);

            (_colorHook, _colorBypass) = InstallHook<ColorDelegate>(
                "user32.dll",
                "GetSysColor",
                ColorHook);

            _theme = theme;
        }

        public static void InstallMessageBoxHooks()
        {
            (_messageBoxAHook, _messageBoxABypass) = InstallHook<MessageBoxADelegate>(
                "user32.dll",
                "MessageBoxA",
                MessageBoxAHook);

            (_messageBoxWHook, _messageBoxWBypass) = InstallHook<MessageBoxWDelegate>(
                "user32.dll",
                "MessageBoxW",
                MessageBoxWHook);
        }

        public static void Uninstall()
        {
            _colorHook?.Dispose();
            _brushHook?.Dispose();
            _messageBoxAHook?.Dispose();
            _messageBoxWHook?.Dispose();
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CreateSolidBrush(int nIndex);

        private static (LocalHook, TDelegate) InstallHook<TDelegate>(string dll, string method, TDelegate hookImpl)
            where TDelegate : Delegate
        {
            var addr = LocalHook.GetProcAddress(dll, method);
            var original = Marshal.GetDelegateForFunctionPointer<TDelegate>(addr);
            var hook = LocalHook.Create(addr, hookImpl, null);

            try
            {
                hook.ThreadACL.SetExclusiveACL(new int[0]);
            }
            catch
            {
                hook.Dispose();
                throw;
            }

            return (hook, original);
        }

        private static int ColorHook(int nIndex)
        {
            if (_showingMessageBox)
            {
                return _colorBypass(nIndex);
            }

            var color = _theme.GetColor(GetKnownColor(nIndex));
            if (color == Color.Empty)
            {
                return _colorBypass(nIndex);
            }

            return ColorTranslator.ToWin32(color);
        }

        private static IntPtr BrushHook(int nIndex)
        {
            if (_showingMessageBox)
            {
                return _brushBypass(nIndex);
            }

            var color = _theme.GetColor(GetKnownColor(nIndex));
            if (color == Color.Empty)
            {
                return _brushBypass(nIndex);
            }

            return CreateSolidBrush(ColorTranslator.ToWin32(color));
        }

        private static int MessageBoxAHook(IntPtr hwnd, string text, string caption, int type)
        {
            _showingMessageBox = true;
            var result = _messageBoxABypass(hwnd, text, caption, type);
            _showingMessageBox = false;
            return result;
        }

        private static int MessageBoxWHook(IntPtr hwnd, string text, string caption, int type)
        {
            _showingMessageBox = true;
            var result = _messageBoxWBypass(hwnd, text, caption, type);
            _showingMessageBox = false;
            return result;
        }

        private static KnownColor GetKnownColor(int nIndex)
        {
            if ((nIndex & 0xffffff00) == 0)
            {
                nIndex |= -0x80000000;
                return ColorTranslator.FromOle(nIndex).ToKnownColor();
            }

            return ColorTranslator.FromWin32(nIndex).ToKnownColor();
        }
    }
}
