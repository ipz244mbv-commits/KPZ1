using System.Drawing;
using System.Windows.Forms;

namespace Geometry_Dash.GameLogic
{
    public class Spike : Obstacle
    {
        public Spike(int x, Form form) : base(x, 30, Color.Transparent)
        {
            Box.Width = 30;
            Box.Height = 30;
            Box.Left = x;

            // ✔️ Шип стоїть точно на платформі
            Box.Top = form.ClientSize.Height - 30 - Box.Height;

            Box.Paint += (s, e) =>
            {
                Point[] triangle = {
                    new Point(0, Box.Height),
                    new Point(Box.Width / 2, 0),
                    new Point(Box.Width, Box.Height)
                };

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                using SolidBrush fillBrush = new SolidBrush(Color.Black);
                e.Graphics.FillPolygon(fillBrush, triangle);

                using var outlineBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    Box.ClientRectangle, Color.White, Color.MediumPurple,
                    System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal);

                using Pen outlinePen = new Pen(outlineBrush, 2);
                e.Graphics.DrawPolygon(outlinePen, triangle);
            };

            Box.BackColor = Color.Transparent;
        }
    }
}


