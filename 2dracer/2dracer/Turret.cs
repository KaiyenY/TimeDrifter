using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2dracer
{
    class Turret
    {
        Texture2D t;

        double posX;
        double posY;

        public Turret(Texture2D tex)
        {
            t = tex;
        }

        public void setPosition(double x, double y)
        {
            posX = x;
            posY = y;
        }
        
        double dirX = 0;
        double dirY = 0;
        double angle = 0;
        bool shooting;

        public void calcAngle()
        {
            // Mouse position is compared to the center of the screen
            // In the future, we will compare it to the position of the car

            MouseState s = Mouse.GetState();
            shooting = (s.LeftButton == ButtonState.Pressed);

            dirX = s.Position.X - posX;
            dirY = s.Position.Y - posY;
            angle = Math.Atan(dirY / dirX) * 180 / 3.14159;

            if (dirX < 0)
            {
                angle += 180;
            }

            if (angle < 0)
                angle += 360;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(t,
                new Rectangle((int)posX, (int)posY, t.Width, t.Height), 
                null, 
                Color.White, 
                (float)((angle + 90) * 3.14159 / 180), 
                new Vector2(t.Width / 2, t.Height / 2), 
                SpriteEffects.None, 0f);
        }

        public String Debug()
        {
            String text = "Turret Debug:" +
                "\nangle: " + angle + "\nShooting (left click): ";
            
            if (shooting)
                text += "Yes";
            else
                text += "No";

            return text;
        }
    }
}
