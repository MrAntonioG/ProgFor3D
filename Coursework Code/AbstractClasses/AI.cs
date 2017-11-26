using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace Coursework
{
    class AI
    {
        //Field holding Player
        protected EnemyController controller;
        /// <summary>
        /// Read/Write character field
        /// </summary>
        public EnemyController Controller
        {
            get { return controller; }
            set { controller = value; }

        }

        //Field holding speed at which character moves
        protected float speed;
        /// <summary>
        /// Read/Write speed field
        /// </summary>
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        /// <summary>
        /// This method is to update the character state
        /// </summary>
        /// <param name="evt">A frame event which can be used to tune the character update</param>
        virtual public void Update(FrameEvent evt) { }
        

     }
}
