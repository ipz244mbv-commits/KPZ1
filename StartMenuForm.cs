using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace Geometry_Dash
{
    public partial class StartMenuForm : Form
    {
        private readonly Font customFont;

        public StartMenuForm()
        {
            InitializeComponent();
            customFont = LoadCustomFont();
            StartPosition = FormStartPosition.CenterScreen;
            InitializeMenu();
        }

        private Font LoadCustomFont()
        {
            string fontPath = Path.Combine(Application.StartupPath, "Resources", "PressStart2P-Regular.ttf");

            if (!File.Exists(fontPath))
            {
                MessageBox.Show("Шрифт не знайдено: " + fontPath, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new Font("Arial", 12, FontStyle.Bold);
            }

            var fonts = new PrivateFontCollection();
            fonts.AddFontFile(fontPath);
            return new Font(fonts.Families[0], 12, FontStyle.Bold);
        }

        private void InitializeMenu()
        {
            Text = "Geometry Dash - Menu";
            Width = 800;
            Height = 440;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            BackColor = Color.MidnightBlue;

            Controls.Add(CreateLabel("GEOMETRY DASH", 14, Color.Orange, new Point(0, 20), new Size(ClientSize.Width, 40)));

            int buttonWidth = 140;
            int buttonHeight = 38;
            int spacing = 12;
            int totalButtons = 6;
            int totalHeight = totalButtons * buttonHeight + (totalButtons - 1) * spacing;
            int startY = (ClientSize.Height - totalHeight) / 2 + 20;

            for (int i = 1; i <= 5; i++)
            {
                int level = i;
                string buttonText = level == 1 ? "ПОЧАТОК" : $"РІВЕНЬ {level}";
                int width = level == 1 ? buttonWidth + 20 : buttonWidth;
                Point location = new Point((ClientSize.Width - width) / 2, startY + (level - 1) * (buttonHeight + spacing));

                Controls.Add(CreateButton(buttonText, location, width, () =>
                {
                    new Form1(level).Show();
                    Hide();
                }));
            }

            Point exitLocation = new Point((ClientSize.Width - buttonWidth) / 2, startY + 5 * (buttonHeight + spacing));
            Controls.Add(CreateButton("ВИХІД", exitLocation, buttonWidth, () => Application.Exit()));
        }

        private Label CreateLabel(string text, int fontSize, Color color, Point location, Size size)
        {
            return new Label
            {
                Text = text,
                Font = new Font(customFont.FontFamily, fontSize, FontStyle.Bold),
                ForeColor = color,
                AutoSize = false,
                Size = size,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = location
            };
        }

        private Button CreateButton(string text, Point location, int width, Action onClick)
        {
            var button = new Button
            {
                Text = text,
                Width = width,
                Height = 38,
                Font = new Font(customFont.FontFamily, 10),
                Location = location,
                BackColor = text == "ВИХІД" ? Color.DarkRed : Color.MediumPurple,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Padding = new Padding(4),
                TextAlign = ContentAlignment.MiddleCenter
            };
            button.Click += (s, e) => onClick();
            return button;
        }
    }
} 
