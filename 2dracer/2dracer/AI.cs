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
    class AI
    {
        private Enemy[] enemies;

        public AI(Texture2D tex)
        {
            enemies = new Enemy[3];

            for (int i = 0; i < 3; i++)
            {
                enemies[i] = new Enemy(tex, new Vector2(50, 100 * i));
            }
        }

        public void Update()
        {
            foreach (Enemy i in enemies)
                i.Update();
        }

        public void Draw()
        {
            foreach (Enemy i in enemies)
                i.Draw();
        }
    }
}
