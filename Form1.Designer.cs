using System.Drawing;
using System.Windows.Forms;

namespace Geometry_Dash
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Timer gameTimer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            gameTimer = new System.Windows.Forms.Timer(components); // створення таймера



            // gameTimer
            gameTimer.Interval = 20;
            gameTimer.Tick += GameLoop;

            // Form1
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.DarkBlue;
            ClientSize = new Size(785, 362);
            MaximumSize = new Size(801, 401);
            MinimumSize = new Size(800, 400);
            Name = "Form1";
            Text = "Geometry Dash";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;

        }

        #endregion
    }
}
