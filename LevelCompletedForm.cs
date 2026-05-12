using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace Geometry_Dash
{
    public partial class LevelCompletedForm : Form
    {
        private Font customFont = null!;
        private int level;

        public LevelCompletedForm(int level)
        {
            InitializeComponent();
            this.level = level;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.MidnightBlue;
            this.ClientSize = new Size(800, 400);
            this.Text = "Level Completed!";

            string fontPath = Path.Combine(Application.StartupPath, "Resources", "PressStart2P-Regular.ttf");
            Font customFont;
            if (!File.Exists(fontPath))
            {
                MessageBox.Show("Не знайдено шрифт: " + fontPath);
                customFont = new Font("Arial", 12);
            }
            else
            {
                PrivateFontCollection pfc = new PrivateFontCollection();
                pfc.AddFontFile(fontPath);
                customFont = new Font(pfc.Families[0], 12, FontStyle.Bold);
            }

            // 🟠 Заголовок
            Label title = new Label
            {
                Text = "ПЕРЕМОГА",
                Font = new Font(customFont.FontFamily, 14),
                ForeColor = Color.Orange,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Width = this.ClientSize.Width,
                Height = 40,
                Location = new Point(0, 20)
            };
            Controls.Add(title);

            // 🔵 Прогрес-бар (фон — бірюзовий)
            // 🔵 Прогрес-бар (фон — бірюзовий)
            Panel barBack = new Panel
            {
                BackColor = Color.Cyan,
                Size = new Size(200, 20),
                Location = new Point((ClientSize.Width - 200) / 2, 80)
            };
            Controls.Add(barBack);

            // 🔵 Прогрес-бар (заповнення — 100%)
            Panel barFill = new Panel
            {
                BackColor = Color.DeepSkyBlue,
                Size = new Size(200, 20),
                Location = new Point(0, 0)
            };
            barBack.Controls.Add(barFill);

            // 🟧 Надпис "100%" по центру шкали
            Label percentLabel = new Label
            {
                Text = "100%",
                Font = new Font(customFont.FontFamily, 16, FontStyle.Bold),
                ForeColor = Color.OrangeRed,
                AutoSize = false,
                Size = barBack.Size,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            barBack.Controls.Add(percentLabel);
            percentLabel.BringToFront();

            // 🔘 Кнопка ДАЛІ
            Button nextButton = new Button
            {
                Text = "ДАЛІ",
                Size = new Size(120, 35),
                Location = new Point((ClientSize.Width - 120) / 2, 125),
                BackColor = Color.MediumPurple,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font(customFont.FontFamily, 10)
            };
            nextButton.Click += (s, e) =>
            {
                if (level < 5)
                {
                    new Form1(level + 1).Show();
                }
                else
                {
                    MessageBox.Show("Це був останній рівень!");
                    new StartMenuForm().Show();
                }
                this.Close();
            };
            Controls.Add(nextButton);

            // 🔘 Кнопка МЕНЮ
            Button menuButton = new Button
            {
                Text = "МЕНЮ",
                Size = new Size(120, 35),
                Location = new Point((ClientSize.Width - 120) / 2, 170),
                BackColor = Color.Maroon,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font(customFont.FontFamily, 10)
            };
            menuButton.Click += (s, e) =>
            {
                StartMenuForm menu = new StartMenuForm();
                menu.Show();
                this.Close();
            };
            Controls.Add(menuButton);
        }
    }
}
