using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Geometry_Dash.GameLogic
{
    public class Level1 : LevelBuilder
    {
        public int Length { get; private set; }


        public List<Obstacle> Build(Form form)
        {
            var obstacles = new List<Obstacle>();
            string platformImagePath = "Resources/platform_block.jpg";
            int x = 0;

            while (x < 2050)
            {
                obstacles.Add(new Platform(x, form, 30, platformImagePath));

                if (x % 300 == 0 && x > 0)
                    obstacles.Add(new Spike(x, form));

                x += 30;
            }

            for (int i = 0; i < 30; i++, x += 30)
                obstacles.Add(new Platform(x, form, 30, platformImagePath));

            for (int i = 0; i < 5; i++, x += 30)
                obstacles.Add(new Platform(x, form, 30, platformImagePath));

            Length = x;
            return obstacles;
        }
    }
}
