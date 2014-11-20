using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;

namespace Client
{
    class Input
    {
        //Static input layer over SFML input, makes things easier.
        public static Vector2f MousePosition;
        private static List<Keyboard.Key> pressedKeys = new List<Keyboard.Key>();

        public Input()
        {
        }

        public static void Update()
        {
            //Remove keys if they are released.
            foreach (Keyboard.Key key in pressedKeys.ToList())
            {
                if(!Keyboard.IsKeyPressed(key))
                {
                    pressedKeys.Remove(key);
                }
            }

            Vector2i mouseCoords = Client.Window.MapCoordsToPixel(new Vector2f(Client.Window.InternalGetMousePosition().X, Client.Window.InternalGetMousePosition().Y));
            MousePosition = new Vector2f(mouseCoords.X, mouseCoords.Y);
        }

        public static bool OnKeyDown(Keyboard.Key key)
        {
            if (Keyboard.IsKeyPressed(key) && !pressedKeys.Contains(key)) 
            {
                pressedKeys.Add(key);
                return true;
            }
            return false;
        }
    }
}
