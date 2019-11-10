using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using GitExtUtils.GitUI;

namespace GitUI.Theming
{
    internal class ButtonRenderer : ThemeRenderer
    {
        protected override string Clsid { get; } = "Button";

        public override int RenderBackground(IntPtr hdc, int partid, int stateid, Rectangle prect,
            ref NativeMethods.RECT pcliprect)
        {
            switch ((Parts)partid)
            {
                case Parts.BP_GROUPBOX:
                    RenderGroupBox(hdc, prect, pcliprect);
                    return 0;

                case Parts.BP_PUSHBUTTON:
                    RenderPushButton(hdc, (State.Push)stateid, prect);
                    return 0;

                case Parts.BP_CHECKBOX:
                    return RenderCheckBox(hdc, (State.CheckBox)stateid, prect);

                case Parts.BP_RADIOBUTTON:
                    RenderRadio(hdc, (State.Radio)stateid, prect);
                    break;

                default:
                    return 1;
            }

            return 0;
        }

        private static void RenderGroupBox(IntPtr hdc, Rectangle prect, NativeMethods.RECT pcliprect)
        {
            using (var g = Graphics.FromHdcInternal(hdc))
            {
                var prevClip = g.Clip;
                Rectangle clip;
                try
                {
                    clip = pcliprect;
                }
                catch (NullReferenceException)
                {
                    clip = Rectangle.Empty;
                }

                g.SetClip(clip);
                g.DrawRectangle(SystemPens.ControlDark, prect.Inclusive());
                g.Clip = prevClip;
            }
        }

        private static void RenderPushButton(IntPtr hdc, State.Push stateid, Rectangle prect)
        {
            using (var g = Graphics.FromHdcInternal(hdc))
            {
                var border = prect.Inclusive();
                switch (stateid)
                {
                    case State.Push.PBS_DISABLED:
                        g.FillRectangle(SystemBrushes.Control, prect);
                        g.DrawRectangle(SystemPens.ControlLight, border);
                        break;

                    case State.Push.PBS_HOT:
                        g.FillRectangle(SystemBrushes.ControlLight, prect);
                        g.DrawRectangle(SystemPens.HotTrack, border);
                        break;

                    case State.Push.PBS_PRESSED:
                        g.FillRectangle(SystemBrushes.ControlDark, prect);
                        g.DrawRectangle(SystemPens.HotTrack, border);
                        break;

                    case State.Push.PBS_DEFAULTED:
                    case State.Push.PBS_DEFAULTED_ANIMATING:
                        g.FillRectangle(SystemBrushes.Control, prect);
                        using (var pen = new Pen(SystemColors.HotTrack, 2))
                        {
                            border.Inflate(-1, -1);
                            g.DrawRectangle(pen, border);
                        }

                        break;

                    // case State.Push.PBS_NORMAL:
                    default:
                        g.FillRectangle(SystemBrushes.Control, prect);
                        g.DrawRectangle(SystemPens.ControlDark, prect.Inclusive());
                        break;
                }
            }
        }

        private int RenderCheckBox(IntPtr hdc, State.CheckBox stateid, Rectangle prect)
        {
            Brush backBrush;
            Color foreColor;
            Pen borderPen;
            switch (stateid)
            {
                case State.CheckBox.CBS_UNCHECKEDNORMAL:
                case State.CheckBox.CBS_CHECKEDNORMAL:
                case State.CheckBox.CBS_MIXEDNORMAL:
                    backBrush = SystemBrushes.Window;
                    foreColor = SystemColors.WindowText;
                    borderPen = SystemPens.ControlDarkDark;
                    break;

                case State.CheckBox.CBS_UNCHECKEDHOT:
                case State.CheckBox.CBS_CHECKEDHOT:
                case State.CheckBox.CBS_MIXEDHOT:
                    backBrush = SystemBrushes.Window;
                    foreColor = SystemColors.HotTrack;
                    borderPen = SystemPens.HotTrack;
                    break;
                case State.CheckBox.CBS_UNCHECKEDPRESSED:
                case State.CheckBox.CBS_CHECKEDPRESSED:
                case State.CheckBox.CBS_MIXEDPRESSED:
                    backBrush = SystemBrushes.ControlLightLight;
                    foreColor = SystemColors.HotTrack;
                    borderPen = SystemPens.HotTrack;
                    break;

                case State.CheckBox.CBS_UNCHECKEDDISABLED:
                case State.CheckBox.CBS_CHECKEDDISABLED:
                case State.CheckBox.CBS_MIXEDDISABLED:
                    backBrush = SystemBrushes.Control;
                    foreColor = SystemColors.ControlDark;
                    borderPen = SystemPens.ControlLight;
                    break;

                // case State.CheckBox.CBS_IMPLICITNORMAL:
                // case State.CheckBox.CBS_IMPLICITHOT:
                // case State.CheckBox.CBS_IMPLICITPRESSED:
                // case State.CheckBox.CBS_IMPLICITDISABLED:
                // case State.CheckBox.CBS_EXCLUDEDNORMAL:
                // case State.CheckBox.CBS_EXCLUDEDHOT:
                // case State.CheckBox.CBS_EXCLUDEDPRESSED:
                // case State.CheckBox.CBS_EXCLUDEDDISABLED:
                default:
                    return 1;
            }

            using (var g = Graphics.FromHdcInternal(hdc))
            {
                g.FillRectangle(backBrush, prect);
                var border = prect.Inclusive();
                g.DrawRectangle(borderPen, border);

                switch (stateid)
                {
                    case State.CheckBox.CBS_MIXEDNORMAL:
                    case State.CheckBox.CBS_MIXEDHOT:
                    case State.CheckBox.CBS_MIXEDPRESSED:
                    case State.CheckBox.CBS_MIXEDDISABLED:
                        prect.Inflate(-prect.Width / 4, -prect.Height / 4);
                        g.FillRectangle(SystemBrushes.FromSystemColor(foreColor), prect);
                        break;

                    case State.CheckBox.CBS_CHECKEDNORMAL:
                    case State.CheckBox.CBS_CHECKEDHOT:
                    case State.CheckBox.CBS_CHECKEDPRESSED:
                    case State.CheckBox.CBS_CHECKEDDISABLED:
                        int padding = DpiUtil.Scale(2);
                        int x1 = border.Left + padding;
                        int x2 = border.Left + (border.Width / 2);
                        int x3 = border.Right - padding;
                        int y1 = border.Top + (border.Height / 4);
                        int y2 = border.Top + (border.Height / 2);
                        int y3 = border.Bottom - (border.Height / 4);
                        var points = new[]
                        {
                            new Point(x1, y2),
                            new Point(x2, y3),
                            new Point(x3, y1),
                        };
                        using (var checkPen = new Pen(foreColor, DpiUtil.Scale(1.5f)))
                        using (new HighQualityScope(g))
                        {
                            g.DrawLines(checkPen, points);
                        }

                        break;
                }
            }

            return 0;
        }

        private void RenderRadio(IntPtr hdc, State.Radio stateid, Rectangle prect)
        {
            Brush backBrush;
            Color foreColor;
            switch (stateid)
            {
                case State.Radio.RBS_UNCHECKEDNORMAL:
                case State.Radio.RBS_CHECKEDNORMAL:
                    backBrush = SystemBrushes.Window;
                    foreColor = SystemColors.WindowText;
                    break;

                case State.Radio.RBS_UNCHECKEDHOT:
                case State.Radio.RBS_CHECKEDHOT:
                    backBrush = SystemBrushes.Window;
                    foreColor = SystemColors.HotTrack;
                    break;

                case State.Radio.RBS_UNCHECKEDPRESSED:
                case State.Radio.RBS_CHECKEDPRESSED:
                    backBrush = SystemBrushes.ControlLightLight;
                    foreColor = SystemColors.HotTrack;
                    break;

                // case State.Radio.RBS_UNCHECKEDDISABLED:
                // case State.Radio.RBS_CHECKEDDISABLED:
                default:
                    backBrush = SystemBrushes.Control;
                    foreColor = SystemColors.ControlDark;
                    break;
            }

            using (var g = Graphics.FromHdcInternal(hdc))
            using (new HighQualityScope(g))
            {
                g.FillEllipse(backBrush, prect);
                g.DrawEllipse(SystemPens.FromSystemColor(foreColor), prect);

                switch (stateid)
                {
                    case State.Radio.RBS_CHECKEDNORMAL:
                    case State.Radio.RBS_CHECKEDHOT:
                    case State.Radio.RBS_CHECKEDPRESSED:
                    case State.Radio.RBS_CHECKEDDISABLED:
                        prect.Inflate(-prect.Width / 4, -prect.Height / 4);
                        g.FillEllipse(SystemBrushes.FromSystemColor(foreColor), prect);
                        break;
                }
            }
        }

        private enum Parts
        {
            BP_PUSHBUTTON = 1,
            BP_RADIOBUTTON = 2,
            BP_CHECKBOX = 3,
            BP_GROUPBOX = 4,
            BP_USERBUTTON = 5,
            BP_COMMANDLINK = 6,
            BP_COMMANDLINKGLYPH = 7,
        }

        private class State
        {
            public enum Push
            {
                PBS_NORMAL = 1,
                PBS_HOT = 2,
                PBS_PRESSED = 3,
                PBS_DISABLED = 4,
                PBS_DEFAULTED = 5,
                PBS_DEFAULTED_ANIMATING = 6,
            }

            public enum Radio
            {
                RBS_UNCHECKEDNORMAL = 1,
                RBS_UNCHECKEDHOT = 2,
                RBS_UNCHECKEDPRESSED = 3,
                RBS_UNCHECKEDDISABLED = 4,
                RBS_CHECKEDNORMAL = 5,
                RBS_CHECKEDHOT = 6,
                RBS_CHECKEDPRESSED = 7,
                RBS_CHECKEDDISABLED = 8,
            }

            public enum CheckBox
            {
                CBS_UNCHECKEDNORMAL = 1,
                CBS_UNCHECKEDHOT = 2,
                CBS_UNCHECKEDPRESSED = 3,
                CBS_UNCHECKEDDISABLED = 4,
                CBS_CHECKEDNORMAL = 5,
                CBS_CHECKEDHOT = 6,
                CBS_CHECKEDPRESSED = 7,
                CBS_CHECKEDDISABLED = 8,
                CBS_MIXEDNORMAL = 9,
                CBS_MIXEDHOT = 10,
                CBS_MIXEDPRESSED = 11,
                CBS_MIXEDDISABLED = 12,
                CBS_IMPLICITNORMAL = 13,
                CBS_IMPLICITHOT = 14,
                CBS_IMPLICITPRESSED = 15,
                CBS_IMPLICITDISABLED = 16,
                CBS_EXCLUDEDNORMAL = 17,
                CBS_EXCLUDEDHOT = 18,
                CBS_EXCLUDEDPRESSED = 19,
                CBS_EXCLUDEDDISABLED = 20,
            }

            public enum GroupBox
            {
                GBS_NORMAL = 1,
                GBS_DISABLED = 2,
            }

            public enum CommandLink
            {
                CMDLGS_NORMAL = 1,
                CMDLGS_HOT = 2,
                CMDLGS_PRESSED = 3,
                CMDLGS_DISABLED = 4,
                CMDLGS_DEFAULTED = 5,
            }
        }
    }
}
