using System.Drawing;
using System.Linq;
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
            container.FindDescendantsOfType<GroupBox>()
                .Where(IsFirstTime)
                .ForEach(SetupGroupBox);

            container.FindDescendantsOfType<Panel>()
                .Where(IsFirstTime)
                .ForEach(SetupPanel);
            container.FindDescendantsOfType<TabControl>()
                .Where(IsFirstTime)
                .ForEach(SetupTabControl);
            container.FindDescendantsOfType<TreeView>()
                .Where(IsFirstTime)
                .ForEach(SetupTreeView);
            container.FindDescendantsOfType<DataGridView>()
                .Where(IsFirstTime)
                .ForEach(SetupDataGridView);
            container.FindDescendantsOfType<ButtonBase>()
                .Where(IsFirstTime)
                .ForEach(SetupButtonBase);
            container.FindDescendantsOfType<ComboBox>()
                .Where(IsFirstTime)
                .ForEach(SetupComboBox);
            container.FindDescendantsOfType<ListView>()
                .Where(IsFirstTime)
                .ForEach(SetupListView);
            container.FindDescendantsOfType<TextBoxBase>()
                .Where(IsFirstTime)
                .ForEach(SetupTextBoxBase);
            container.FindDescendantsOfType<LinkLabel>()
                .Where(IsFirstTime)
                .ForEach(SetupLinkLabel);
            container.FindDescendantsOfType<ToolStrip>()
                .Where(IsFirstTime)
                .ForEach(SetupToolStrip);
        }

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
            box.ForeColor = box.ForeColor;
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
                page.BackColor = page.BackColor;
            }
        }

        private static void SetupTreeView(TreeView view)
        {
            var unused = view.Handle; // force handle creation
            view.BackColor = view.BackColor;
            view.ForeColor = view.ForeColor;
            if (view.LineColor == Color.Black || view.LineColor == Color.Empty)
            {
                view.LineColor = SystemColors.WindowText;
            }
            else if (view.LineColor.IsKnownColor)
            {
                view.LineColor = view.LineColor;
            }

            // semi-transparent images are blended multiple times
            // when toggling HotTrack state multiple times
            view.HotTracking = false;
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
            menu.FlatStyle = PreferredFlatStyle;
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
