using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LevelDesigner
{
    public static class Camera
    {
        private static int movespeed = 10;

        public static Vector2 Position { get; private set; }

        public static Matrix ViewMatrix { get; private set; }

        public static void Update()
        {
            Rectangle rect = Map.Rect;

            if (Input.KeyHold(Keys.W))
            {
                Position = new Vector2(
                    Position.X,
                    Position.Y - movespeed);
            }
            if (Input.KeyHold(Keys.S))
            {
                Position = new Vector2(
                    Position.X,
                    Position.Y + movespeed);
            }
            if (Input.KeyHold(Keys.A))
            {
                Position = new Vector2(
                    Position.X - movespeed,
                    Position.Y);
            }
            if (Input.KeyHold(Keys.D))
            {
                Position = new Vector2(
                    Position.X + movespeed,
                    Position.Y);
            }

            ViewMatrix = Matrix.CreateTranslation(new Vector3(-Position, 0));
        }
    }
}
