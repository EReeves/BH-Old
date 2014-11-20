using SFML.Graphics;

namespace Client.Entity
{
    //TODO: Test.
    class AnimatedEntity : BaseEntity, IClientTemplate
    {
        public int FrameInterval { get; set; }
        public bool AnimateMain = true;
        public bool Loop = true;
        private Texture mainTexture;

        private int frameWidth;
        private int frameCount;
        private int currentFrame;
        private int currentTime;

        public AnimatedEntity(Texture _texture, int _frameWidth)
        {
            Texture = _texture;
            mainTexture = Texture;
            frameWidth = _frameWidth;
            FrameInterval = 30;
            ResetAnimation();
        }

        public override void Update()
        {
            base.Update();

            if (AnimateMain)
            {
                int newTime = Client.ElapsedGameTime;
                if (newTime > currentTime + FrameInterval)
                {
                    currentFrame++;
                    currentTime = newTime;

                    if (currentFrame > frameCount)
                    {
                        currentFrame = 1;
                        if (!Loop)
                            AnimateMain = false;
                    }

                    IntRect framePosition = new IntRect(frameWidth * (currentFrame-1), 0, frameWidth, (int)Texture.Size.Y);
                    TextureRect = framePosition;
                }
            }
        }

        public void ResetAnimation()
        {
            frameCount = (int)Texture.Size.X / frameWidth;
            currentFrame = 1;
            currentTime = Client.ElapsedGameTime;
            IntRect framePosition = new IntRect(0, 0, frameWidth, (int)Texture.Size.Y);
            TextureRect = framePosition;
            
        }

        /// <summary>
        /// Will return to main animation when completed, will not loop.
        /// </summary>
        public void PlayAnimation(Texture texture, int _frameWidth, int interval)
        {
            AnimateMain = false;
            Texture = texture;
            IntRect framePosition = new IntRect(0, 0, _frameWidth, (int)texture.Size.Y);
            TextureRect = framePosition;

            int time = Client.ElapsedGameTime;
            int count = (int)texture.Size.X / _frameWidth;
            int frame = 1;

            Client.ClientHandler delDelegate = null;
            delDelegate = () =>
            {
                int newTime = Client.ElapsedGameTime;
                if (newTime > time + interval)
                {
                    frame++;
                    time = newTime;

                    if (frame > count)
                    {
                        currentFrame = 1;
                        AnimateMain = true;
                        Texture = mainTexture;
                        Client.OnUpdate -= delDelegate;
                    }
                    framePosition = new IntRect(_frameWidth * (frame - 1), 0, _frameWidth, (int)texture.Size.Y);
                    TextureRect = framePosition;

                }
            };
            Client.OnUpdate += delDelegate;
        }
    }
}
