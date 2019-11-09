using System;
using System.Drawing;
using GitExtUtils.GitUI;

namespace GitUI.Theming
{
    internal class TreeViewRenderer : ThemeRenderer
    {
        protected override string Clsid { get; } = "Treeview";

        public override int RenderBackground(IntPtr hdc, int partid, int stateid, Rectangle prect)
        {
            bool hot;
            bool expand;
            switch ((Parts)partid)
            {
                case Parts.TVP_GLYPH:
                    hot = false;
                    switch ((State.Glyph)stateid)
                    {
                        case State.Glyph.GLPS_CLOSED:
                            expand = true;
                            break;

                        // case State.Glyph.GLPS_OPENED:
                        default:
                            expand = false;
                            break;
                    }

                    break;

                case Parts.TVP_HOTGLYPH:
                    hot = true;
                    switch ((State.HotGlyph)stateid)
                    {
                        case State.HotGlyph.HGLPS_CLOSED:
                            expand = true;
                            break;

                        // case State.HotGlyph.HGLPS_CLOSED:
                        default:
                            expand = false;
                            break;
                    }

                    break;

                default:
                    return 1;
            }

            RenderExpandCollapse(hdc, prect, expand, hot);
            return 0;
        }

        private static void RenderExpandCollapse(IntPtr hdc, Rectangle prect, bool expand, bool hot)
        {
            using (var g = Graphics.FromHdcInternal(hdc))
            {
                Point[] arrowPoints;

                if (expand)
                {
                    int w = prect.Width / 2;
                    int y1 = prect.Top;
                    int y3 = prect.Bottom - 1;
                    int y2 = (y1 + y3) / 2;

                    int x1 = prect.Left + ((prect.Width - w) / 2);
                    int x2 = x1 + w;

                    arrowPoints = new[]
                    {
                        new Point(x1, y1),
                        new Point(x2, y2),
                        new Point(x1, y3)
                    };
                }
                else
                {
                    int h = prect.Height / 2;
                    int x1 = prect.Left;
                    int x3 = prect.Right - 1;
                    int x2 = (x1 + x3) / 2;
                    int y1 = prect.Top + ((prect.Height - h) / 2);
                    int y2 = y1 + h;

                    arrowPoints = new[]
                    {
                        new Point(x1, y1),
                        new Point(x2, y2),
                        new Point(x3, y1)
                    };
                }

                g.FillRectangle(SystemBrushes.Window, prect);
                using (var forePen = new Pen(SystemColors.ControlDarkDark, DpiUtil.Scale(2)))
                {
                    g.DrawLines(forePen, arrowPoints);
                }
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
