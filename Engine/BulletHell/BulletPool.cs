using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BulletHell
{
    class BulletPool
    {
        private static List<Bullet> bullets = new List<Bullet>();

        static BulletPool()
        {
            //Add 200 bullets to start with. They will get used.
            for (int i = 0; i < 200; i++)
            {
                Bullet newBullet = new Bullet();
                bullets.Add(newBullet);
            }
        }

        public static Bullet GetBullet()
        {
            foreach (Bullet bullet in bullets)
            {
                if (bullet.Free)
                {
                    bullet.UnPool();
                    return bullet;
                }
            }
            //No free bullets, spawn a new one.
            Bullet newBullet = new Bullet();
            newBullet.UnPool();
            bullets.Add(newBullet);
            return newBullet;
        }
    }
}
