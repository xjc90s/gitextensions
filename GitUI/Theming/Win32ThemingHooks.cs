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
        private static GetThemeColorDelegate _getThemeColorBypass;
        private static DrawThemeTextExDelegate _drawThemeTextExBypass;
        private static CreateWindowExDelegate _createWindowExBypass;

        private static LocalHook _colorHook;
        private static LocalHook _brushHook;
        private static LocalHook _messageBoxAHook;
        private static LocalHook _messageBoxWHook;
        private static LocalHook _drawThemeBackgroundHook;
        private static LocalHook _getThemeColorHook;
        private static LocalHook _drawThemeTextExHook;
        private static LocalHook _createWindowExHook;

        private static HashSet<IntPtr> _scrollBarThemeHandles;
        private static IntPtr _nativeListViewThemeHandle;

        public static event Action<IntPtr> WindowCreated;

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

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        private delegate int GetThemeColorDelegate(IntPtr htheme,
            int ipartid, int istateid, int ipropid,
            out int pcolor);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Unicode)]
        private delegate int DrawThemeTextExDelegate(
            IntPtr htheme, IntPtr hdc,
            int partid, int stateid,
            string pszText,
            int cchText,
            NativeMethods.DT dwtextflags,
            ref NativeMethods.RECT prect,
            ref NativeMethods.DTTOPTS poptions);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Unicode)]
        private delegate IntPtr CreateWindowExDelegate(
            int dwexstyle,
            IntPtr lpclassname,
            IntPtr lpwindowname,
            int dwstyle,
            int x,
            int y,
            int nwidth,
            int nheight,
            IntPtr hwndparent,
            IntPtr hmenu,
            IntPtr hinstance,
            IntPtr lpparam);

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

            (_getThemeColorHook, _getThemeColorBypass) =
                InstallHook<GetThemeColorDelegate>(
                    "uxtheme.dll",
                    "GetThemeColor",
                    GetThemeColorHook);

            (_drawThemeTextExHook, _drawThemeTextExBypass) =
                InstallHook<DrawThemeTextExDelegate>(
                    "uxtheme.dll",
                    "DrawThemeTextEx",
                    DrawThemeTextExHook);

            const string scrollbarClsid = "Scrollbar";
            const string listviewClsid = "Listview";

            _scrollBarThemeHandles = new HashSet<IntPtr>
            {
                NativeMethods.OpenThemeData(IntPtr.Zero, scrollbarClsid),
                NativeMethods.OpenThemeData(new ICSharpCode.TextEditor.TextEditorControl().Handle, scrollbarClsid)
            };

            var nativeListViewHandle = new NativeListView().Handle;
            _scrollBarThemeHandles.Add(NativeMethods.OpenThemeData(nativeListViewHandle, scrollbarClsid));
            _nativeListViewThemeHandle = NativeMethods.OpenThemeData(nativeListViewHandle, listviewClsid);
        }

        public static void InstallCreateWindowHook()
        {
            (_createWindowExHook, _createWindowExBypass) =
                InstallHook<CreateWindowExDelegate>(
                    "user32.dll",
                    "CreateWindowExW",
                    CreateWindowExHook);
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
            _getThemeColorHook?.Dispose();
            _drawThemeTextExHook?.Dispose();
            _createWindowExHook?.Dispose();

            if (_scrollBarThemeHandles != null)
            {
                foreach (IntPtr htheme in _scrollBarThemeHandles)
                {
                    NativeMethods.CloseThemeData(htheme);
                }
            }

            if (_nativeListViewThemeHandle != IntPtr.Zero)
            {
                NativeMethods.CloseThemeData(_nativeListViewThemeHandle);
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
            int partid, int stateid,
            ref NativeMethods.RECT prect, ref NativeMethods.RECT pcliprect)
        {
            if (!AppSettings.UseSystemVisualStyle)
            {
                if (_scrollBarThemeHandles.Contains(htheme))
                {
                    ScrollBarRenderer.RenderScrollBar(hdc, partid, stateid, prect);
                    return 0;
                }
            }

            int result = _drawThemeBackgroundBypass(htheme, hdc, partid, stateid, ref prect, ref pcliprect);
            return result;
        }

        private static int GetThemeColorHook(IntPtr htheme, int ipartid, int istateid, int ipropid, out int pcolor)
        {
            if (!AppSettings.UseSystemVisualStyle)
            {
                if (_scrollBarThemeHandles.Contains(htheme))
                {
                    if (ScrollBarRenderer.GetThemeColor(ipartid, istateid, ipropid, out pcolor) == 0)
                    {
                        return 0;
                    }
                }
            }

            return _getThemeColorBypass(htheme, ipartid, istateid, ipropid, out pcolor);
        }

        private static int DrawThemeTextExHook(
            IntPtr htheme, IntPtr hdc,
            int partid, int stateid,
            string psztext, int cchtext,
            NativeMethods.DT dwtextflags,
            ref NativeMethods.RECT prect, ref NativeMethods.DTTOPTS poptions)
        {
            if (NativeListViewRenderer.RenderListView(
                htheme, hdc,
                partid, stateid,
                psztext, cchtext,
                dwtextflags,
                ref prect, ref poptions) == 0)
            {
                return 0;
            }

            return _drawThemeTextExBypass(
                htheme, hdc,
                partid, stateid,
                psztext, cchtext,
                dwtextflags,
                ref prect, ref poptions);
        }

        private static IntPtr CreateWindowExHook(
            int dwexstyle, IntPtr lpclassname, IntPtr lpwindowname, int dwstyle,
            int x, int y, int nwidth, int nheight,
            IntPtr hwndparent, IntPtr hmenu, IntPtr hinstance, IntPtr lpparam)
        {
            var hwnd = _createWindowExBypass(
                dwexstyle, lpclassname, lpwindowname, dwstyle,
                x, y, nwidth, nheight,
                hwndparent, hmenu, hinstance, lpparam);
            WindowCreated?.Invoke(hwnd);
            return hwnd;
        }
    }
}
