using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2dracer
{
    /// <summary>
    /// Class for Buttons and things for the Menu
    /// </summary>
    public class MenuElement
    {
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

        /// <summary>
        /// Draws the menuElement with text in the middle
        /// </summary>
        /// <param name="text">Text to draw to the middle of the element</param>
        public void DrawWithText(SpriteFont font, string text, Color color)
        {
            //TODO: Find out why not drawing to the exact centre of the object
            Draw();
            int middleXCoord = position.Center.X - (int)font.MeasureString(text).X;
            int middleYCoord = position.Center.Y - (int)font.MeasureString(text).Y;
            Game1.spriteBatch.DrawString(font, text, new Vector2(middleXCoord, middleYCoord), color);
        }

        /// <summary>
        /// Checks to see if user left clicks the button
        /// </summary>
        public bool IsClicked()
        {
            if(Input.MouseReleased(MouseButton.Left)) 
            {
                return Input.MouseReleased(MouseButton.Left, position);
            }
            else
            {
                return false;
            }
        }
        
    }
}
