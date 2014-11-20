using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client;

namespace Client.Queue
{
    class ActionQueue
    {
        private int currentTime;
        private Queue<QueueAction> actionList = new Queue<QueueAction>();

        public ActionQueue()
        {
            currentTime = Client.ElapsedGameTime;
            Client.OnUpdate += Update;
        }

        public void Update()
        {
            int newTime = Client.ElapsedGameTime;
            if (actionList.Count >= 1)
            {
                if (newTime > currentTime + actionList.Peek().Interval)
                {
                    currentTime = newTime;
                    actionList.Dequeue().Invoke();
                }
            }
        }

        public void Add(int interval, Action action)
        {
            //Convert interval from frames to realtime.
            actionList.Enqueue(new QueueAction(interval,action));
        }

        public static void AddRelative(int interval, Action action)
        {
            //Self contained, will unhook itself from OnUpdate via delegate wizardry.
            int elapseTime = interval + Client.ElapsedGameTime;
            Client.ClientHandler delAction = null;
            delAction = delegate()
            {
                int newTime = Client.ElapsedGameTime;
                if (newTime > elapseTime)
                {
                    Task.Factory.StartNew(action);
                    Client.OnUpdate -= delAction;
                }
            };
            Client.OnUpdate += delAction;
        }

        /// <param name="repeatInterval">A lazy modulus check, so 2 = every second frame, 10 = every tenth frame etc.</param>       
        /// <param name="repeatAmount">0 = infinite, use sparingly.</param>
        public static void AddRelativeRepeat(int startInterval, int repeatInterval, int repeatAmount, Action action)
        {
            bool active = false;
            int count = 0;
            int elapseTime = startInterval + Client.ElapsedGameTime;
            Client.ClientHandler delAction = null;
            delAction = delegate()
            {
                int newTime = Client.ElapsedGameTime;
                if (newTime > elapseTime)
                    active = true;

                if(active && Client.ElapsedGameTime % repeatInterval == 0)
                {
                    Task.Factory.StartNew(action);
                    count++;
                    if(count >= repeatAmount && repeatAmount != 0)
                        Client.OnUpdate -= delAction;
                }
            };
            Client.OnUpdate += delAction;
        }

    }

    class QueueAction
    {
        public int Interval { get; set; }
        private Action Action;

        public QueueAction(int interval, Action action)
        {
            Action = action;
            Interval = interval;
        }

        public void Invoke()
        {
            Task.Factory.StartNew(Action);
        }
    }

}
