using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GitUI;

namespace GitExtUtils.GitUI.Theming
{
    internal class TabControlRenderer
    {
        private readonly TabControl _tabs;

        public TabControlRenderer(TabControl tabs)
        {
            _tabs = tabs;
        }

        private static int ImagePadding { get; } = (int)Math.Round(DpiUtil.Scale(6f));
        private static int SelectedTabPadding { get; } = (int)Math.Round(DpiUtil.Scale(2f));
        private static int BorderWidth { get; } = (int)Math.Round(DpiUtil.Scale(1f));

        public void Setup()
        {
            _tabs.SetStyle(ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            _tabs.Paint += HandlePaint;
            _tabs.Disposed += HandleDisposed;
        }

        private void HandlePaint(object s, PaintEventArgs e)
        {
            using (var canvasBrush = new SolidBrush(_tabs.Parent.BackColor))
            {
                e.Graphics.FillRectangle(canvasBrush, e.ClipRectangle);
            }

            RenderSelectedPageBackground(_tabs, e);

            var pageIndices = Enumerable.Empty<int>()
                .Concat(Enumerable.Range(0, _tabs.SelectedIndex))
                .Concat(Enumerable.Range(_tabs.SelectedIndex + 1, _tabs.TabCount - (_tabs.SelectedIndex + 1)).Reverse())
                .Concat(Enumerable.Range(_tabs.SelectedIndex, Math.Min(1, _tabs.TabCount)));

            foreach (var index in pageIndices)
            {
                RenderTabBackground(index, e);
                RenderTabImage(index, e);
                RenderTabText(index, GetTabImage(index) != null, e);
            }
        }

        private void RenderTabBackground(int index, PaintEventArgs e)
        {
            using (var borderPen = GetBorderPen())
            {
                var outerRect = GetOuterTabRect(index);
                e.Graphics.FillRectangle(GetBackgroundBrush(index), outerRect);

                var points = new List<Point>(4);
                if (index <= _tabs.SelectedIndex)
                {
                    points.Add(new Point(outerRect.Left, outerRect.Bottom - 1));
                }

                points.Add(new Point(outerRect.Left, outerRect.Top));
                points.Add(new Point(outerRect.Right - 1, outerRect.Top));

                if (index >= _tabs.SelectedIndex)
                {
                    points.Add(new Point(outerRect.Right - 1, outerRect.Bottom - 1));
                }

                e.Graphics.DrawLines(borderPen, points.ToArray());
            }
        }

        private void RenderTabImage(int index, PaintEventArgs e)
        {
            var image = GetTabImage(index);
            if (image == null)
            {
                return;
            }

            var imgRect = GetTabImageRect(_tabs, index);
            e.Graphics.DrawImage(image, imgRect);
        }

        private Rectangle GetTabImageRect(TabControl tabs, int index)
        {
            var page = tabs.TabPages[index];
            var innerRect = tabs.GetTabRect(index);
            int imgHeight = tabs.ImageList.ImageSize.Height;
            var imgRect = new Rectangle(
                new Point(innerRect.X + ImagePadding,
                    innerRect.Y + ((innerRect.Height - imgHeight) / 2)),
                tabs.ImageList.ImageSize);

            if (page == tabs.SelectedTab)
            {
                imgRect.Offset(0, -SelectedTabPadding);
            }

            return imgRect;
        }

        private Image GetTabImage(int index)
        {
            var images = _tabs.ImageList?.Images;
            if (images == null)
            {
                return null;
            }

            var page = _tabs.TabPages[index];
            if (!string.IsNullOrEmpty(page.ImageKey))
            {
                return images[page.ImageKey];
            }

            if (page.ImageIndex.IsWithin(0, images.Count))
            {
                return images[page.ImageIndex];
            }

            return null;
        }

        private void RenderTabText(int index, bool hasImage, PaintEventArgs e)
        {
            var page = _tabs.TabPages[index];
            if (string.IsNullOrEmpty(page.Text))
            {
                return;
            }

            var textRect = GetTabTextRect(index, hasImage);

            const TextFormatFlags format =
                TextFormatFlags.NoClipping |
                TextFormatFlags.NoPrefix |
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.HorizontalCenter;

            var textColor = _tabs.Enabled
                ? page == _tabs.SelectedTab
                    ? SystemColors.WindowText
                    : SystemColors.ControlText
                : SystemColors.GrayText;

            TextRenderer.DrawText(e.Graphics, page.Text, _tabs.Font, textRect, textColor, format);
        }

        private Rectangle GetTabTextRect(int index, bool hasImage)
        {
            var innerRect = _tabs.GetTabRect(index);
            Rectangle textRect;
            if (hasImage)
            {
                int deltaWidth = _tabs.ImageList.ImageSize.Width + ImagePadding;
                textRect = new Rectangle(
                    innerRect.X + deltaWidth,
                    innerRect.Y,
                    innerRect.Width - deltaWidth,
                    innerRect.Height);
            }
            else
            {
                textRect = innerRect;
            }

            if (index == _tabs.SelectedIndex)
            {
                textRect.Offset(0, -SelectedTabPadding);
            }

            return textRect;
        }

        private Rectangle GetOuterTabRect(int index)
        {
            var innerRect = _tabs.GetTabRect(index);

            if (index == _tabs.SelectedIndex)
            {
                return Rectangle.FromLTRB(
                    innerRect.Left - SelectedTabPadding,
                    innerRect.Top - SelectedTabPadding,
                    innerRect.Right + SelectedTabPadding,
                    innerRect.Bottom + 1); // +1 to overlap tabs bottom line
            }

            return Rectangle.FromLTRB(
                innerRect.Left,
                innerRect.Top + 1,
                innerRect.Right,
                innerRect.Bottom);
        }

        private void RenderSelectedPageBackground(TabControl tabs, PaintEventArgs e)
        {
            var tabRect = tabs.GetTabRect(tabs.SelectedIndex);
            var pageRect = Rectangle.FromLTRB(0, tabRect.Bottom, tabs.Width - 1,
                tabs.Height - 1);

            if (!e.ClipRectangle.IntersectsWith(pageRect))
            {
                return;
            }

            e.Graphics.FillRectangle(GetBackgroundBrush(tabs.SelectedIndex), pageRect);
            using (var borderPen = GetBorderPen())
            {
                e.Graphics.DrawRectangle(borderPen, pageRect);
            }
        }

        private Brush GetBackgroundBrush(int index)
        {
            if (index == _tabs.SelectedIndex)
            {
                return _tabs.SelectedTab.BackColor == Color.Transparent
                    ? SystemBrushes.Window
                    : new SolidBrush(_tabs.SelectedTab.BackColor);
            }

            var mouseCursor = _tabs.PointToClient(Cursor.Position);
            bool isHighlighted = _tabs.GetTabRect(index).Contains(mouseCursor);

            return isHighlighted
                ? SystemBrushes.ControlLightLight
                : SystemBrushes.Control;
        }

        private Pen GetBorderPen() =>
            _tabs.Enabled
                ? new Pen(SystemBrushes.ActiveBorder, BorderWidth)
                : new Pen(SystemBrushes.InactiveBorder, BorderWidth);

        private void HandleDisposed(object sender, EventArgs e)
        {
            _tabs.Paint -= HandlePaint;
            _tabs.Disposed -= HandleDisposed;
        }
    }
}
