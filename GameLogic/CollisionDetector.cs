using System.Drawing;

namespace Geometry_Dash.GameLogic
{
    public static class CollisionDetector
    {
        // Перевірка зіткнення зі стіною (збоку)
        public static bool CheckSideCollision(Rectangle playerRect, Rectangle platRect, bool isGravityInverted)
        {
            return playerRect.Right > platRect.Left &&
                   playerRect.Left < platRect.Right &&
                   playerRect.Bottom > platRect.Top + 5 &&
                   playerRect.Top < platRect.Bottom - 5 &&
                   !(playerRect.Bottom >= platRect.Top - 10 && playerRect.Bottom <= platRect.Top + 10) &&
                   !(isGravityInverted && playerRect.Top >= platRect.Bottom - 10 && playerRect.Top <= platRect.Bottom + 10);
        }

        // Перевірка приземлення на платформу
        public static bool CheckGroundCollision(Player player, Platform platform)
        {
            Rectangle playerRect = player.Box.Bounds;
            Rectangle platRect = platform.Box.Bounds;

            if (!player.IsGravityInverted)
            {
                return playerRect.Bottom >= platRect.Top - 10 &&
                       playerRect.Bottom <= platRect.Top + 10 &&
                       playerRect.Right > platRect.Left + 5 &&
                       playerRect.Left < platRect.Right - 5 &&
                       player.VelocityY >= 0;
            }
            else
            {
                return playerRect.Top <= platRect.Bottom + 10 &&
                       playerRect.Top >= platRect.Bottom - 10 &&
                       playerRect.Right > platRect.Left + 5 &&
                       playerRect.Left < platRect.Right - 5 &&
                       player.VelocityY <= 0;
            }
        }
    }
}
