using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Geometry_Dash.GameLogic
{
    public class Level2 : LevelBuilder
    {
        public int Length { get; private set; }

        public List<Obstacle> Build(Form form)
        {
            var obstacles = new List<Obstacle>();
            int playerSpawnX = 100;

            // 🟩 Основа — довга платформа під усією картою
            for (int g = 0; g < 4300; g += 30)
                obstacles.Add(new Platform(g, form, 30, "Resources/platform_block.jpg"));

            // 🟣 Сходи з фейкових платформ (починаються далі)
            int x = 390;
            obstacles.Add(new Platform(x, form, 40, "Resources/platform_block.jpg", 30)); x += 110;
            obstacles.Add(new Platform(x, form, 40, "Resources/platform_block.jpg", 70)); 
            obstacles.Add(new Platform(x, form, 40, "Resources/platform_block.jpg", 30)); x += 110;

            int step = 0;
            while (x < 1300)
            {
                if (step != 3 && step != 4 && step != 8 && step != 9 && step != 12 && step != 13) 
                {
                    obstacles.Add(new Platform(x, form, 40, "Resources/platform_block.jpg", 110));
                }
                x += 40;
                step++;
            }

            x = 600;
            // 🔺 Шипи (розріджені)            
            while (x < 1500)
            {
                obstacles.Add(new Spike(x, form));
                x+= 60; 
            }

            x = 1500;
            obstacles.Add(new Spike(x, form));

            x = 1900;
            // 🟦 Сходи вгору
            int[] offsets = { 30, 75, 110, 190 };
            foreach (int offset in offsets)
            {
                obstacles.Add(new Platform(x, form, 40, "Resources/platform_block.jpg", offset));
                x += 130;
            }
            x -= 130;

            while (x < 3000)
            {
                obstacles.Add(new Platform(x, form, 40, "Resources/platform_block.jpg", 70));
                x += 240;
            }

            x = 2000;
            // 🔺 Шипи (розріджені)            
            while (x < 3000)
            {
                obstacles.Add(new Spike(x, form));
                x += 60;
            }

            // 🔁 Платформи до фінішу
            while (x < 3900)
            {
                if (x % 500 == 0)
                    obstacles.Add(new Spike(x + 20, form));
                obstacles.Add(new Platform(x, form, 30, "Resources/platform_block.jpg"));
                x += 30;
            }

            Length = 3900;
            return obstacles;
        }
    }
}