using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BulletHell
{
    class PlayerBulletPool
    {
        private static List<PlayerBullet> bullets = new List<PlayerBullet>();

        static PlayerBulletPool()
        {
            //Add 50 PlayerBullets to start with. They will get used.
            for (int i = 0; i < 50; i++)
            {
                PlayerBullet newbullet = new PlayerBullet();
                bullets.Add(newbullet);
            }
        }

        public static PlayerBullet GetPlayerBullet()
        {
            foreach (PlayerBullet bullet in bullets)
            {
                if (bullet.Free)
                {
                    bullet.UnPool();
                    return bullet;
                }
            }
            //No free PlayerBullets, spawn a new one.
            PlayerBullet newbullet = new PlayerBullet();
            newbullet.UnPool();
            bullets.Add(newbullet);
            return newbullet;
        }
    }
}
