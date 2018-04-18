using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using _2dracer.Managers;

namespace _2dracer.MapElements
{
    public class Building
    {
        private Model model;
        public Vector2 Position;

        public Building(Vector2 position)
        {
            model = Program.game.Content.Load<Model>("Models/untitled");
            Position = position;
        }

        public void Draw(Texture2D img)
        {
            BasicEffect effect = (BasicEffect)model.Meshes[0].Effects[0];
            effect.Texture = img;
            
            double fieldOfView = (3.14159 / 4) * Options.Graphics.GraphicsDevice.Viewport.Width/1500;

            // This transfers 2d coordinates to 3d world space coordinates
            Vector3 modelPos = Options.Graphics.GraphicsDevice.Viewport.Unproject(
                new Vector3(Vector2.Subtract(Position, Game1.camera.Position), 0.97f),      // 97% between near and far planes
                effect.Projection,
                effect.View,
                Matrix.Identity);

            int z = 5;

            effect.Projection = Matrix.CreatePerspectiveFieldOfView((float)fieldOfView, Options.Graphics.GraphicsDevice.Viewport.AspectRatio, 0.1f, 200f);
            effect.View = Matrix.CreateLookAt(new Vector3(Vector2.Zero, 2.3f), Vector3.Zero, Vector3.Up);
            effect.World = Matrix.CreateTranslation(modelPos.X, modelPos.Y, 0f);

            model.Meshes[0].Draw();
        }
    }
}
