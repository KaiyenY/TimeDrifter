using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2dracer
{
    class MenuElement
    {
        /// <summary>
        /// Class for Buttons and things for the Menu
        /// </summary>
        

        //Fields
        public Rectangle position;
        public Texture2D texture;
        public float rotation; //Probably for the lever

        //Constructors
        public MenuElement(Rectangle position, Texture2D texture, float rotation)
        {
            this.position = position;
            this.texture = texture;
            this.rotation = rotation; 
        }

        public MenuElement(Rectangle position, Texture2D texture)
        {
            this.position = position;
            this.texture = texture;
            this.rotation = 0.0f;
        }

        public void Draw()//Draws to the screen
        {
            Game1.spriteBatch.Draw(texture, position, Color.White);
        }

        public void DrawWithText(string text) //Draws but with desired text in the middle
        {
            Draw();
            Game1.spriteBatch.DrawString(Game1.comicSans, text, new Vector2(position.Location.X + position.Width / 2, position.Location.Y + position.Height / 2), Color.White);
        }

        
    }
}
