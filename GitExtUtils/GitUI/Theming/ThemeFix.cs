using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using GitUI;

namespace GitExtUtils.GitUI.Theming
{
    public static class ThemeFix
    {
        private const BorderStyle PreferredBorderStyle = BorderStyle.FixedSingle;

        // PreferredFlatStyle can replace AppSettings.UseSystemVisualStyle
        // UseSystemVisualStyle = true -> FlatStyle.Standard
        // UseSystemVisualStyle = false -> FlatStyle.Popup or FlatStyle.Flat
        // Flat and Popup are both viable with Dark theme
        private static readonly FlatStyle PreferredFlatStyle = FlatStyle.Popup;

        private static readonly ConditionalWeakTable<IWin32Window, IWin32Window> Ready =
            new ConditionalWeakTable<IWin32Window, IWin32Window>();

        public static void FixVisualStyle(this Control container)
        {
            container.DescendantsToFix<GroupBox>()
                 .ForEach(SetupGroupBox);
            container.DescendantsToFix<TreeView>()
                .ForEach(SetupTreeView);

            // container.DescendantsToFix<Panel>()
            //     .ForEach(SetupPanel);
            // container.DescendantsToFix<TabControl>()
            //     .ForEach(SetupTabControl);

            // container.DescendantsToFix<DataGridView>()
            //     .ForEach(SetupDataGridView);
            // container.DescendantsToFix<ButtonBase>()
            //     .ForEach(SetupButtonBase);
            // container.DescendantsToFix<ComboBox>()
            //     .ForEach(SetupComboBox);
            // container.DescendantsToFix<ListView>()
            //     .ForEach(SetupListView);
            // container.DescendantsToFix<TextBoxBase>()
            //     .ForEach(SetupTextBoxBase);
            // container.DescendantsToFix<NumericUpDown>()
            //     .ForEach(SetupNumericUpDown);
            // container.DescendantsToFix<LinkLabel>()
            //     .ForEach(SetupLinkLabel);
            // container.DescendantsToFix<ToolStrip>()
            //     .ForEach(SetupToolStrip);
        }

        private static IEnumerable<TControl> DescendantsToFix<TControl>(this Control c)
            where TControl : Control
        {
            return c.FindDescendantsOfType<TControl>(SkipThemeAware)
                .Where(IsFirstTime);
        }

        private static bool SkipThemeAware(Control c) =>
            c.GetType().GetCustomAttribute<ThemeAwareAttribute>() != null;

        private static void SetupDataGridView(DataGridView grid)
        {
            grid.EnableHeadersVisualStyles = false;

            if (grid.BorderStyle != BorderStyle.None)
            {
                grid.BorderStyle = BorderStyle.None;
            }

            foreach (var column in grid.Columns)
            {
                SetupDataGridColumn(column);
            }

            grid.ColumnAdded += (s, e) =>
            {
                SetupDataGridColumn(e.Column);
            };
        }

        private static void SetupDataGridColumn(object column)
        {
            switch (column)
            {
                case DataGridViewComboBoxColumn comboBoxCol:
                    SetupComboBoxColumn(comboBoxCol);
                    break;
                case DataGridViewButtonColumn btnCol:
                    SetupButtonColumn(btnCol);
                    break;
                case DataGridViewCheckBoxColumn checkBoxCol:
                    SetupCheckBoxColumn(checkBoxCol);
                    break;
            }
        }

        private static void SetupGridCellStyle(DataGridViewCellStyle style)
        {
            if (style == null)
            {
                return;
            }

            if (style.BackColor != SystemColors.Window && style.BackColor != Color.Empty)
            {
                return;
            }

            style.BackColor = SystemColors.Control;
            if (style.ForeColor == SystemColors.WindowText)
            {
                style.ForeColor = SystemColors.ControlText;
            }
        }

        private static void SetupButtonColumn(DataGridViewButtonColumn column)
        {
            if (column.CellTemplate != null)
            {
                column.FlatStyle = PreferredFlatStyle;
            }
        }

        private static void SetupComboBoxColumn(DataGridViewComboBoxColumn column)
        {
            if (column.CellTemplate != null)
            {
                column.FlatStyle = PreferredFlatStyle;
            }
        }

        private static void SetupCheckBoxColumn(DataGridViewCheckBoxColumn column)
        {
            if (column.CellTemplate == null)
            {
                column.CellTemplate = new DataGridViewCheckBoxCell();
            }

            column.FlatStyle = PreferredFlatStyle;
            SetupGridCellStyle(column.CellTemplate.Style);
        }

        private static void SetupListView(ListView listView)
        {
            if (listView.BorderStyle != BorderStyle.None)
            {
                listView.BorderStyle = PreferredBorderStyle;
            }
        }

        private static void SetupTextBoxBase(TextBoxBase textBox)
        {
            if (textBox.BorderStyle != BorderStyle.None)
            {
                textBox.BorderStyle = PreferredBorderStyle;
            }

            textBox.TouchBackColor();
            textBox.EnabledChanged += (s, e) => ((TextBoxBase)s).TouchBackColor();
        }

        private static void SetupNumericUpDown(NumericUpDown numericUpDown)
        {
            if (numericUpDown.BorderStyle != BorderStyle.None)
            {
                numericUpDown.BorderStyle = PreferredBorderStyle;
            }
        }

        private static void SetupToolStrip(ToolStrip strip)
        {
            strip.RenderMode = ToolStripRenderMode.Professional;
            strip.Items.OfType<ToolStripLabel>()
                .ForEach(SetupToolStripLabel);
        }

        private static void SetupToolStripLabel(ToolStripLabel label)
        {
            label.LinkColor = label.LinkColor.AdaptTextColor();
            label.VisitedLinkColor = label.VisitedLinkColor.AdaptTextColor();
            label.ActiveLinkColor = label.ActiveLinkColor.AdaptTextColor();
        }

        private static void SetupLinkLabel(this LinkLabel label)
        {
            label.LinkColor = label.LinkColor.AdaptTextColor();
            label.VisitedLinkColor = label.VisitedLinkColor.AdaptTextColor();
            label.ActiveLinkColor = label.ActiveLinkColor.AdaptTextColor();
        }

        private static void SetupGroupBox(this GroupBox box)
        {
            box.TouchForeColor();
        }

        private static void SetupPanel(Panel panel)
        {
            if (panel.BorderStyle != BorderStyle.None)
            {
                panel.BorderStyle = PreferredBorderStyle;
            }
        }

        private static void SetupTabControl(TabControl tabControl)
        {
            new TabControlRenderer(tabControl).Setup();
            tabControl.TabPages.OfType<TabPage>()
                .ForEach(SetupTabPage);
        }

        private static void SetupTabPage(TabPage page)
        {
            if (page.BackColor.IsKnownColor)
            {
                page.TouchBackColor();
            }
        }

        private static void SetupTreeView(TreeView view)
        {
            var unused = view.Handle; // force handle creation
            view.TouchBackColor();
            view.TouchForeColor();
            view.LineColor = SystemColors.ControlDark;
        }

        private static void SetupButtonBase(this ButtonBase btn)
        {
            btn.FlatStyle = PreferredFlatStyle;
            if (PreferredFlatStyle == FlatStyle.Flat)
            {
                UpdateBorderColor(btn);
                btn.EnabledChanged += (s, e) => UpdateBorderColor((ButtonBase)s);
            }

            void UpdateBorderColor(ButtonBase b)
            {
                b.FlatAppearance.BorderColor = b.Enabled
                    ? SystemColors.ActiveBorder
                    : SystemColors.InactiveBorder;
            }

            if (btn is CheckBox checkBox)
            {
                SetupCheckBox(checkBox);
            }
        }

        private static void SetupCheckBox(CheckBox checkBox)
        {
            if (checkBox.BackColor == Color.Transparent)
            {
                checkBox.BackColor = SystemColors.Control;
            }
        }

        private static void SetupComboBox(this ComboBox menu)
        {
        }

        private static void TouchBackColor(this Control c, bool rudely = false)
        {
            if (rudely)
            {
                var original = c.BackColor;
                c.BackColor = Color.Magenta;
                c.BackColor = original;
            }
            else
            {
                c.BackColor = c.BackColor;
            }
        }

        private static void TouchForeColor(this Control c, bool rudely = false)
        {
            if (rudely)
            {
                var original = c.ForeColor;
                c.ForeColor = Color.Magenta;
                c.ForeColor = original;
            }
            else
            {
                c.ForeColor = c.ForeColor;
            }
        }

        private static bool IsFirstTime(IWin32Window menu)
        {
            if (Ready.TryGetValue(menu, out _))
            {
                return false;
            }

            Ready.Add(menu, menu);
            return true;
        }
    }
}
