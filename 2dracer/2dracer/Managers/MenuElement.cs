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
        //Properties
        public Rectangle Position { get; set; }
        public Texture2D ActiveTexture { get; set; }

        //Fields
        private Texture2D idleTexture;
        private Texture2D pressedTexture;
        public float rotation; //Probably for the lever



        //Constructors
        public MenuElement(Rectangle position, Texture2D idleTexture, Texture2D pressedTexture, float rotation)
        {
            this.Position = position;
            this.idleTexture = idleTexture;
            this.pressedTexture = pressedTexture;
            this.ActiveTexture = this.idleTexture;
            this.rotation = rotation; 
        }

        public MenuElement(Rectangle position, Texture2D idleTexture, Texture2D pressedTexture)
        {
            this.Position = position;
            this.idleTexture = idleTexture;
            this.pressedTexture = pressedTexture;
            this.ActiveTexture = this.idleTexture;
            this.rotation = 0.0f;
        }

        public void Draw()//Draws to the screen
        {
            Game1.spriteBatch.Draw(ActiveTexture, Position, Color.White);
        }

        /// <summary>
        /// Draws the menuElement with text in the middle
        /// </summary>
        /// <param name="text">Text to draw to the middle of the element</param>
        public void DrawWithText(SpriteFont font, string text, Color color)
        {
            //TODO: Find out why not drawing to the exact centre of the object
            Draw();
            int middleXCoord = this.Position.Center.X - (int)font.MeasureString(text).X;
            int middleYCoord = this.Position.Center.Y - (int)font.MeasureString(text).Y;
            Game1.spriteBatch.DrawString(font, text, new Vector2(middleXCoord, middleYCoord), color);
        }

        /// <summary>
        /// Checks to see if user left clicks the button
        /// </summary>
        public bool IsClicked()
        {
            if(Input.MouseHold(MouseButton.Left, this.Position))
            {
                ActiveTexture = pressedTexture; //Change the texture to the pressed version
                
                return false;
            }
            if(Input.MouseReleased(MouseButton.Left, this.Position))
            {
                ActiveTexture = idleTexture; //Change it back
                return true;
            }

            return false;
        }
        
    }
}
