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

        public Building(Vector2 p)
        {
            model = Program.game.Content.Load<Model>("Models/untitled");
            Position = p;
        }

        public void Draw(Texture2D img)
        {
            BasicEffect effect = (BasicEffect)model.Meshes[0].Effects[0];
            effect.Texture = img;
            
            float aspectRatio = Game1.graphics.PreferredBackBufferWidth / (float)Game1.graphics.PreferredBackBufferHeight;
            float fieldOfView = MathHelper.PiOver4;

            int z = 5;

            effect.Projection = Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, 0.1f, 200);
            effect.View = Matrix.CreateTranslation(-1 * Game1.camera.Position.X / (58 * z), Game1.camera.Position.Y / (58 * z), (float)-10 / z);
            effect.World = Matrix.CreateTranslation(Position.X, Position.Y, 0);
            
            model.Meshes[0].Draw();
        }
    }
}
