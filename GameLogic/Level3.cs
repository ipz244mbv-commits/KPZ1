using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Geometry_Dash.GameLogic
{
    public class Level3 : LevelBuilder
    {
        public int Length { get; private set; }

        public List<Obstacle> Build(Form form)
        {
            var obstacles = new List<Obstacle>();
            int x = 0;

            // 🟩 Основа — довга платформа під усією картою
            for (int g = 0; g < 6000; g += 30)
                obstacles.Add(new Platform(g, form, 30, "Resources/platform_block.jpg"));

            // 🟣 Початкові сходи з шипами
            x = 300;
            int[] heights = { 20, 30, 40, 30, 20 };
            foreach (int h in heights)
            {
                obstacles.Add(new Platform(x, form, 40, "Resources/platform_block.jpg", h));
                x += 80;
                obstacles.Add(new Spike(x, form));
                x += 120;
            }

            // 🌕 Орби нижче з ще більшою відстанню
            // 🌕 Орби в центрі кожної групи
            x = 1700;
            for (int i = 0; i < 3; i++)
            {
                int groupCenter = x + 50;
                obstacles.Add(new JumpOrb(groupCenter, form.ClientSize.Height - 40 - 80, form));
                x += 300;
            }

            // 🔺 Розріджені шипи під усім блоком, без прогалин
            int spikeStartX = 1700;
            int spikeEndX = x; // значення після циклу орб
            for (int spikeX = spikeStartX; spikeX < spikeEndX; spikeX += 25) // крок 25 = розріджено
            {
                obstacles.Add(new Spike(spikeX, form));
            }

            // 🟦 Розріджені шипи після орб
            for (int i = 0; i < 5; i++)
            {
                x += 250;
                obstacles.Add(new Spike(x, form));
            }

            // 🔁 Завершення


            Length = 4500;
            return obstacles;
        }
    }
}