using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Entity;
using SFML.Window;
using SFML.Graphics;
using Client.BulletHell;

namespace Client
{
    class Player : BaseEntity, IClientTemplate
    {
        private Texture bulletTexture;
        private Random random = new Random(Client.ElapsedGameTime);

        public Player()
        {
            while (Texture==null)
            {
                Texture = new Texture("Content/Sprites/Ship.png");
                bulletTexture = new Texture("Content/Sprites/PlayerBullet.png");
            }
            Position = new Vector2f(Client.Window.Size.X/2 - Texture.Size.X/2, Client.Window.Size.Y - Texture.Size.Y*2);

            Client.OnUpdate += Update;
            Client.OnDraw += Draw;
        }

        public override void Update()
        {
            base.Update();
            if (Texture != null)
            {
                Vector2i mouseCoords = Client.Window.MapCoordsToPixel(new Vector2f(Client.Window.InternalGetMousePosition().X,Client.Window.InternalGetMousePosition().Y));

                if (mouseCoords.X > 5 && mouseCoords.Y > 5 && mouseCoords.X < Client.Window.Size.X-5 && mouseCoords.Y < Client.Window.Size.Y-5)
                {
                    Position = (new Vector2f(mouseCoords.X - Texture.Size.X / 2, mouseCoords.Y - Texture.Size.Y / 2));
                }
            }
            InputTest();
        }

        private void InputTest()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                PlayerBullet bullet = PlayerBullet.BulletPool.GetObject();
                bullet.Texture = bulletTexture;
                bullet.Speed = 17f;
                bullet.Position = Position - new Vector2f(-bulletTexture.Size.X/4, bulletTexture.Size.Y/2);
                bullet.Texture.Smooth = true;
                bullet.Direction = new Vector2f((float)NextDouble(random, -0.1, 0.1), -1);
            }
        }

        static double NextDouble(Random rng, double min, double max)
        {
            return min + (rng.NextDouble() * (max - min));
        }

    }
}
