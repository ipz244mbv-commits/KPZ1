using System.Drawing;
using System.Windows.Forms;

namespace Geometry_Dash.GameLogic
{
    public class Obstacle
    {
        public PictureBox Box { get; protected set; }

        public Obstacle(int x, int height, Color color)
        {
            Box = new PictureBox();
            Box.Width = 30;
            Box.Height = height;
            Box.Left = x;
            Box.Top = 0;
            Box.BackColor = color;
        }
    }
}
