using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Geometry_Dash.GameLogic;
using WinFormsTimer = System.Windows.Forms.Timer;

namespace Geometry_Dash
{
    public partial class Form1 : Form
    {
        private Player player = null!;
        private List<Obstacle> currentObstacles = new();
        private readonly int levelNumber;
        private bool wasInPortal = false;

        private bool isFinishing = false;
        private int gameSpeed = 6;
        private int tickCounter = 0;
        private int distanceTraveled = 0;

        private readonly Color[] bgColors = { Color.DarkBlue, Color.DarkRed, Color.Black, Color.DarkGreen };
        private int colorIndex = 0;

        private WinFormsTimer mainGameTimer = new();
        private WinFormsTimer finishFadeTimer = new();

        public Form1(int level = 1)
        {
            InitializeComponent();
            levelNumber = level;
            DoubleBuffered = true;
            StartPosition = FormStartPosition.CenterScreen;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            player = new Player(Controls, ClientSize);
            currentObstacles = Level.LoadLevel(levelNumber, this);
            foreach (var obs in currentObstacles)
                Controls.Add(obs.Box);

            mainGameTimer.Interval = GetLevelInterval(levelNumber);
            mainGameTimer.Tick += GameLoop;
            mainGameTimer.Start();
        }

        private int GetLevelInterval(int level)
        {
            return level switch
            {
                1 => 16,
                2 => 12,
                3 => 10,
                4 => 8,
                _ => 16
            };
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                player.Jump();
        }

        private void GameLoop(object? sender, EventArgs e)
        {
            tickCounter++;

            if (isFinishing)
            {
                player.Box.Top += (int)player.VelocityY;
                player.Box.Left += 6;

                if (player.VelocityY > -20)
                    player.VelocityY -= 0.5f;

                if (player.Box.Bottom < 0 || player.Box.Left > ClientSize.Width)
                {
                    mainGameTimer.Stop();
                    new LevelCompletedForm(levelNumber).Show();
                    Hide();
                }
                return;
            }

            player.ApplyGravity();
            distanceTraveled += gameSpeed;

            player.HandleRotation();
            player.IsGrounded = false;

            bool touchedOrbThisFrame = false;

            foreach (var obs in currentObstacles)
            {
                if (!isFinishing || obs is not Platform)
                    obs.Box.Left -= gameSpeed;

                if (obs is Platform platform)
                {
                    CheckGroundCollision(platform);

                    if (platform.IsCeiling)
                        continue;

                    Rectangle playerRect = player.Box.Bounds;
                    Rectangle platRect = platform.Box.Bounds;

                    bool sideCollision =
                        playerRect.Right > platRect.Left &&
                        playerRect.Left < platRect.Right &&
                        playerRect.Bottom > platRect.Top + 5 &&
                        playerRect.Top < platRect.Bottom - 5 &&
                        !(playerRect.Bottom >= platRect.Top - 10 && playerRect.Bottom <= platRect.Top + 10) &&
                        !(player.IsGravityInverted && playerRect.Top >= platRect.Bottom - 10 && playerRect.Top <= platRect.Bottom + 10);

                    if (sideCollision)
                    {
                        int progress = CalculateProgressPercent();
                        mainGameTimer.Stop();
                        new GameOverForm(progress, levelNumber).Show();
                        Hide();
                        return;
                    }
                }

                if (obs is JumpOrb orb && player.Box.Bounds.IntersectsWith(orb.Box.Bounds))
                {
                    touchedOrbThisFrame = true;
                }

                if (obs is TeleportPortal tp)
                {
                    Rectangle expandedPortal = tp.Box.Bounds;
                    expandedPortal.Inflate(5, 5);

                    if (!wasInPortal && expandedPortal.IntersectsWith(player.Box.Bounds))
                    {
                        MessageBox.Show("-> Готові викликати метод");
                        try
                        {
                            DebugTeleportPlayer(tp);
                            MessageBox.Show("✔ Метод викликаний без помилок");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"❌ Виняток: {ex.Message}");
                        }
                    }

                }

                if (obs is Spike && player.Box.Bounds.IntersectsWith(obs.Box.Bounds))
                {
                    int progress = CalculateProgressPercent();
                    mainGameTimer.Stop();
                    new GameOverForm(progress, levelNumber).Show();
                    Hide();
                    return;
                }
            }

            // 🔄 Скидаємо wasInPortal, якщо гравець вийшов з порталу
            if (!currentObstacles.Exists(o => o is TeleportPortal p && player.Box.Bounds.IntersectsWith(p.Box.Bounds)))
            {
                wasInPortal = false;
            }

            player.IsTouchingOrb = touchedOrbThisFrame;

            int finishThreshold = levelNumber switch
            {
                1 => 75,
                2 => 85,
                3 => 98,
                4 => 98,
                _ => 75
            };

            if (!isFinishing && CalculateProgressPercent() >= finishThreshold)
            {
                StartFinishSequence();
            }

            if (!player.IsGravityInverted && player.Box.Top >= 330)
            {
                player.Box.Top = 330;
                player.IsGrounded = true;
                player.VelocityY = 0;
            }
            else if (player.IsGravityInverted && player.Box.Top <= 60)
            {
                player.Box.Top = 60;
                player.IsGrounded = true;
                player.VelocityY = 0;

                MessageBox.Show(
                $"STOP at ceiling\nTop = {player.Box.Top}, Velocity = {player.VelocityY}"
                );

            }
            if (tickCounter % 200 == 0)
            {
                BackColor = bgColors[++colorIndex % bgColors.Length];
                gameSpeed++;
            }
        }



        private void CheckGroundCollision(Platform platform)
        {
            Rectangle playerRect = player.Box.Bounds;
            Rectangle platRect = platform.Box.Bounds;

            bool standingOn = false;

            if (!player.IsGravityInverted)
            {
                // Звичайна гравітація — стоїмо зверху платформи
                standingOn =
                    playerRect.Bottom >= platRect.Top - 10 &&
                    playerRect.Bottom <= platRect.Top + 10 &&
                    playerRect.Right > platRect.Left + 5 &&
                    playerRect.Left < platRect.Right - 5 &&
                    player.VelocityY >= 0;

                if (standingOn)
                {
                    player.IsGrounded = true;
                    player.VelocityY = 0;
                    player.Box.Top = platRect.Top - player.Box.Height;
                }
            }
            else
            {
                // Інверсована гравітація — стоїмо ПІД платформою
                standingOn =
                    playerRect.Top <= platRect.Bottom + 10 &&
                    playerRect.Top >= platRect.Bottom - 10 &&
                    playerRect.Right > platRect.Left + 5 &&
                    playerRect.Left < platRect.Right - 5 &&
                    player.VelocityY <= 0;

                if (standingOn)
                {
                    player.IsGrounded = true;
                    player.VelocityY = 0;
                    player.Box.Top = platRect.Bottom;
                }
            }
        }

        private void StartFinishSequence()
        {
            isFinishing = true;
            finishFadeTimer.Interval = 40;
            finishFadeTimer.Tick += FadeOutWindow;
            finishFadeTimer.Start();
        }

        private void FadeOutWindow(object? sender, EventArgs e)
        {
            this.Opacity -= 0.03;
            if (this.Opacity <= 0.1)
            {
                finishFadeTimer.Stop();
                this.Opacity = 1.0;
                player.VelocityY = -5;
            }
        }

        private int CalculateProgressPercent()
        {
            int levelLength = Level.LevelLength;
            if (levelLength <= 0) return 0;

            int adjusted = distanceTraveled + 500;
            int percent = (int)((adjusted / (float)levelLength) * 100);
            return Math.Clamp(percent, 0, 100);
        }

        private void DebugTeleportPlayer(TeleportPortal tp)
        {
            try
            {
                MessageBox.Show("🟡 Увійшли в TeleportPlayer");

                this.BackColor = Color.LimeGreen; // ⬅️ Робимо форму зеленою

                player.Box.Left = tp.Destination.X;

                if (tp.InvertGravityAfterTeleport)
                    player.IsGravityInverted = !player.IsGravityInverted;

                player.Box.Top = 60;

                player.VelocityY = 0;
                player.IsGrounded = true;
                wasInPortal = true;

                player.Box.BackColor = Color.Red;
                player.Box.Visible = true;
                player.Box.BringToFront();

                if (player.Box.Parent == null)
                {
                    this.Controls.Add(player.Box);
                    MessageBox.Show("Box був відсутній, додано знову");
                }

                MessageBox.Show("🟢 Вийшли з TeleportPlayer");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Виняток усередині методу: {ex.Message}");
            }
        }

    }
}
