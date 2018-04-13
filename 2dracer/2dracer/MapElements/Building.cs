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
        private Texture2D tex;

        public Vector2 Position;

        public Building(Model m, String x, Vector2 p)
        {
            model = m;
            Position = p;
            
            BasicEffect effect = (BasicEffect)model.Meshes[0].Effects[0];
            effect.Texture = Program.game.Content.Load<Texture2D>(x);
        }

        public void Draw()
        {
            BasicEffect effect = (BasicEffect)model.Meshes[0].Effects[0];
            
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
