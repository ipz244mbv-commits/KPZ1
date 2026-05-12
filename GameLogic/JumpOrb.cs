using System.Drawing;
using System.Windows.Forms;

namespace Geometry_Dash.GameLogic
{
    public class JumpOrb : Obstacle
    {
        public JumpOrb(int x, int y, Form form) : base(x, 20, Color.Transparent)
        {
            Box.Width = Box.Height = 20;
            Box.Left = x;
            Box.Top = y;

            Box.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                using SolidBrush fill = new(Color.Gold);
                e.Graphics.FillEllipse(fill, 0, 0, Box.Width - 1, Box.Height - 1);

                using Pen outline = new(Color.White, 2);
                e.Graphics.DrawEllipse(outline, 0, 0, Box.Width - 1, Box.Height - 1);
            };

            Box.BackColor = Color.Transparent;
            Box.Enabled = false;
        }
    }
}
