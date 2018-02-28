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
        List<Bullet> bullets;

        MouseState curr;
        MouseState prev;

        double posX;
        double posY;

        public Turret(Texture2D tex, Texture2D bullet)
        {
            t = tex;
            b = bullet;

            bullets = new List<Bullet>();
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
            shooting = (curr.LeftButton == ButtonState.Pressed);

            dirX = curr.Position.X - posX;
            dirY = curr.Position.Y - posY;
            angle = Math.Atan(dirY / dirX) * 180 / 3.14159;

            if (dirX < 0)
                angle += 180;

            if (angle < 0)
                angle += 360;
        }

        double timer = 0;
        public void Update(GameTime gameTime, double x, double y)
        {
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            curr = Mouse.GetState();

            if (curr.LeftButton == ButtonState.Pressed &&
                timer >= 150)
            {
                timer = 0;
                Bullet b1;
                b1 = new Bullet(posX, posY, angle);

                // make bullet start at tip of gun
                // rather than center of gun

                b1.x += Math.Cos(b1.angle * (3.14159 / 180)) * 70;
                b1.y += Math.Sin(b1.angle * (3.14159 / 180)) * 70;

                bullets.Add(b1);
            }

            setPosition(x, y);
            calcAngle();

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].x += Math.Cos(bullets[i].angle * (3.14159 / 180)) * 10;
                bullets[i].y += Math.Sin(bullets[i].angle * (3.14159 / 180)) * 10;

                if (Math.Abs(bullets[i].x) > 1000 ||
                    Math.Abs(bullets[i].y) > 1000)
                    bullets.RemoveAt(i);

            }
            prev = curr;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(t,
                new Rectangle((int)posX, (int)posY, t.Width / 4, t.Height / 4),
                null,
                Color.White,
                (float)((angle + 90) * 3.14159 / 180),
                new Vector2(t.Width / 2, t.Height / 2),
                SpriteEffects.None, 0f);

            for (int i = 0; i < bullets.Count; i++)
            {
                spriteBatch.Draw(b,
                    new Rectangle((int)bullets[i].x, (int)bullets[i].y, b.Width / 20, b.Height / 20),
                    null,
                    Color.White,
                    (float)((bullets[i].angle + 90) * 3.14159 / 180),
                    new Vector2(b.Width / 2, b.Height / 2),
                    SpriteEffects.None, 0f);
            }
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
