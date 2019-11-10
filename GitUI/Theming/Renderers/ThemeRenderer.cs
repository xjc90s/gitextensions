using System;
using System.Collections.Generic;
using System.Drawing;

namespace GitUI.Theming
{
    internal abstract class ThemeRenderer : IDisposable
    {
        private readonly HashSet<IntPtr> _themeDataHandles = new HashSet<IntPtr>();

        protected ThemeRenderer()
        {
            AddThemeData(IntPtr.Zero);
        }

        protected abstract string Clsid { get; }

        public virtual bool ForceUseRenderTextEx => false;

        public virtual int RenderBackground(IntPtr hdc, int partid, int stateid, Rectangle prect,
            ref NativeMethods.RECT pcliprect)
        {
            return 1;
        }

        public virtual int RenderText(IntPtr htheme,
            IntPtr hdc,
            int partid, int stateid,
            string psztext, int cchtext,
            NativeMethods.DT dwtextflags,
            int dwtextflags2,
            NativeMethods.RECT prect)
        {
            return 1;
        }

        public virtual int RenderTextEx(IntPtr htheme,
            IntPtr hdc,
            int partid, int stateid,
            string psztext, int cchtext,
            NativeMethods.DT dwtextflags,
            NativeMethods.RECT prect, ref NativeMethods.DTTOPTS poptions)
        {
            return 1;
        }

        public virtual int GetThemeColor(int ipartid, int istateid, int ipropid, out int pcolor)
        {
            pcolor = 0;
            return 1;
        }

        /// <summary>
        /// By using this method we find which theme data handle corresponds to a given CLSID e.g.
        /// "SCROLLBAR". The result depends on window class, e.g. ListView or NativeListView will
        /// have different theme data.
        /// </summary>
        /// <param name="hwnd">win32 window handle</param>
        public void AddThemeData(IntPtr hwnd)
        {
            var htheme = NativeMethods.OpenThemeData(hwnd, Clsid);
            _themeDataHandles.Add(htheme);
        }

        public bool Supports(IntPtr htheme) =>
            _themeDataHandles.Contains(htheme);

        public void Dispose()
        {
            foreach (var htheme in _themeDataHandles)
            {
                NativeMethods.CloseThemeData(htheme);
            }
        }
    }
}
