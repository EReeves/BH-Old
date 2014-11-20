using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Window;
using SFML.Graphics;
using Client.GUI;
using Client.Entity;
using Client.Queue;
using Client.BulletHell;
using Client.BulletHell.BulletSpawners;
using System.Threading.Tasks;

namespace Client
{

    class Client
    {
        //Globals because it's event driven. Way faster than passing, still maintainable.
        public static RenderWindow Window;        
        public delegate void ClientHandler();
        public static event ClientHandler OnUpdate, OnLateUpdate, OnDraw, OnLateDraw;
        public static int ElapsedGameTime = 0;
        public static Player Player;
        ///////////////////////////////////////////////////////////////////////////

        //Remove these once a texture handling class is created.
        private static Texture baseTexture = new Texture(@"Content\Sprites\Base.png");
        private static Texture baseBulletTexture = new Texture(@"Content\Sprites\Bullet.png");

        static void Main(string[] args)
        {           
            ContextSettings contextSettings = new ContextSettings(){
                DepthBits = 32,
                AntialiasingLevel = 8};
            Window = new RenderWindow(new VideoMode(900, 900), "Client", Styles.Default, contextSettings);
            Window.SetFramerateLimit(61);
            Window.SetActive();
            Window.Closed += new EventHandler(Window_Closed);
            Window.SetMouseCursorVisible(false);

            //Player = new Player();
            Load();
            

            while (Window.IsOpen())
            {
                Window.DispatchEvents();
                Window.Clear(new Color(50, 50, 50));
                    MainLoop(Window); 
                Window.Display();
            }
        }

        private static void Load()
        {
            //Testing
            DebugView debugView = new DebugView();

            BaseEntity bEa = new BaseEntity();
            bEa.Texture = baseBulletTexture;
            bEa.Position = new Vector2f(1, 1);

            AnimatedEntity bE = new AnimatedEntity(new Texture("Content/Sprites/Anim.png"), 32);
            bE.Position = new Vector2f(100,100);

            Random rand = new Random(DateTime.Now.Millisecond);
            ActionQueue aQ = new ActionQueue();
           
            for (int i = 0; i < 500; i++)
            {
                aQ.Add(30, () =>
                {
                    bE.Helper.ComplexBezier(new Vector2f(500, 500), new Vector2f(rand.Next(-400, 400), rand.Next(-400, 400)), new Vector2f(rand.Next(-400, 400), rand.Next(-400, 400)), 30);
                });
                aQ.Add(30, () =>
                {
                    bE.Helper.Bezier(new Vector2f(300, 300), new Vector2f(rand.Next(-400, 400), rand.Next(-400, 400)), 30);
                });
            }
            ActionQueue.AddRelative(30, () =>
            {
                bE.PlayAnimation(new Texture("Content/Sprites/Ship.png"), 5, 5);
            });

            var u = RV2.F(0, 0);
            var o = RV2.F(-1, -1);
            
        }

        private static void MainLoop(RenderWindow Window)
        {
            ElapsedGameTime++;

            //Update.
            if(OnUpdate != null)
                OnUpdate.Invoke();
            if(OnLateUpdate != null)
                OnLateUpdate.Invoke();
            //Input needs to update last.
            Input.Update();

            //Draw
            if(OnDraw != null)
                OnDraw.Invoke();
            if(OnLateDraw != null)
                OnLateDraw.Invoke();
        }

        private static void Window_Closed(object sender, EventArgs e)
        {
            //Aww.
            RenderWindow Window = (RenderWindow)sender;
            Window.Close();
        }

    }
}
