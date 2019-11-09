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

        public virtual int RenderBackground(IntPtr hdc, int partid, int stateid, Rectangle prect)
        {
            return 1;
        }

        public virtual int RenderText(
            IntPtr htheme,
            IntPtr hdc,
            int partid, int stateid,
            string psztext, int cchtext,
            NativeMethods.DT dwtextflags,
            ref NativeMethods.RECT prect, ref NativeMethods.DTTOPTS poptions)
        {
            return 1;
        }

        public virtual int GetThemeColor(int ipartid, int istateid, int ipropid, out int pcolor)
        {
            pcolor = 0;
            return 1;
        }

        public void AddThemeData(IntPtr hwnd) =>
            _themeDataHandles.Add(NativeMethods.OpenThemeData(hwnd, Clsid));

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
