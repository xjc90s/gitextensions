﻿using System;
using System.Drawing;

namespace GitUI.Theming
{
    internal static class HeaderRenderer
    {
        public static int RenderHeader(IntPtr hdc, int partId, int stateId, NativeMethods.RECT prect)
        {
            switch ((Parts)partId)
            {
                case 0:
                {
                    using (var g = Graphics.FromHdcInternal(hdc))
                    {
                        g.FillRectangle(SystemBrushes.Control, prect.ToRectangle());
                    }

                    return 0;
                }

                case Parts.HP_HEADERITEM:
                {
                    using (var g = Graphics.FromHdcInternal(hdc))
                    {
                        var backBrush = GetBackBrush((State.Item)stateId);
                        g.FillRectangle(backBrush, prect.ToRectangle());
                        g.DrawLine(SystemPens.ControlDark,
                            new Point(prect.Right - 2, prect.Top),
                            new Point(prect.Right - 2, prect.Bottom - 1));
                    }

                    return 0;
                }

                case Parts.HP_HEADERSORTARROW:
                {
                    using (var g = Graphics.FromHdcInternal(hdc))
                    {
                        g.FillRectangle(SystemBrushes.Control, prect.ToRectangle());
                        var arrowPoints = GetArrowPolygon((State.SortArrow)stateId, prect);
                        g.FillPolygon(SystemBrushes.ControlDarkDark, arrowPoints);
                    }

                    return 0;
                }

                // case Parts.HP_HEADERITEMLEFT:
                // case Parts.HP_HEADERITEMRIGHT:
                // case Parts.HP_HEADERDROPDOWN:
                // case Parts.HP_HEADERDROPDOWNFILTER:
                // case Parts.HP_HEADEROVERFLOW:
                default:
                {
                    return 1;
                }
            }
        }

        private static Point[] GetArrowPolygon(State.SortArrow stateId, NativeMethods.RECT prect)
        {
            switch (stateId)
            {
                case State.SortArrow.HSAS_SORTEDUP:
                    return GetUpArrowPolygon(prect);

                // case State.SortArrow.HSAS_SORTEDDOWN:
                default:
                    return GetDownArrowPolygon(prect);
            }
        }

        private static Brush GetBackBrush(State.Item stateId)
        {
            switch (stateId)
            {
                case State.Item.HIS_NORMAL:
                case State.Item.HIS_SORTEDNORMAL:
                case State.Item.HIS_ICONNORMAL:
                case State.Item.HIS_ICONSORTEDNORMAL:
                    return SystemBrushes.Control;

                case State.Item.HIS_HOT:
                case State.Item.HIS_SORTEDHOT:
                case State.Item.HIS_ICONHOT:
                case State.Item.HIS_ICONSORTEDHOT:
                    return SystemBrushes.ControlLight;

                // case State.HeaderItem.HIS_PRESSED:
                // case State.HeaderItem.HIS_SORTEDPRESSED:
                // case State.HeaderItem.HIS_ICONPRESSED:
                // case State.HeaderItem.HIS_ICONSORTEDPRESSED:
                default:
                    return SystemBrushes.ControlDark;
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

        private enum Parts
        {
            HP_HEADERITEM = 1,
            HP_HEADERITEMLEFT = 2,
            HP_HEADERITEMRIGHT = 3,
            HP_HEADERSORTARROW = 4,
            HP_HEADERDROPDOWN = 5,
            HP_HEADERDROPDOWNFILTER = 6,
            HP_HEADEROVERFLOW = 7
        }

        private static class State
        {
            public enum Item
            {
                HIS_NORMAL = 1,
                HIS_HOT = 2,
                HIS_PRESSED = 3,
                HIS_SORTEDNORMAL = 4,
                HIS_SORTEDHOT = 5,
                HIS_SORTEDPRESSED = 6,
                HIS_ICONNORMAL = 7,
                HIS_ICONHOT = 8,
                HIS_ICONPRESSED = 9,
                HIS_ICONSORTEDNORMAL = 10,
                HIS_ICONSORTEDHOT = 11,
                HIS_ICONSORTEDPRESSED = 12,
            }

            public enum ItemLeft
            {
                HILS_NORMAL = 1,
                HILS_HOT = 2,
                HILS_PRESSED = 3
            }

            public enum ItemRight
            {
                HIRS_NORMAL = 1,
                HIRS_HOT = 2,
                HIRS_PRESSED = 3
            }

            public enum ItemOverflow
            {
                HOFS_NORMAL = 1,
                HOFS_HOT = 2,
            }

            public enum DropDown
            {
                HDDS_NORMAL = 1,
                HDDS_SOFTHOT = 2,
                HDDS_HOT = 3
            }

            public enum DropDownFilter
            {
                HDDFS_NORMAL = 1,
                HDDFS_SOFTHOT = 2,
                HDDFS_HOT = 3
            }

            public enum SortArrow
            {
                HSAS_SORTEDUP = 1,
                HSAS_SORTEDDOWN = 2,
            }
        }
    }
}
