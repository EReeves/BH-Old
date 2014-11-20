using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Entity;
using SFML.Graphics;
using SFML.Window;

namespace Client
{
    class PlayerBullet : Sprite, IPoolable, IClientTemplate
    {
        public Vector2f Direction { get; set; }
        public float Speed { get; set; }
        public int Damage { get; set; }
        public bool Free { get; set; }

        //Unlike enemy bullets, they will need to be accessed by other classes for collision checks.
        public static ObjectPool<PlayerBullet> BulletPool = new ObjectPool<PlayerBullet>(200);

        public PlayerBullet()
        {
            Direction = new Vector2f(0, 1);
            Speed = 0.1f;
            Damage = 2;
        }

        public void Update()
        {
            Position += (Direction * Speed);
            //Dispose if off screen.
            if ((Position.Y > Client.Window.Size.Y) ||
                (Position.Y < -Texture.Size.Y)      ||
                (Position.X > Client.Window.Size.X) ||
                (Position.X < -Texture.Size.X))
            {
                Pool();
            }
        }

        public void Draw()
        {
            base.Draw(Client.Window, RenderStates.Default);
        }

        //Call when removing bullet.
        public virtual void Pool()
        {
            Client.OnUpdate -= Update;
            Client.OnDraw -= Draw;
            //Remember to remove anything set.
        }

        //Call when reusing bullet.
        public virtual void UnPool()
        {
            Client.OnUpdate += Update;
            Client.OnDraw += Draw;
        }

    }
}
