using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;

namespace Client
{
    /// <summary>
    /// Provides resolution relative Vector2's.
    /// Feed methods a float relative to 1/10th of the resolution, e.g. 5 would return 1/2 of the resolution.
    /// </summary>
    class RV2
    {
        private static Vector2u res = Client.Window.Size / 10;

        public static Vector2f F(float x, float y)
        {
            return new Vector2f(res.X * x, res.Y * y);
        }

        public static Vector2i I(float x, float y)
        {
            return new Vector2i((int)(res.X * x), (int)(res.Y * y));
        }

        public static Vector2u U(float x, float y)
        {
            return new Vector2u((uint)(res.X * x), (uint)(res.Y * y));
        }

    }
}
