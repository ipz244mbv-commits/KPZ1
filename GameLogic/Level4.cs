using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Geometry_Dash.GameLogic
{
    public class Level4 : LevelBuilder
    {
        public int Length { get; private set; }

        public List<Obstacle> Build(Form form)
        {
            var obstacles = new List<Obstacle>();
            int x = 0;

            // 🟩 Основа — земля
            for (int g = 0; g < 3000; g += 30)
                obstacles.Add(new Platform(g, form, 30, "Resources/platform_block.jpg"));

            x = 400;
            int[] heights = { 20, 40, 60, 80 };
            for (int i = 0; i < heights.Length; i++)
            {
                int h = heights[i];
                obstacles.Add(new Platform(x, form, 40, "Resources/platform_block.jpg", h));
                x += 160;

                if (i == heights.Length - 1)
                    obstacles.Add(new JumpOrb(x - 25, form.ClientSize.Height - h - 100, form));
            }

            x += 100;
            obstacles.Add(new JumpOrb(x + 140, form.ClientSize.Height - 150, form));
            for (int i = 0; i < 6; i++)
            {
                obstacles.Add(new Spike(x, form));
                x += 60;
            }

            x += 300;

            // === 🌀 ТЕЛЕПОРТ: Знизу → стеля ===

            int portalEnterX = 2000;
            int portalEnterY = form.ClientSize.Height - 90; // вхід на підлозі

            int portalExitX = portalEnterX;
            int portalExitY = 30;
            int playerExitY = 60;

            // 🧱 Суцільна стеля
            int ceilingStartX = portalExitX;
            int ceilingWidth = 900;
            for (int i = 0; i < ceilingWidth; i += 30)
            {
                Platform top = new Platform(ceilingStartX + i, form, 30, "Resources/platform_block.jpg");
                top.Box.Top = 0;
                top.IsCeiling = true;
                obstacles.Add(top);
            }

            // 🌀 Активний телепорт (з точкою виходу гравця)
            TeleportPortal entryPortal = new TeleportPortal(
                portalEnterX, portalEnterY,
                new Point(portalExitX, playerExitY),
                invertGravity: true);
            obstacles.Add(entryPortal);

            // 🔵 Візуальний портал-вихід
            TeleportPortal exitVisual = new TeleportPortal(portalExitX, portalExitY, Point.Empty, false);
            exitVisual.Box.Enabled = true; // зробити видимим
            obstacles.Add(exitVisual);

            x += 600;

            // 🔫 Фінальні платформи
            x += 400;
            for (int i = 0; i < 500; i += 30)
                obstacles.Add(new Platform(x + i, form, 30, "Resources/platform_block.jpg"));

            x += 500;
            Length = x + 300;
            return obstacles;
        }
    }
}
