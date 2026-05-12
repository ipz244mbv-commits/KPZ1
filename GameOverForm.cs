using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace Geometry_Dash
{
    public partial class GameOverForm : Form
    {
        private readonly Font customFont;
        private readonly int level;

        public GameOverForm(int progressPercent, int level)
        {
            InitializeComponent();
            this.level = level;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.MidnightBlue;
            ClientSize = new Size(800, 400);

            string fontPath = Path.Combine(Application.StartupPath, "Resources", "PressStart2P-Regular.ttf");
            customFont = File.Exists(fontPath)
                ? LoadCustomFont(fontPath)
                : new Font("Arial", 12);

            // 🔸 Назва
            Controls.Add(CreateLabel("ПОРАЗКА", 14, Color.Orange, new Point((ClientSize.Width - 130) / 2, 20)));

            // 🔸 Відображення % прогресу
            Controls.Add(new Label
            {
                Text = $"Прогрес: {progressPercent}%",
                Font = new Font(customFont.FontFamily, 16, FontStyle.Bold),
                ForeColor = Color.OrangeRed,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(ClientSize.Width, 30),
                Location = new Point(0, 75),
                BackColor = Color.Transparent
            });

            // 🔘 Повтор
            Controls.Add(CreateButton("ПОВТОР", new Point((ClientSize.Width - 120) / 2, 125), () =>
            {
                new Form1(level).Show();
                Close();
            }));

            // 🔘 Меню
            Controls.Add(CreateButton("МЕНЮ", new Point((ClientSize.Width - 120) / 2, 170), () =>
            {
                new StartMenuForm().Show();
                Close();
            }));
        }

        private Font LoadCustomFont(string path)
        {
            var pfc = new PrivateFontCollection();
            pfc.AddFontFile(path);
            return new Font(pfc.Families[0], 12, FontStyle.Bold);
        }

        private Label CreateLabel(string text, int fontSize, Color color, Point location, Size? size = null)
        {
            return new Label
            {
                Text = text,
                Font = new Font(customFont.FontFamily, fontSize),
                ForeColor = color,
                AutoSize = size is null,
                Size = size ?? Size.Empty,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = location
            };
        }

        private Button CreateButton(string text, Point location, Action onClick)
        {
            var button = new Button
            {
                Text = text,
                Size = new Size(120, 35),
                Location = location,
                BackColor = text == "МЕНЮ" ? Color.Maroon : Color.MediumPurple,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font(customFont.FontFamily, 10)
            };
            button.Click += (s, e) => onClick();
            return button;
        }
    }
}
