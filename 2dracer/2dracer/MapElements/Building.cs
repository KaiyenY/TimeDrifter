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
            
            float fieldOfView = MathHelper.PiOver4;

            // This transfers 2d coordinates to 3d world space coordinates
            Vector3 modelPos = Options.Graphics.GraphicsDevice.Viewport.Unproject(
                new Vector3(Vector2.Subtract(Position, Game1.camera.Position), 0.97f),      // 97% between near and far planes
                effect.Projection,
                effect.View,
                Matrix.Identity);

            int z = 5;

            effect.Projection = Matrix.CreatePerspectiveFieldOfView(fieldOfView, Options.Graphics.GraphicsDevice.Viewport.AspectRatio, 0.1f, 200f);
            // effect.View = Matrix.CreateTranslation(-1 * Game1.camera.Position.X / (58 * z), Game1.camera.Position.Y / (58 * z), (float)-10 / z);
            // effect.World = Matrix.CreateTranslation(Position.X, Position.Y, 0f);

            // I worked out some code to make it look like the above, except we don't need
            // to hard code any numbers to get world coordinates. Not sure what the 2.3f is
            // but I'm thinking it's the height of the model. When it's set to 0 the model is
            // rendered with the 2d plane on the roofs. If you want to change the code back
            // just comment out these lines, uncomment the ones above, and changes the comments
            // in map where buildings are created.
            effect.View = Matrix.CreateLookAt(new Vector3(Vector2.Zero, 2.3f), Vector3.Zero, Vector3.Up);
            effect.World = Matrix.CreateTranslation(modelPos.X, modelPos.Y, 0f);

            model.Meshes[0].Draw();
        }
    }
}
