using System.Drawing;
using System.Windows.Forms;

namespace Geometry_Dash.GameLogic
{
    public class TeleportPortal : Obstacle
    {
        public Point Destination { get; }
        public bool InvertGravityAfterTeleport { get; }

        public TeleportPortal(int x, int y, Point destination, bool invertGravity) : base(x, 60, Color.Transparent)
        {
            Destination = destination;
            InvertGravityAfterTeleport = invertGravity;

            Box.Width = 30;
            Box.Height = 60;
            Box.Left = x;
            Box.Top = y;
            Box.BackColor = Color.FromArgb(50, Color.Cyan); // напівпрозорий    


            Box.Paint += DrawPortal;
        }

        private void DrawPortal(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Rectangle outer = new Rectangle(0, 0, Box.Width, Box.Height);
            Rectangle inner = new Rectangle(6, 10, Box.Width - 12, Box.Height - 20);

            using Pen ringPen = new Pen(Color.DeepSkyBlue, 3);
            using SolidBrush glow = new SolidBrush(Color.FromArgb(60, Color.Cyan));

            g.FillEllipse(glow, outer);
            g.DrawEllipse(ringPen, outer);
            g.DrawEllipse(ringPen, inner);
        }
    }
}
