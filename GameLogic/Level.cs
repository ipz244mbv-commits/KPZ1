using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Geometry_Dash.GameLogic
{
    public interface LevelBuilder
    {
        List<Obstacle> Build(Form form);
        int Length { get; }

    }

    public static class Level
    {
        public static int LevelLength { get; private set; }

        public static List<Obstacle> LoadLevel(int levelNumber, Form form)
        {
            LevelBuilder builder = levelNumber switch
            {
                1 => new Level1(),
                2 => new Level2(),
                3 => new Level3(),
                4 => new Level4(),

                _ => throw new System.Exception("Невідомий рівень")
            };

            var obstacles = builder.Build(form);
            LevelLength = builder.Length;
            return obstacles;
        }
    }
}
