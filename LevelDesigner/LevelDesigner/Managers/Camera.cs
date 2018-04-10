using LevelDesigner.MapElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LevelDesigner.Managers
{
    public static class Camera
    {
        #region Fields
        private static int movespeed = 20;
        #endregion

        #region Properties
        public static Vector2 Position { get; private set; }

        public static Matrix ViewMatrix { get; private set; }
        #endregion

        #region Constructor
        static Camera()
        {
            Rectangle rect = Map.Rect;
        }
        #endregion

        #region Methods
        public static void Update()
        {
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

            if (Position.X < Map.Rect.Left - 384)
            {
                Position = new Vector2(Map.Rect.Left - 384, Position.Y);
            }
            else if (Position.X > Map.Rect.Right - Designer.screenWidth - 384)
            {
                Position = new Vector2(Map.Rect.Right - Designer.screenWidth - 384, Position.Y);
            }

            if (Position.Y < Map.Rect.Top - 384)
            {
                Position = new Vector2(Position.X, Map.Rect.Top - 384);
            }
            else if (Position.Y > Map.Rect.Bottom - Designer.screenHeight - 384)
            {
                Position = new Vector2(Position.X, Map.Rect.Bottom - Designer.screenHeight - 384);
            }

            ViewMatrix = Matrix.CreateTranslation(new Vector3(-Position, 0));
        }
        #endregion
    }
}

// -- Genoah but some of Niko's Code