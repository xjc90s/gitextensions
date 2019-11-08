using System;
using System.Drawing;

namespace GitUI.Theming
{
    internal static class EditRenderer
    {
        public static int RenderEdit(IntPtr hdc, int partId, int stateId, NativeMethods.RECT prect)
        {
            switch ((Parts)partId)
            {
                case Parts.EP_EDITTEXT:
                {
                    switch ((State.Text)stateId)
                    {
                        case State.Text.ETS_NORMAL:
                            // fix numeric updown border
                            using (var g = Graphics.FromHdcInternal(hdc))
                            {
                                g.FillRectangle(SystemBrushes.Window, prect.ToRectangle());
                                var borderRect = Rectangle.FromLTRB(
                                    prect.Left, prect.Top,
                                    prect.Right - 1, prect.Bottom - 1);
                                g.DrawRectangle(SystemPens.ControlDark, borderRect);
                            }

                            return 0;
                    }

                    break;
                }
            }

            return 1;
        }

        private enum Parts
        {
            EP_EDITTEXT = 1,
            EP_CARET = 2,
            EP_BACKGROUND = 3,
            EP_PASSWORD = 4,
            EP_BACKGROUNDWITHBORDER = 5,
            EP_EDITBORDER_NOSCROLL = 6,
            EP_EDITBORDER_HSCROLL = 7,
            EP_EDITBORDER_VSCROLL = 8,
            EP_EDITBORDER_HVSCROLL = 9,
        }

        private class State
        {
            public enum Text
            {
                ETS_NORMAL = 1,
                ETS_HOT = 2,
                ETS_SELECTED = 3,
                ETS_DISABLED = 4,
                ETS_FOCUSED = 5,
                ETS_READONLY = 6,
                ETS_ASSIST = 7,
                ETS_CUEBANNER = 8,
            }

            public enum Background
            {
                EBS_NORMAL = 1,
                EBS_HOT = 2,
                EBS_DISABLED = 3,
                EBS_FOCUSED = 4,
                EBS_READONLY = 5,
                EBS_ASSIST = 6,
            }

            public enum BackgroundWithBorder
            {
                EBWBS_NORMAL = 1,
                EBWBS_HOT = 2,
                EBWBS_DISABLED = 3,
                EBWBS_FOCUSED = 4,
            }

            public enum BorderNoScroll
            {
                EPSN_NORMAL = 1,
                EPSN_HOT = 2,
                EPSN_FOCUSED = 3,
                EPSN_DISABLED = 4,
            }

            public enum BorderHScroll
            {
                EPSH_NORMAL = 1,
                EPSH_HOT = 2,
                EPSH_FOCUSED = 3,
                EPSH_DISABLED = 4,
            }

            public enum BorderVScroll
            {
                EPSV_NORMAL = 1,
                EPSV_HOT = 2,
                EPSV_FOCUSED = 3,
                EPSV_DISABLED = 4,
            }

            public enum BorderHVScroll
            {
                EPSHV_NORMAL = 1,
                EPSHV_HOT = 2,
                EPSHV_FOCUSED = 3,
                EPSHV_DISABLED = 4,
            }
        }
    }
}
