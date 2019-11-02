﻿using System;
using System.Drawing;
using System.Linq;

namespace GitUI.Theming
{
    internal static class ScrollBarRenderer
    {
        public static void RenderScrollBar(IntPtr hdc, int partId, int stateId, NativeMethods.RECT prect)
        {
            using (var graphics = Graphics.FromHdcInternal(hdc))
            {
                DrawBackground(graphics, partId, stateId, prect);
                if ((Parts)partId == Parts.SBP_ARROWBTN)
                {
                    DrawArrow(graphics, (States.ArrowButton)stateId, prect);
                }
            }
        }

        private static void DrawBackground(Graphics g, int partId, int stateId, NativeMethods.RECT prect)
        {
            var backColor = GetBackColor(stateId, (Parts)partId);
            g.FillRectangle(
                SystemBrushes.FromSystemColor(Color.FromKnownColor(backColor)),
                Rectangle.FromLTRB(prect.Left, prect.Top, prect.Right, prect.Bottom));
        }

        private static void DrawArrow(Graphics g, States.ArrowButton stateId, NativeMethods.RECT prect)
        {
            var foreColor = GetArrowButtonForeColor(stateId);
            var arrowPts = GetArrowPolygon(prect, stateId);

            g.FillPolygon(
                SystemBrushes.FromSystemColor(Color.FromKnownColor(foreColor)),
                arrowPts.Select(p => new Point(p.X, p.Y)).ToArray());
        }

        private static KnownColor GetBackColor(int stateId, Parts partId)
        {
            KnownColor backColor;
            switch (partId)
            {
                case Parts.SBP_ARROWBTN:
                    backColor = GetArrowButtonBackColor((States.ArrowButton)stateId);
                    break;

                case Parts.SBP_THUMBBTNHORZ:
                case Parts.SBP_THUMBBTNVERT:
                case Parts.SBP_GRIPPERHORZ:
                case Parts.SBP_GRIPPERVERT:
                    switch ((States.TrackThumb)stateId)
                    {
                        case States.TrackThumb.SCRBS_NORMAL:
                        case States.TrackThumb.SCRBS_HOVER:
                            backColor = KnownColor.ControlLight;
                            break;

                        case States.TrackThumb.SCRBS_HOT:
                            backColor = KnownColor.ControlDark;
                            break;
                        case States.TrackThumb.SCRBS_PRESSED:
                            backColor = KnownColor.ControlDarkDark;
                            break;

                        // case States.TrackThumb.SCRBS_DISABLED:
                        default:
                            backColor = KnownColor.Control;
                            break;
                    }

                    break;

                // case Parts.SBP_LOWERTRACKHORZ:
                // case Parts.SBP_LOWERTRACKVERT:
                // case Parts.SBP_UPPERTRACKHORZ:
                // case Parts.SBP_UPPERTRACKVERT:
                // case Parts.SBP_SIZEBOX:
                default:
                    switch ((States.TrackThumb)stateId)
                    {
                        // case States.TrackThumb.SCRBS_NORMAL:
                        // case States.TrackThumb.SCRBS_HOVER:
                        // case States.TrackThumb.SCRBS_HOT:
                        // case States.TrackThumb.SCRBS_PRESSED:
                        // case States.TrackThumb.SCRBS_DISABLED:
                        default:
                            backColor = KnownColor.Control;
                            break;
                    }

                    break;
            }

            return backColor;
        }

        private static KnownColor GetArrowButtonBackColor(States.ArrowButton stateId)
        {
            switch (stateId)
            {
                case States.ArrowButton.ABS_UPPRESSED:
                case States.ArrowButton.ABS_DOWNPRESSED:
                case States.ArrowButton.ABS_LEFTPRESSED:
                case States.ArrowButton.ABS_RIGHTPRESSED:
                    return KnownColor.ControlDarkDark;

                case States.ArrowButton.ABS_UPHOT:
                case States.ArrowButton.ABS_DOWNHOT:
                case States.ArrowButton.ABS_LEFTHOT:
                case States.ArrowButton.ABS_RIGHTHOT:
                    return KnownColor.ControlDark;

                case States.ArrowButton.ABS_UPHOVER:
                case States.ArrowButton.ABS_DOWNHOVER:
                case States.ArrowButton.ABS_LEFTHOVER:
                case States.ArrowButton.ABS_RIGHTHOVER:
                    return KnownColor.ControlLight;

                // case States.ArrowButton.ABS_UPDISABLED:
                // case States.ArrowButton.ABS_DOWNDISABLED:
                // case States.ArrowButton.ABS_LEFTDISABLED:
                // case States.ArrowButton.ABS_RIGHTDISABLED:
                // case States.ArrowButton.ABS_UPNORMAL:
                // case States.ArrowButton.ABS_DOWNNORMAL:
                // case States.ArrowButton.ABS_LEFTNORMAL:
                // case States.ArrowButton.ABS_RIGHTNORMAL:
                default:
                    return KnownColor.Control;
            }
        }

        private static KnownColor GetArrowButtonForeColor(States.ArrowButton stateId)
        {
            switch (stateId)
            {
                case States.ArrowButton.ABS_UPPRESSED:
                case States.ArrowButton.ABS_DOWNPRESSED:
                case States.ArrowButton.ABS_LEFTPRESSED:
                case States.ArrowButton.ABS_RIGHTPRESSED:
                    return KnownColor.Control;

                case States.ArrowButton.ABS_UPDISABLED:
                case States.ArrowButton.ABS_DOWNDISABLED:
                case States.ArrowButton.ABS_LEFTDISABLED:
                case States.ArrowButton.ABS_RIGHTDISABLED:
                    return KnownColor.GrayText;

                // case States.ArrowButton.ABS_UPHOT:
                // case States.ArrowButton.ABS_DOWNHOT:
                // case States.ArrowButton.ABS_LEFTHOT:
                // case States.ArrowButton.ABS_RIGHTHOT:
                // case States.ArrowButton.ABS_UPNORMAL:
                // case States.ArrowButton.ABS_DOWNNORMAL:
                // case States.ArrowButton.ABS_LEFTNORMAL:
                // case States.ArrowButton.ABS_RIGHTNORMAL:
                // case States.ArrowButton.ABS_UPHOVER:
                // case States.ArrowButton.ABS_DOWNHOVER:
                // case States.ArrowButton.ABS_LEFTHOVER:
                // case States.ArrowButton.ABS_RIGHTHOVER:
                default:
                    return KnownColor.ControlDarkDark;
            }
        }

        private static Point[] GetArrowPolygon(NativeMethods.RECT prect, States.ArrowButton stateId)
        {
            switch (stateId)
            {
                case States.ArrowButton.ABS_UPNORMAL:
                case States.ArrowButton.ABS_UPHOT:
                case States.ArrowButton.ABS_UPPRESSED:
                case States.ArrowButton.ABS_UPDISABLED:
                case States.ArrowButton.ABS_UPHOVER:
                    return GetUpArrowPolygon(prect);

                case States.ArrowButton.ABS_DOWNNORMAL:
                case States.ArrowButton.ABS_DOWNHOT:
                case States.ArrowButton.ABS_DOWNPRESSED:
                case States.ArrowButton.ABS_DOWNDISABLED:
                case States.ArrowButton.ABS_DOWNHOVER:
                    return GetDownArrowPolygon(prect);

                case States.ArrowButton.ABS_RIGHTNORMAL:
                case States.ArrowButton.ABS_RIGHTHOT:
                case States.ArrowButton.ABS_RIGHTPRESSED:
                case States.ArrowButton.ABS_RIGHTDISABLED:
                case States.ArrowButton.ABS_RIGHTHOVER:
                    return GetRightArrowPolygon(prect);

                // case States.ArrowButton.ABS_LEFTNORMAL:
                // case States.ArrowButton.ABS_LEFTHOT:
                // case States.ArrowButton.ABS_LEFTPRESSED:
                // case States.ArrowButton.ABS_LEFTDISABLED:
                // case States.ArrowButton.ABS_LEFTHOVER:
                default:
                    return GetLeftArrowPolygon(prect);
            }
        }

        private static Point[] GetUpArrowPolygon(NativeMethods.RECT prect)
        {
            int h = prect.Bottom - prect.Top;
            int w = prect.Right - prect.Left;
            int arrowHeight = (int)Math.Ceiling(0.25f * h);
            int arrowWidth = (int)Math.Ceiling(0.5f * w);
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
            int arrowWidth = (int)Math.Ceiling(0.5f * w);
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

        private static Point[] GetRightArrowPolygon(NativeMethods.RECT prect)
        {
            int h = prect.Bottom - prect.Top;
            int w = prect.Right - prect.Left;
            int arrowHeight = (int)Math.Ceiling(0.5f * h);
            int arrowWidth = (int)Math.Ceiling(0.25f * w);
            int arrowLeft = prect.Left + ((w - arrowWidth) / 2);
            int arrowTop = prect.Top + ((h - arrowHeight) / 2);
            int x1 = arrowLeft;
            int x2 = arrowLeft + arrowWidth;
            int y1 = arrowTop;
            int y2 = arrowTop + (int)Math.Ceiling(0.5f * arrowHeight);
            int y3 = arrowTop + arrowHeight + 1;
            return new[]
            {
                new Point(x1, y1),
                new Point(x2, y2),
                new Point(x1, y3)
            };
        }

        private static Point[] GetLeftArrowPolygon(NativeMethods.RECT prect)
        {
            int h = prect.Bottom - prect.Top;
            int w = prect.Right - prect.Left;
            int arrowHeight = (int)Math.Ceiling(0.5f * h);
            int arrowWidth = (int)Math.Ceiling(0.25f * w);
            int arrowLeft = prect.Left + ((w - arrowWidth) / 2);
            int arrowTop = prect.Top + ((h - arrowHeight) / 2);
            int x1 = arrowLeft;
            int x2 = arrowLeft + arrowWidth;
            int y1 = arrowTop;
            int y2 = arrowTop + (int)Math.Ceiling(0.5f * arrowHeight);
            int y3 = arrowTop + arrowHeight + 1;

            return new[]
            {
                new Point(x2, y1),
                new Point(x1, y2),
                new Point(x2, y3),
            };
        }

        private enum Parts
        {
            SBP_ARROWBTN = 1,
            SBP_THUMBBTNHORZ = 2,
            SBP_THUMBBTNVERT = 3,
            SBP_LOWERTRACKHORZ = 4,
            SBP_UPPERTRACKHORZ = 5,
            SBP_LOWERTRACKVERT = 6,
            SBP_UPPERTRACKVERT = 7,
            SBP_GRIPPERHORZ = 8,
            SBP_GRIPPERVERT = 9,
            SBP_SIZEBOX = 10,
        }

        private static class States
        {
            public enum ArrowButton
            {
                ABS_UPNORMAL = 1,
                ABS_UPHOT = 2,
                ABS_UPPRESSED = 3,
                ABS_UPDISABLED = 4,
                ABS_DOWNNORMAL = 5,
                ABS_DOWNHOT = 6,
                ABS_DOWNPRESSED = 7,
                ABS_DOWNDISABLED = 8,
                ABS_LEFTNORMAL = 9,
                ABS_LEFTHOT = 10,
                ABS_LEFTPRESSED = 11,
                ABS_LEFTDISABLED = 12,
                ABS_RIGHTNORMAL = 13,
                ABS_RIGHTHOT = 14,
                ABS_RIGHTPRESSED = 15,
                ABS_RIGHTDISABLED = 16,
                ABS_UPHOVER = 17,
                ABS_DOWNHOVER = 18,
                ABS_LEFTHOVER = 19,
                ABS_RIGHTHOVER = 20,
            }

            public enum SizeBox
            {
                SZB_HALFBOTTOMLEFTALIGN = 6,
                SZB_HALFBOTTOMRIGHTALIGN = 5,
                SZB_HALFTOPLEFTALIGN = 8,
                SZB_HALFTOPRIGHTALIGN = 7,
                SZB_LEFTALIGN = 2,
                SZB_RIGHTALIGN = 1,
                SZB_TOPLEFTALIGN = 4,
                SZB_TOPRIGHTALIGN = 3,
            }

            public enum TrackThumb
            {
                SCRBS_NORMAL = 1,
                SCRBS_HOT = 2,
                SCRBS_PRESSED = 3,
                SCRBS_DISABLED = 4,
                SCRBS_HOVER = 5,
            }
        }
    }
}
