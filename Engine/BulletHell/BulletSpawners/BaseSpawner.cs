using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace Client.BulletHell
{
    class BaseSpawner : Sprite, IDisposable, IClientTemplate
    {
        public int Frequency { get; set; }
        public int BulletAmount { get; set; }
        public float? BulletSpeed { get; set; }
        public Texture BulletTexture { get; set; }
        public int Health { get; set; }

        private int currentTime;

        public BaseSpawner(Texture texture, int frequency, int bulletAmount)
        {
            BulletTexture = texture;
            Frequency = frequency;
            BulletAmount = bulletAmount;
            Health = 50;
            Reset();
            Client.OnUpdate += Update;
            Client.OnDraw += Draw;
        }

        public void Reset()
        {
            currentTime = Client.ElapsedGameTime;
        }

        public virtual void Update()
        {
            TestIntersect();
            if (Health <= 0)
                Death();
        }

        public virtual void Draw()
        {
            Draw(Client.Window, RenderStates.Default);
        }

        public virtual void Death()
        {
            Client.OnUpdate -= Update;
            Client.OnDraw -= Draw;
            Dispose();
        }

        private void TestIntersect()
        {
            foreach (PlayerBullet pB in PlayerBullet.BulletPool.GetActiveList())
            {
                if (Collision.SeparatingAxisTest(this, pB))
                    Health -= pB.Damage; //Rectangles intersect. Deal damage.
            }
        }

    }
}
