using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Entity;
using SFML.Graphics;
using SFML.Window;

namespace Client.BulletHell.BulletSpawners
{
    class BSpiral : BaseEntity, IClientTemplate
    {
        public int Frequency { get; set; }
        public int BulletAmount { get; set; }
        public float? BulletSpeed { get; set; }
        public Texture BulletTexture { get; set; }

        private int currentTime;
        private double degSeparation;
        private int bulletCount = 0;

        public BSpiral(Texture texture, int frequency, int bulletAmount)
        {
            BulletTexture = texture;
            Frequency = frequency;
            BulletAmount = bulletAmount;
            Reset();
        }

        public void Reset()
        {
            currentTime = Client.ElapsedGameTime;
        }

        public override void Update()
        {
            base.Update();

            degSeparation = 360.0 / BulletAmount;

            int newTime = Client.ElapsedGameTime;
            if (newTime > currentTime + Frequency)
            {
                if (bulletCount > BulletAmount)
                    bulletCount = 1;

                bulletCount++;
                currentTime = newTime;
                Bullet bullet = Bullet.BulletPool.GetObject();
                bullet.Texture = BulletTexture;
                bullet.Speed = BulletSpeed ?? 1f;
                bullet.Position = Position;
                bullet.Texture.Smooth = true;
                float x = (float)System.Math.Sin(DegToRad(degSeparation * bulletCount));
                float y = (float)System.Math.Cos(DegToRad(degSeparation * bulletCount));
                bullet.Direction = new Vector2f(x, y);
            }
        }

        private double DegToRad(double angle)
        {
            return System.Math.PI * angle / 180.0;
        }

    }
}
