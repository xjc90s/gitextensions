using System.Drawing;

namespace GitUI.Theming
{
    internal static class RectangleExtension
    {
        public static Rectangle InclusiveRect(this Rectangle rect) =>
            Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right - 1, rect.Bottom - 1);
    }
}
