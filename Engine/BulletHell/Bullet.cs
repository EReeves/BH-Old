using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Entity;
using SFML.Graphics;
using SFML.Window;
using Client.BulletHell;

namespace Client
{
    class Bullet : BaseEntity, IPoolable, IClientTemplate
    {
        public Vector2f Direction { get; set; }
        public float Speed { get; set; }
        public bool Free { get; set; }

        public delegate void PlayerCollideDelegate();
        public event PlayerCollideDelegate OnPlayerCollide;

        public static ObjectPool<Bullet> BulletPool = new ObjectPool<Bullet>(200);
       
        public Bullet()
        {
            Free = true;
            Direction = new Vector2f(0, 1);
            Speed = 0.1f;
            OnPlayerCollide += colorTest;
        }

        private void colorTest()
        {
            Color = Color.Red;
        }

        public void Update()
        {
            Position += (Direction * Speed);
            //Pool if outside borders.
            if ((Position.Y > Client.Window.Size.Y)  ||
                (Position.Y < -Texture.Size.Y)       ||
                (Position.X > Client.Window.Size.X)  ||
                (Position.X < -Texture.Size.X))
                {
                    Pool();
                }

            if (Collision.SeparatingAxisTest(this, Client.Player))
            {
                //Player hit.
                if(OnPlayerCollide != null)
                    OnPlayerCollide.Invoke();
            }
        }

        public void Draw()
        {
            base.Draw(Client.Window, RenderStates.Default);
        }

        //Call when removing bullet.
        public void Pool()
        {
            Client.OnUpdate -= Update;
            Client.OnDraw -= Draw;
            //temp
            Color = Color.White;
        }

        //Call when using bullet.
        public virtual void UnPool()
        {
            Client.OnUpdate += Update;
            Client.OnDraw += Draw;
        }

    }
}
