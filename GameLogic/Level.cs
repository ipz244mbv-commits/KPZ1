using System;
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

    // ВИРІШЕННЯ ISSUE 1 (DRY): Єдиний генератор рівнів, що усуває дублювання
    public class LevelGenerator : LevelBuilder
    {
        public int Length { get; private set; }
        private readonly Action<List<Obstacle>, Form> _generateLogic;

        public LevelGenerator(int length, Action<List<Obstacle>, Form> generateLogic)
        {
            Length = length;
            _generateLogic = generateLogic;
        }

        public List<Obstacle> Build(Form form)
        {
            var obstacles = new List<Obstacle>();
            _generateLogic(obstacles, form);
            return obstacles;
        }
    }

    public static class Level
    {
        public static int LevelLength { get; private set; }

        public static List<Obstacle> LoadLevel(int levelNumber, Form form)
        {
            LevelBuilder builder = levelNumber switch
            {
                1 => GetLevel1(),
                2 => GetLevel2(),
                3 => new Level3(), // Старі класи залишаємо, щоб не зламати 3 і 4 рівні
                4 => new Level4(),
                _ => throw new Exception("Невідомий рівень")
            };

            var obstacles = builder.Build(form);
            LevelLength = builder.Length;
            return obstacles;
        }

        private static LevelBuilder GetLevel1()
        {
            return new LevelGenerator(3100, (obstacles, form) =>
            {
                string path = "Resources/platform_block.jpg";
                int x = 0;
                while (x < 2050)
                {
                    obstacles.Add(new Platform(x, form, 30, path));
                    if (x % 300 == 0 && x > 0) obstacles.Add(new Spike(x, form));
                    x += 30;
                }
                for (int i = 0; i < 35; i++, x += 30) obstacles.Add(new Platform(x, form, 30, path));
            });
        }

        private static LevelBuilder GetLevel2()
        {
            return new LevelGenerator(3900, (obstacles, form) =>
            {
                string path = "Resources/platform_block.jpg";
                for (int g = 0; g < 4300; g += 30) obstacles.Add(new Platform(g, form, 30, path));
                
                int x = 390;
                obstacles.Add(new Platform(x, form, 40, path, 30)); x += 110;
                obstacles.Add(new Platform(x, form, 40, path, 70)); 
                obstacles.Add(new Platform(x, form, 40, path, 30)); x += 110;

                int step = 0;
                while (x < 1300)
                {
                    if (step != 3 && step != 4 && step != 8 && step != 9 && step != 12 && step != 13) 
                        obstacles.Add(new Platform(x, form, 40, path, 110));
                    x += 40; step++;
                }

                x = 600;
                while (x < 1500) { obstacles.Add(new Spike(x, form)); x+= 60; }
                obstacles.Add(new Spike(1500, form));

                x = 1900;
                foreach (int offset in new[] { 30, 75, 110, 190 })
                {
                    obstacles.Add(new Platform(x, form, 40, path, offset)); x += 130;
                }
                x -= 130;

                while (x < 3000) { obstacles.Add(new Platform(x, form, 40, path, 70)); x += 240; }
                
                x = 2000;
                while (x < 3000) { obstacles.Add(new Spike(x, form)); x += 60; }

                while (x < 3900)
                {
                    if (x % 500 == 0) obstacles.Add(new Spike(x + 20, form));
                    obstacles.Add(new Platform(x, form, 30, path)); x += 30;
                }
            });
        }
    }
}
