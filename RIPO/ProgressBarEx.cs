using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace RIPO
{
    public class ProgressBarEx : ProgressBar
    {
        public ProgressBarEx()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.Opaque, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            ProgressBarRenderer.DrawHorizontalBar(e.Graphics, ClientRectangle);

            float width = (Width - 3) * ((float)Value / Maximum);
            if (width > 0)
            {
                var rect = new RectangleF(1, 1, width, Height - 3);
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                using (var brush = new LinearGradientBrush(rect, BackColor, ForeColor, LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
            }
        }
    }
}
