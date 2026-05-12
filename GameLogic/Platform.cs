using System.Drawing;
using System.Windows.Forms;

namespace Geometry_Dash.GameLogic
{
    public class Platform : Obstacle
    {
        public bool IsCeiling { get; set; } = false;

        // Звичайна платформа без offset
        public Platform(int x, Form form, int size, string imagePath)
            : this(x, form, size, imagePath, 0) { }

        // Платформа з offset (наприклад сходи)
        public Platform(int x, Form form, int size, string imagePath, int verticalOffset)
            : base(x, size, Color.Transparent)
        {
            Box.Width = size;
            Box.Height = size;
            Box.Left = x;

            int baseY = form.ClientSize.Height - Box.Height;
            Box.Top = baseY - verticalOffset;

            LoadImage(imagePath);
        }

        // Платформа на стелі
        public Platform(int x, Form form, int size, string imagePath, bool isCeiling)
            : base(x, size, Color.Transparent)
        {
            Box.Width = size;
            Box.Height = size;
            Box.Left = x;

            if (isCeiling)
            {
                Box.Top = 0;
                IsCeiling = true;
            }
            else
            {
                int baseY = form.ClientSize.Height - Box.Height;
                Box.Top = baseY;
            }

            LoadImage(imagePath);
        }

        private void LoadImage(string imagePath)
        {
            try
            {
                Image img = Image.FromFile(imagePath);
                Box.Image = img;
                Box.SizeMode = PictureBoxSizeMode.StretchImage;
                Box.BackColor = Color.Transparent;
            }
            catch
            {
                Box.BackColor = Color.Gray;
            }
        }
    }
}
