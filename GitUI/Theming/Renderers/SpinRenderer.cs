using System;
using System.Drawing;

namespace GitUI.Theming
{
    internal static class SpinRenderer
    {
        public static int RenderSpin(IntPtr hdc, int partId, int stateId, NativeMethods.RECT prect)
        {
            switch ((Parts)partId)
            {
                case Parts.SPNP_UP:
                {
                    var backBrush = GetBackBrush((State.Up)stateId);

                    var foreBrush = GetForeBrush((State.Up)stateId);
                    var arrowPolygon = GetUpArrowPolygon(prect);
                    using (var g = Graphics.FromHdcInternal(hdc))
                    {
                        g.FillRectangle(backBrush, prect.ToRectangle());
                        g.FillPolygon(foreBrush, arrowPolygon);
                    }

                    return 0;
                }

                case Parts.SPNP_DOWN:
                {
                    var backBrush = GetBackBrush((State.Down)stateId);
                    var foreBrush = GetForeBrush((State.Down)stateId);
                    var arrowPolygon = GetDownArrowPolygon(prect);
                    using (var g = Graphics.FromHdcInternal(hdc))
                    {
                        g.FillRectangle(backBrush, prect.ToRectangle());
                        g.FillPolygon(foreBrush, arrowPolygon);
                    }

                    return 0;
                }
            }

            return 1;
        }

        private static Brush GetBackBrush(State.Up stateId)
        {
            switch (stateId)
            {
                case State.Up.UPS_HOT:
                    return SystemBrushes.ControlDark;

                case State.Up.UPS_PRESSED:
                    return SystemBrushes.ControlDarkDark;

                // case States.Up.UPS_NORMAL:
                // case States.Up.UPS_DISABLED:
                default:
                    return SystemBrushes.Control;
            }
        }

        private static Brush GetBackBrush(State.Down stateId)
        {
            switch (stateId)
            {
                case State.Down.DNS_HOT:
                    return SystemBrushes.ControlDark;

                case State.Down.DNS_PRESSED:
                    return SystemBrushes.ControlDarkDark;

                // case States.Down.DNS_NORMAL:
                // case States.Down.DNS_DISABLED:
                default:
                    return SystemBrushes.Control;
            }
        }

        private static Brush GetForeBrush(State.Up stateId)
        {
            switch (stateId)
            {
                case State.Up.UPS_PRESSED:
                    return SystemBrushes.Control;

                case State.Up.UPS_DISABLED:
                    return SystemBrushes.GrayText;

                // case States.Up.UPS_NORMAL:
                // case States.Up.UPS_HOT:
                default:
                    return SystemBrushes.ControlDarkDark;
            }
        }

        private static Brush GetForeBrush(State.Down stateId)
        {
            switch (stateId)
            {
                case State.Down.DNS_PRESSED:
                    return SystemBrushes.Control;

                case State.Down.DNS_DISABLED:
                    return SystemBrushes.GrayText;

                // case States.Down.DNS_NORMAL:
                // case States.Down.DNS_HOT:
                default:
                    return SystemBrushes.ControlDarkDark;
            }
        }

        private static Point[] GetUpArrowPolygon(NativeMethods.RECT prect)
        {
            int h = prect.Bottom - prect.Top;
            int w = prect.Right - prect.Left;
            int arrowHeight = (int)Math.Ceiling(0.25f * h);
            int arrowWidth = 2 * arrowHeight;
            int arrowLeft = prect.Left + ((w - arrowWidth) / 2);
            int arrowTop = prect.Top + ((h - arrowHeight) / 2);
            int x1 = arrowLeft - 1;
            int x2 = arrowLeft + (int)Math.Floor(0.5f * arrowWidth);
            int x3 = arrowLeft + arrowWidth;
            int y1 = arrowTop - 1;
            int y2 = arrowTop + arrowHeight;
            return new[]
            {
                new Point(x1, y2),
                new Point(x2, y1),
                new Point(x3, y2)
            };
        }

        private static Point[] GetDownArrowPolygon(NativeMethods.RECT prect)
        {
            int h = prect.Bottom - prect.Top;
            int w = prect.Right - prect.Left;
            int arrowHeight = (int)Math.Ceiling(0.25f * h);
            int arrowWidth = 2 * arrowHeight;
            int arrowLeft = prect.Left + ((w - arrowWidth) / 2);
            int arrowTop = prect.Top + ((h - arrowHeight) / 2);
            int x1 = arrowLeft;
            int x2 = arrowLeft + (int)Math.Floor(0.5f * arrowWidth);
            int x3 = arrowLeft + arrowWidth;
            int y1 = arrowTop;
            int y2 = arrowTop + arrowHeight;
            return new[]
            {
                new Point(x1, y1),
                new Point(x2, y2),
                new Point(x3, y1)
            };
        }

        private enum Parts
        {
            SPNP_UP = 1,
            SPNP_DOWN = 2,
            SPNP_UPHORZ = 3,
            SPNP_DOWNHORZ = 4,
        }

        private static class State
        {
            public enum Up
            {
                UPS_NORMAL = 1,
                UPS_HOT = 2,
                UPS_PRESSED = 3,
                UPS_DISABLED = 4,
            }

            public enum Down
            {
                DNS_NORMAL = 1,
                DNS_HOT = 2,
                DNS_PRESSED = 3,
                DNS_DISABLED = 4,
            }

            public enum UpHorizontal
            {
                UPHZS_NORMAL = 1,
                UPHZS_HOT = 2,
                UPHZS_PRESSED = 3,
                UPHZS_DISABLED = 4,
            }

            public enum DownHorizontal
            {
                DNHZS_NORMAL = 1,
                DNHZS_HOT = 2,
                DNHZS_PRESSED = 3,
                DNHZS_DISABLED = 4,
            }
        }
    }
}
