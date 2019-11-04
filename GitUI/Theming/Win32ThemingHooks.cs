using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using EasyHook;
using GitCommands;
using GitExtUtils.GitUI.Theming;
using GitUI.UserControls;

namespace GitUI.Theming
{
    internal static class Win32ThemingHooks
    {
        private static Theme _theme;
        private static ColorDelegate _colorBypass;
        private static BrushDelegate _brushBypass;
        private static MessageBoxADelegate _messageBoxABypass;
        private static MessageBoxWDelegate _messageBoxWBypass;
        private static DrawThemeBackgroundDelegate _drawThemeBackgroundBypass;

        private static LocalHook _colorHook;
        private static LocalHook _brushHook;
        private static LocalHook _messageBoxAHook;
        private static LocalHook _messageBoxWHook;
        private static LocalHook _drawThemeBackgroundHook;

        private static HashSet<IntPtr> _scrollBarThemeDataHandles;

        private static bool _showingMessageBox;

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        private delegate int ColorDelegate(int nindex);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        private delegate IntPtr BrushDelegate(int nindex);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Ansi)]
        private delegate int MessageBoxADelegate(IntPtr hwnd, string test, string caption, int type);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Unicode)]
        private delegate int MessageBoxWDelegate(IntPtr hwnd, string test, string caption, int type);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        private delegate int DrawThemeBackgroundDelegate(
            IntPtr htheme, IntPtr hdc,
            int partId, int stateId,
            ref NativeMethods.RECT prect, ref NativeMethods.RECT pcliprect);

        public static void InstallColorHooks(Theme theme)
        {
            _theme = theme;

            (_brushHook, _brushBypass) = InstallHook<BrushDelegate>(
                "user32.dll",
                "GetSysColorBrush",
                BrushHook);

            (_colorHook, _colorBypass) = InstallHook<ColorDelegate>(
                "user32.dll",
                "GetSysColor",
                ColorHook);

            (_drawThemeBackgroundHook, _drawThemeBackgroundBypass) =
                InstallHook<DrawThemeBackgroundDelegate>(
                    "uxtheme.dll",
                    "DrawThemeBackground",
                    DrawThemeBackgroundHook);

            const string scrollbarClsid = "Scrollbar";

            _scrollBarThemeDataHandles = new HashSet<IntPtr>
            {
                NativeMethods.OpenThemeData(IntPtr.Zero, scrollbarClsid),
                NativeMethods.OpenThemeData(new NativeListView().Handle, scrollbarClsid),
                NativeMethods.OpenThemeData(new ICSharpCode.TextEditor.TextEditorControl().Handle, scrollbarClsid)
            };
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
            _drawThemeBackgroundHook?.Dispose();

            if (_scrollBarThemeDataHandles != null)
            {
                foreach (IntPtr htheme in _scrollBarThemeDataHandles)
                {
                    NativeMethods.CloseThemeData(htheme);
                }
            }
        }

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

        private static int ColorHook(int nindex)
        {
            if (_showingMessageBox)
            {
                return _colorBypass(nindex);
            }

            var color = _theme.GetColor(Win32ColorTranslator.GetKnownColor(nindex));
            if (color == Color.Empty)
            {
                return _colorBypass(nindex);
            }

            return ColorTranslator.ToWin32(color);
        }

        private static IntPtr BrushHook(int nindex)
        {
            if (_showingMessageBox)
            {
                return _brushBypass(nindex);
            }

            var color = _theme.GetColor(Win32ColorTranslator.GetKnownColor(nindex));
            if (color == Color.Empty)
            {
                return _brushBypass(nindex);
            }

            return NativeMethods.CreateSolidBrush(ColorTranslator.ToWin32(color));
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

        private static int DrawThemeBackgroundHook(
            IntPtr htheme, IntPtr hdc,
            int partId, int stateId,
            ref NativeMethods.RECT prect, ref NativeMethods.RECT pcliprect)
        {
            if (!AppSettings.UseSystemVisualStyle)
            {
                if (_scrollBarThemeDataHandles.Contains(htheme))
                {
                    ScrollBarRenderer.RenderScrollBar(hdc, partId, stateId, prect);
                    return 0;
                }
            }

            int result = _drawThemeBackgroundBypass(htheme, hdc, partId, stateId, ref prect, ref pcliprect);
            return result;
        }
    }
}
