using SFML.Graphics;
using SFML.Window;

namespace Client.BulletHell
{
    class Powerup : Sprite, IClientTemplate
    {
        public Vector2f Direction { get; set; }
        public float Speed { get; set; }

        public delegate void PlayerCollideDelegate();
        public event PlayerCollideDelegate OnPlayerCollide;

        public Powerup()
        {
            Client.OnUpdate += Update;
            Client.OnDraw += Draw;
        }

        public virtual void Update()
        {
            Position += (Direction * Speed);
            if (Collision.SeparatingAxisTest(this, Client.Player))
            {
                OnPlayerCollide.Invoke();
                //Give player powerup.
            }
        }

        public virtual void Draw()
        {
            Draw(Client.Window, RenderStates.Default);
        }

    }
}
