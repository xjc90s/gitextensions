using System;
using System.Drawing;
using GitExtUtils.GitUI;

namespace GitUI.Theming
{
    internal class TreeViewRenderer : ThemeRenderer
    {
        protected override string Clsid { get; } = "Treeview";

        public override int RenderBackground(IntPtr hdc, int partid, int stateid, Rectangle prect,
            ref NativeMethods.RECT pcliprect)
        {
            switch ((Parts)partid)
            {
                case Parts.TVP_GLYPH:
                    switch ((State.Glyph)stateid)
                    {
                        case State.Glyph.GLPS_CLOSED:
                            RenderClosed(hdc, prect, SystemColors.ControlDarkDark);
                            return 0;

                        case State.Glyph.GLPS_OPENED:
                            RenderOpened(hdc, prect, SystemColors.ControlDarkDark);
                            return 0;

                        default:
                            return 1;
                    }

                case Parts.TVP_HOTGLYPH:
                    switch ((State.HotGlyph)stateid)
                    {
                        case State.HotGlyph.HGLPS_CLOSED:
                            RenderClosed(hdc, prect, SystemColors.HotTrack);
                            return 0;

                        case State.HotGlyph.HGLPS_OPENED:
                            RenderOpened(hdc, prect, SystemColors.HotTrack);
                            return 0;

                        default:
                            return 1;
                    }

                default:
                    return 1;
            }
        }

        public override int RenderTextEx(IntPtr htheme, IntPtr hdc, int partid, int stateid,
            string psztext, int cchtext,
            NativeMethods.DT dwtextflags, NativeMethods.RECT prect,
            ref NativeMethods.DTTOPTS poptions)
        {
            switch ((Parts)partid)
            {
                case Parts.TVP_TREEITEM:
                {
                    Color foreColor;
                    switch ((State.Item)stateid)
                    {
                        case State.Item.TREIS_DISABLED:
                            foreColor = SystemColors.GrayText;
                            break;

                        case State.Item.TREIS_SELECTED:
                        case State.Item.TREIS_HOTSELECTED:
                        case State.Item.TREIS_SELECTEDNOTFOCUS:
                            foreColor = SystemColors.WindowText;
                            break;

                        case State.Item.TREIS_NORMAL:
                        case State.Item.TREIS_HOT:
                            foreColor = SystemColors.WindowText;
                            break;
                        default:
                            return 1;
                    }

                    // do not render, just modify text color
                    poptions.dwFlags |= NativeMethods.DTT.TextColor;
                    poptions.iColorPropId = 0;
                    poptions.crText = ColorTranslator.ToWin32(foreColor);
                    break;
                }
            }

            return 1;
        }

        private static void RenderClosed(IntPtr hdc, Rectangle prect, Color foreColor)
        {
            int w = prect.Width / 4;
            int h = w * 2;

            int y1 = prect.Top + ((prect.Height - h) / 2);
            int y2 = y1 + (h / 2);
            int y3 = y1 + h;

            int x1 = prect.Left + ((prect.Width - w) / 2);
            int x2 = x1 + w;

            var arrowPoints = new[]
            {
                new Point(x1, y1),
                new Point(x2, y2),
                new Point(x1, y3)
            };

            using (var g = Graphics.FromHdcInternal(hdc))
            using (new HighQualityScope(g))
            using (var forePen = new Pen(foreColor, DpiUtil.Scale(2)))
            {
                g.DrawLines(forePen, arrowPoints);
            }
        }

        private static void RenderOpened(IntPtr hdc, Rectangle prect, Color foreColor)
        {
            int h = prect.Height / 4;
            int w = h * 2;

            int x1 = prect.Left + ((prect.Width - w) / 2);
            int x2 = x1 + (w / 2);
            int x3 = x1 + w;

            int y1 = prect.Top + ((prect.Height - h) / 2);
            int y2 = y1 + h;

            var arrowPoints = new[]
            {
                new Point(x1, y1),
                new Point(x2, y2),
                new Point(x3, y1)
            };

            using (var g = Graphics.FromHdcInternal(hdc))
            using (new HighQualityScope(g))
            using (var forePen = new Pen(foreColor, DpiUtil.Scale(2)))
            {
                g.DrawLines(forePen, arrowPoints);
            }
        }

        private enum Parts
        {
            TVP_TREEITEM = 1,
            TVP_GLYPH = 2,
            TVP_BRANCH = 3,
            TVP_HOTGLYPH = 4,
        }

        private class State
        {
            public enum Item
            {
                TREIS_NORMAL = 1,
                TREIS_HOT = 2,
                TREIS_SELECTED = 3,
                TREIS_DISABLED = 4,
                TREIS_SELECTEDNOTFOCUS = 5,
                TREIS_HOTSELECTED = 6,
            }

            public enum Glyph
            {
                GLPS_CLOSED = 1,
                GLPS_OPENED = 2,
            }

            public enum HotGlyph
            {
                HGLPS_CLOSED = 1,
                HGLPS_OPENED = 2,
            }
        }
    }
}
