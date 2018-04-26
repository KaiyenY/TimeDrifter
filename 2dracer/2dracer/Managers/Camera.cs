using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using _2dracer.MapElements;


namespace _2dracer.Managers
{
    public class Camera
    {
        private Vector2 position;

        public Vector2 Position { get { return position; } }

        public Rectangle ViewRect { get; private set; }

        public Matrix ViewMatrix { get; set; }

        public void Update()
        {
            Vector2 pos = GameMaster.GameObjects[1].Position;
            Vector2 mapSize = Map.Size;
            float tileSize = Map.TileSize;
            ViewRect = new Rectangle(Position.ToPoint(), new Point(Options.ScreenWidth, Options.ScreenHeight));

            // Through complicated stuff, these two checks will make sure the
            // camera doesn't go off the map
            if (pos.X >= (Options.ScreenWidth / 2) - (tileSize / 2) &&
                pos.X <= -(Options.ScreenWidth / 2) + mapSize.X - (tileSize / 2))
            {
                position.X = pos.X - (Options.ScreenWidth / 2) + Player.playerVelocity.X / 5;
            }
            if (pos.Y >= (Options.ScreenHeight / 2) - (tileSize / 2) &&
                pos.Y <= -(Options.ScreenHeight / 2) + mapSize.Y - (tileSize / 2))
            {
                position.Y = pos.Y - (Options.ScreenHeight / 2) + Player.playerVelocity.Y / 5;
            }

            if (position.X < (Options.ScreenWidth / 2) - (tileSize / 2) - (Options.ScreenWidth / 2) + Player.playerVelocity.X / 5)
                position.X = (Options.ScreenWidth / 2) - (tileSize / 2) - (Options.ScreenWidth / 2) + Player.playerVelocity.X / 5;

            if (position.X > (Options.ScreenWidth / 2) + mapSize.X - (tileSize / 2) - (Options.ScreenWidth / 2) + Player.playerVelocity.X / 5)
                position.X = (Options.ScreenWidth / 2) + mapSize.X - (tileSize / 2) - (Options.ScreenWidth / 2) + Player.playerVelocity.X / 5;

            if (position.Y < (Options.ScreenHeight / 2) - (tileSize / 2) - (Options.ScreenHeight / 2) + Player.playerVelocity.Y / 5)
                position.Y = (Options.ScreenHeight / 2) - (tileSize / 2) - (Options.ScreenHeight / 2) + Player.playerVelocity.Y / 5;

            if (position.Y > (Options.ScreenHeight / 2) + mapSize.Y - (tileSize / 2) - (Options.ScreenHeight / 2) + Player.playerVelocity.Y / 5)
                position.Y = (Options.ScreenHeight / 2) + mapSize.Y - (tileSize / 2) - (Options.ScreenHeight / 2) + Player.playerVelocity.Y / 5;

            ViewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));
        }
    }
}

// Niko Procopi