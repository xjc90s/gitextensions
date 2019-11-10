using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GitUI.Theming
{
    public class HighQualityScope : IDisposable
    {
        private readonly Graphics _g;
        private readonly SmoothingMode _smoothing;
        private readonly PixelOffsetMode _offset;

        public HighQualityScope(Graphics g)
        {
            _g = g;
            _smoothing = g.SmoothingMode;
            _offset = g.PixelOffsetMode;

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.None;
        }

        public void Dispose()
        {
            _g.SmoothingMode = _smoothing;
            _g.PixelOffsetMode = _offset;
        }
    }
}
