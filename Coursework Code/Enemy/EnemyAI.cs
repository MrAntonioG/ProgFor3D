using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace Coursework
{
    class EnemyAI:AI
    {
        protected Timer time;         // Timer value
        /// <summary>
        /// Write only. Timer
        /// </summary>
        public Timer Time
        {
            set { time = value; }
        }
        protected int maxTime;         // Timer value
        /// <summary>
        /// Write only. Timer
        /// </summary>
        public int MaxTime
        {
            set { maxTime = value; }
        }
        public EnemyAI(EnemyController Enemy)
        {
            time = new Timer();
            maxTime = 5000;
            controller = Enemy;
            speed = 100;//default speed value
        }

        public override void Update(FrameEvent evt)
        {
            ((EnemyController)controller).MovementsControl(evt);
            if (time.Milliseconds > maxTime)
            {
                //((EnemyController)controller).Fire();
                time.Reset();
            }
        }


    }
}
