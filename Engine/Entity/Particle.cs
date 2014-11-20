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
    class Particle : BaseEntity, IDisposable, IClientTemplate
    {
        public Vector2f Direction { get; set; }
        public float Speed { get; set; }
        public float DistanceLimit { get; set; }

        private Vector2f startPosition;
      
        public Particle(int x, int y)
        {
            Position = new Vector2f(x, y);
            startPosition = Position;
            Direction = new Vector2f(0, 1);
            Speed = 0.1f;
            DistanceLimit = 50;
        }

        public override void Update()
        {
            base.Update();
            Position += (Direction * Speed);

            Vector2f vectDiff = startPosition - Position;
            if (vectDiff.X < DistanceLimit || vectDiff.X > DistanceLimit && vectDiff.Y < DistanceLimit || vectDiff.Y > DistanceLimit)
            {
                Client.OnUpdate -= Update;
                Client.OnDraw -= Draw;
                Dispose();
            }
        }

    }
}
