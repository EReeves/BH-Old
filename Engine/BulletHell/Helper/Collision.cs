using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace Client.BulletHell
{
    class Collision
    {
        public static bool SeparatingAxisTest(Sprite a, Sprite b)
        {
            float xDist = System.Math.Abs(a.Position.X - b.Position.X - a.Texture.Size.X);
            float yDist = System.Math.Abs(a.Position.Y - b.Position.Y - a.Texture.Size.Y);
            float calculatedGapX = xDist - a.Texture.Size.X / 2;
            float calculatedGapY = yDist - a.Texture.Size.Y / 2;

            if (calculatedGapX < 0 && calculatedGapY < 0)
                return true;    //Rectangles intersect.
            else
                return false;
        }
    }
}
