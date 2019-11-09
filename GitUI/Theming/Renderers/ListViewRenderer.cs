﻿using System;
using System.Drawing;
using GitExtUtils.GitUI.Theming;

namespace GitUI.Theming
{
    internal class ListViewRenderer : ThemeRenderer
    {
        protected override string Clsid { get; } = "Listview";

        public override int RenderText(
            IntPtr htheme,
            IntPtr hdc,
            int partid, int stateid,
            string psztext, int cchtext,
            NativeMethods.DT dwtextflags,
            ref NativeMethods.RECT prect, ref NativeMethods.DTTOPTS poptions)
        {
            switch ((Parts)partid)
            {
                case Parts.LVP_GROUPHEADER:
                {
                    NativeMethods.GetThemeColor(htheme, partid, stateid, poptions.iColorPropId, out var crefText);
                    var color = crefText.ToColor();
                    var adaptedColor = color.AdaptTextColor();

                    // do not render, just modify text color
                    poptions.iColorPropId = 0;
                    poptions.crText = ColorTranslator.ToWin32(adaptedColor);
                    return 1;
                }
            }

            return 1;
        }

        private enum Parts
        {
            LVP_LISTITEM = 1,
            LVP_LISTGROUP = 2,
            LVP_LISTDETAIL = 3,
            LVP_LISTSORTEDDETAIL = 4,
            LVP_EMPTYTEXT = 5,
            LVP_GROUPHEADER = 6,
            LVP_GROUPHEADERLINE = 7,
            LVP_EXPANDBUTTON = 8,
            LVP_COLLAPSEBUTTON = 9,
            LVP_COLUMNDETAIL = 10
        }

        private static class State
        {
            public enum CollapseButton
            {
                LVCB_NORMAL = 1,
                LVCB_HOVER = 2,
                LVCB_PUSHED = 3,
            }

            public enum ExpandButton
            {
                LVEB_NORMAL = 1,
                LVEB_HOVER = 2,
                LVEB_PUSHED = 3,
            }

            public enum GroupHeader
            {
                LVGH_OPEN = 1,
                LVGH_OPENHOT = 2,
                LVGH_OPENSELECTED = 3,
                LVGH_OPENSELECTEDHOT = 4,
                LVGH_OPENSELECTEDNOTFOCUSED = 5,
                LVGH_OPENSELECTEDNOTFOCUSEDHOT = 6,
                LVGH_OPENMIXEDSELECTION = 7,
                LVGH_OPENMIXEDSELECTIONHOT = 8,
                LVGH_CLOSE = 9,
                LVGH_CLOSEHOT = 10,
                LVGH_CLOSESELECTED = 11,
                LVGH_CLOSESELECTEDHOT = 12,
                LVGH_CLOSESELECTEDNOTFOCUSED = 13,
                LVGH_CLOSESELECTEDNOTFOCUSEDHOT = 14,
                LVGH_CLOSEMIXEDSELECTION = 15,
                LVGH_CLOSEMIXEDSELECTIONHOT = 16,
            }

            public enum GroupHeaderLine
            {
                LVGHL_OPEN = 1,
                LVGHL_OPENHOT = 2,
                LVGHL_OPENSELECTED = 3,
                LVGHL_OPENSELECTEDHOT = 4,
                LVGHL_OPENSELECTEDNOTFOCUSED = 5,
                LVGHL_OPENSELECTEDNOTFOCUSEDHOT = 6,
                LVGHL_OPENMIXEDSELECTION = 7,
                LVGHL_OPENMIXEDSELECTIONHOT = 8,
                LVGHL_CLOSE = 9,
                LVGHL_CLOSEHOT = 10,
                LVGHL_CLOSESELECTED = 11,
                LVGHL_CLOSESELECTEDHOT = 12,
                LVGHL_CLOSESELECTEDNOTFOCUSED = 13,
                LVGHL_CLOSESELECTEDNOTFOCUSEDHOT = 14,
                LVGHL_CLOSEMIXEDSELECTION = 15,
                LVGHL_CLOSEMIXEDSELECTIONHOT = 16,
            }

            public enum ListItem
            {
                LISS_NORMAL = 1,
                LISS_HOT = 2,
                LISS_SELECTED = 3,
                LISS_DISABLED = 4,
                LISS_SELECTEDNOTFOCUS = 5,
                LISS_HOTSELECTED = 6,
            }
        }
    }
}
