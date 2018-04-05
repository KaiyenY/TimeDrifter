using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LevelDesigner
{
    public static class Camera
    {
        #region Fields
        private static int movespeed = 10;
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

            ViewMatrix = Matrix.CreateTranslation(new Vector3(-Position, 0));
        }
        #endregion
    }
}

// -- Genoah but Niko's Code