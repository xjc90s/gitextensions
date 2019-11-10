using System;
using System.Drawing;
using GitExtUtils.GitUI;

namespace GitUI.Theming
{
    internal class ComboBoxRenderer : ThemeRenderer
    {
        protected override string Clsid { get; } = "Combobox";

        public override int RenderBackground(IntPtr hdc, int partid, int stateid, Rectangle prect,
            ref NativeMethods.RECT pcliprect)
        {
            switch ((Parts)partid)
            {
                case Parts.CP_BACKGROUND:
                {
                    using (var g = Graphics.FromHdcInternal(hdc))
                    {
                        g.FillRectangle(SystemBrushes.Window, prect);
                    }

                    return 0;
                }

                case Parts.CP_BORDER:
                {
                    RenderBorder(hdc, stateid, prect);
                    return 0;
                }

                case Parts.CP_DROPDOWNBUTTON:
                case Parts.CP_DROPDOWNBUTTONRIGHT:
                case Parts.CP_DROPDOWNBUTTONLEFT:
                {
                    RenderDropDownButton(hdc, stateid, prect);
                    return 0;
                }

                case Parts.CP_READONLY:
                {
                    RenderReadonlyDropDown(hdc, stateid, prect);
                    return 0;
                }

                // case Parts.CP_TRANSPARENTBACKGROUND:
                // case Parts.CP_CUEBANNER:
            }

            return 1;
        }

        public override bool ForceUseRenderTextEx { get; } = true;

        public override int RenderTextEx(IntPtr htheme, IntPtr hdc, int partid, int stateid,
            string psztext, int cchtext, NativeMethods.DT dwtextflags,
            NativeMethods.RECT prect, ref NativeMethods.DTTOPTS poptions)
        {
            switch ((Parts)partid)
            {
                case Parts.CP_READONLY:
                    // do not render, just modify text color
                    poptions.dwFlags |= NativeMethods.DTT.TextColor;
                    poptions.iColorPropId = 0;
                    poptions.crText = ColorTranslator.ToWin32(SystemColors.ControlText);
                    break;
            }

            return 1;
        }

        public override int GetThemeColor(int ipartid, int istateid, int ipropid, out int pcolor)
        {
            pcolor = 0;
            return 1;
        }

        private static void RenderBorder(IntPtr hdc, int stateid, Rectangle prect)
        {
            using (var g = Graphics.FromHdcInternal(hdc))
            {
                Pen borderPen;
                switch ((State.Border)stateid)
                {
                    case State.Border.CBB_HOT:
                        borderPen = SystemPens.HotTrack;
                        break;

                    case State.Border.CBB_FOCUSED:
                        borderPen = SystemPens.Highlight;
                        break;

                    // case State.Border.CBB_DISABLED:
                    // case State.Border.CBB_NORMAL:
                    default:
                        borderPen = SystemPens.ControlDark;
                        break;
                }

                g.FillRectangle(SystemBrushes.Window, prect);
                g.DrawRectangle(borderPen, prect.Inclusive());
            }
        }

        private static void RenderDropDownButton(IntPtr hdc, int stateid, Rectangle prect)
        {
            using (var g = Graphics.FromHdcInternal(hdc))
            {
                var border = prect.Inclusive();
                switch ((State.DropDown)stateid)
                {
                    case State.DropDown.CBXS_HOT:
                        g.FillRectangle(SystemBrushes.Control, prect);
                        g.DrawRectangle(SystemPens.HotTrack, border);
                        break;

                    case State.DropDown.CBXS_PRESSED:
                        g.FillRectangle(SystemBrushes.Control, prect);
                        g.DrawRectangle(SystemPens.Highlight, border);
                        break;

                    // case State.DropDown.CBXS_DISABLED:
                    // case State.DropDown.CBXS_NORMAL:
                }

                RenderDownArrow(g, prect);
            }
        }

        private static void RenderReadonlyDropDown(IntPtr hdc, int stateid, Rectangle prect)
        {
            Pen borderPen;
            switch ((State.Readonly)stateid)
            {
                case State.Readonly.CBRO_HOT:
                    borderPen = SystemPens.HotTrack;
                    break;

                case State.Readonly.CBRO_PRESSED:
                    borderPen = SystemPens.Highlight;
                    break;

                // case State.Readonly.CBRO_DISABLED:
                // case State.Readonly.CBRO_NORMAL:
                default:
                    borderPen = SystemPens.ControlDark;
                    break;
            }

            using (var g = Graphics.FromHdcInternal(hdc))
            {
                g.FillRectangle(SystemBrushes.Control, prect);
                g.DrawRectangle(borderPen, prect.Inclusive());
            }
        }

        private static void RenderDownArrow(Graphics g, Rectangle prect)
        {
            int h = prect.Width / 4;
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

            using (new HighQualityScope(g))
            using (var pen = new Pen(SystemColors.ControlDarkDark, DpiUtil.Scale(2)))
            {
                g.DrawLines(pen, arrowPoints);
            }
        }

        private enum Parts
        {
            CP_DROPDOWNBUTTON = 1,
            CP_BACKGROUND = 2,
            CP_TRANSPARENTBACKGROUND = 3,
            CP_BORDER = 4,
            CP_READONLY = 5,
            CP_DROPDOWNBUTTONRIGHT = 6,
            CP_DROPDOWNBUTTONLEFT = 7,
            CP_CUEBANNER = 8,
        }

        private class State
        {
            public enum DropDown
            {
                CBXS_NORMAL = 1,
                CBXS_HOT = 2,
                CBXS_PRESSED = 3,
                CBXS_DISABLED = 4,
            }

            public enum DropDownRight
            {
                CBXSR_NORMAL = 1,
                CBXSR_HOT = 2,
                CBXSR_PRESSED = 3,
                CBXSR_DISABLED = 4,
            }

            public enum DropDownLeft
            {
                CBXSL_NORMAL = 1,
                CBXSL_HOT = 2,
                CBXSL_PRESSED = 3,
                CBXSL_DISABLED = 4,
            }

            public enum TransparentBack
            {
                CBTBS_NORMAL = 1,
                CBTBS_HOT = 2,
                CBTBS_DISABLED = 3,
                CBTBS_FOCUSED = 4,
            }

            public enum Border
            {
                CBB_NORMAL = 1,
                CBB_HOT = 2,
                CBB_FOCUSED = 3,
                CBB_DISABLED = 4,
            }

            public enum Readonly
            {
                CBRO_NORMAL = 1,
                CBRO_HOT = 2,
                CBRO_PRESSED = 3,
                CBRO_DISABLED = 4,
            }

            public enum CueBanner
            {
                CBCB_NORMAL = 1,
                CBCB_HOT = 2,
                CBCB_PRESSED = 3,
                CBCB_DISABLED = 4,
            }
        }
    }
}
