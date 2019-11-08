using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        private static HashSet<IntPtr> _scrollThemeHandles;
        private static HashSet<IntPtr> _headerThemeHandles;
        private static HashSet<IntPtr> _listViewThemeHandles;
        private static HashSet<IntPtr> _spinThemeHandles;
        private static HashSet<IntPtr> _editThemeHandles;

        public static event Action<IntPtr> WindowCreated;

        private static bool _showingMessageBox;

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        private delegate int ColorDelegate(int nindex);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        private delegate IntPtr BrushDelegate(int nindex);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true,
            CharSet = CharSet.Ansi)]
        private delegate int
            MessageBoxADelegate(IntPtr hwnd, string test, string caption, int type);

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

            const string scrollClsid = "Scrollbar";
            const string listViewClsid = "Listview";
            const string headerClsid = "Header";
            const string spinClsid = "Spin";
            const string editClsid = "Edit";

            _scrollThemeHandles = new HashSet<IntPtr>
            {
                NativeMethods.OpenThemeData(IntPtr.Zero, scrollClsid)
            };

            _headerThemeHandles = new HashSet<IntPtr>
            {
                NativeMethods.OpenThemeData(IntPtr.Zero, headerClsid)
            };

            _listViewThemeHandles = new HashSet<IntPtr>
            {
                NativeMethods.OpenThemeData(IntPtr.Zero, listViewClsid)
            };

            _spinThemeHandles = new HashSet<IntPtr>
            {
                NativeMethods.OpenThemeData(IntPtr.Zero, spinClsid)
            };

            _editThemeHandles = new HashSet<IntPtr>
            {
                NativeMethods.OpenThemeData(IntPtr.Zero, editClsid)
            };

            var editorHandle = new ICSharpCode.TextEditor.TextEditorControl().Handle;
            _scrollThemeHandles.Add(NativeMethods.OpenThemeData(editorHandle, scrollClsid));

            var listViewHandle = new NativeListView().Handle;
            _scrollThemeHandles.Add(NativeMethods.OpenThemeData(listViewHandle, scrollClsid));
            _headerThemeHandles.Add(NativeMethods.OpenThemeData(listViewHandle, headerClsid));
            _listViewThemeHandles.Add(NativeMethods.OpenThemeData(listViewHandle, listViewClsid));
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

            foreach (IntPtr htheme in (
                _scrollThemeHandles ?? Enumerable.Empty<IntPtr>()).Concat(
                _headerThemeHandles ?? Enumerable.Empty<IntPtr>()).Concat(
                _listViewThemeHandles ?? Enumerable.Empty<IntPtr>()).Concat(
                _spinThemeHandles ?? Enumerable.Empty<IntPtr>()).Concat(
                _editThemeHandles ?? Enumerable.Empty<IntPtr>()))
            {
                NativeMethods.CloseThemeData(htheme);
            }
        }

        private static (LocalHook, TDelegate) InstallHook<TDelegate>(string dll, string method,
            TDelegate hookImpl)
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
                if (_headerThemeHandles.Contains(htheme))
                {
                    if (HeaderRenderer.RenderHeader(hdc, partid, stateid, prect) == 0)
                    {
                        return 0;
                    }
                }
                else if (_spinThemeHandles.Contains(htheme))
                {
                    if (SpinRenderer.RenderSpin(hdc, partid, stateid, prect) == 0)
                    {
                        return 0;
                    }
                }
                else if (_editThemeHandles.Contains(htheme))
                {
                    if (EditRenderer.RenderEdit(hdc, partid, stateid, prect) == 0)
                    {
                        return 0;
                    }
                }
                else if (_scrollThemeHandles.Contains(htheme))
                {
                    ScrollBarRenderer.RenderScrollBar(hdc, partid, stateid, prect);
                    return 0;
                }
            }

            int result = _drawThemeBackgroundBypass(htheme, hdc, partid, stateid, ref prect, ref pcliprect);
            return result;
        }

        private static int GetThemeColorHook(IntPtr htheme, int ipartid, int istateid, int ipropid,
            out int pcolor)
        {
            if (!AppSettings.UseSystemVisualStyle)
            {
                if (_scrollThemeHandles.Contains(htheme))
                {
                    if (ScrollBarRenderer.GetThemeColor(ipartid, istateid, ipropid, out pcolor) ==
                        0)
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
            if (_listViewThemeHandles.Contains(htheme))
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
