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
        Texture2D b;
        Bullet b1;

        MouseState curr;
        MouseState prev;

        double posX;
        double posY;

        public Turret(Texture2D tex, Texture2D bullet)
        {
            t = tex;
            b = bullet;

            b1 = new Bullet(-99, -99, 0);
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
            if (curr.LeftButton == ButtonState.Pressed &&
               prev.LeftButton == ButtonState.Released)
            {
                b1 = new Bullet(posX, posY, angle);

                // make bullet start at tip of gun
                // rather than center of gun

                b1.x += Math.Cos(b1.angle * (3.14159 / 180)) * 50;
                b1.y += Math.Sin(b1.angle * (3.14159 / 180)) * 50;
            }

            shooting = (curr.LeftButton == ButtonState.Pressed);

            dirX = curr.Position.X - posX;
            dirY = curr.Position.Y - posY;
            angle = Math.Atan(dirY / dirX) * 180 / 3.14159;

            if (dirX < 0)
                angle += 180;

            if (angle < 0)
                angle += 360;
        }

        public void Update(double x, double y)
        {

            curr = Mouse.GetState();

            setPosition(x, y);
            calcAngle();

            if (curr.LeftButton == ButtonState.Pressed)
            {
                b1.x += Math.Cos(b1.angle * (3.14159 / 180)) * 10;
                b1.y += Math.Sin(b1.angle * (3.14159 / 180)) * 10;
            }

            prev = curr;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(t,
                new Rectangle((int)posX, (int)posY, t.Width/4, t.Height/4), 
                null, 
                Color.White, 
                (float)((angle + 90) * 3.14159 / 180), 
                new Vector2(t.Width / 2, t.Height / 2), 
                SpriteEffects.None, 0f);

            spriteBatch.Draw(b,
                new Rectangle((int)b1.x, (int)b1.y, b.Width / 20, b.Height / 20),
                null,
                Color.White,
                (float)((b1.angle + 90) * 3.14159 / 180),
                new Vector2(b.Width / 2, b.Height / 2),
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
