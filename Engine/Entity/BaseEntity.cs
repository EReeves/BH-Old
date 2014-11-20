using System;
using SFML.Graphics;
using SFML.Window;

namespace Client.Entity
{
    class BaseEntity : Sprite, IClientTemplate
    {
        public InternalHelper Helper;

        public BaseEntity()
        {
            Helper = new InternalHelper(this);
            Client.OnUpdate += Update;
            Client.OnDraw += Draw;
        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {
            base.Draw(Client.Window, RenderStates.Default);
        }


        public class InternalHelper
        {
            private BaseEntity Parent;
            public InternalHelper(BaseEntity parent)
            {
                Parent = parent;
            }

            public void Lerp(Vector2f endPosition, int interval)
            {
                Vector2f difference = endPosition - Parent.Position;
                Vector2f direction = difference / interval;
                int count = 0;

                Client.ClientHandler delDelegate = null;
                delDelegate = () =>
                {   
                    Parent.Position += direction;

                    count++;
                    if (count >= interval)
                        Client.OnUpdate -= delDelegate;
                };
                Client.OnUpdate += delDelegate;
            }

            public bool SeparatingAxisTest(Sprite sprite)
            {
                float xDist = System.Math.Abs(sprite.Position.X - Parent.Position.X - sprite.Texture.Size.X);
                float yDist = System.Math.Abs(sprite.Position.Y - Parent.Position.Y - sprite.Texture.Size.Y);
                float calculatedGapX = xDist - sprite.Texture.Size.X / 2;
                float calculatedGapY = yDist - sprite.Texture.Size.Y / 2;

                if (calculatedGapX < 0 && calculatedGapY < 0)
                    return true;    //Rectangles intersect.
                else
                    return false;
            }

            public void Bezier(Vector2f endPosition, Vector2f controlPoint, int interval)
            {
                Vector2f startPos = Parent.Position;
                double add = 1.0 / interval;
                float count = 0;

                Client.ClientHandler delDelegate = null;
                delDelegate = () =>
                {
                    Parent.Position = (1 - count) * startPos + (2 * (1 - count)) * count * controlPoint + count * endPosition;

                    count += (float)add;
                    if (count >= 1)
                        Client.OnUpdate -= delDelegate;
                };
                Client.OnUpdate += delDelegate;
            }

            public void ComplexBezier(Vector2f endPosition, Vector2f controlPointA, Vector2f controlPointB, int interval)
            {
                Vector2f startPos = Parent.Position;
                double add = 1.0 / interval;
                float count = 0;

                Client.ClientHandler delDelegate = null;
                delDelegate = () =>
                {
                    Parent.Position = (1 - count) * startPos + (2 * (1 - count)) * count * controlPointA + count * controlPointB + count * endPosition;

                    count += (float)add;
                    if (count >= 1)
                        Client.OnUpdate -= delDelegate;
                };
                Client.OnUpdate += delDelegate;
            }


        }
    }
}
