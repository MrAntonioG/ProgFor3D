using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace Coursework
{
    class EnemyController:CharacterController
    {
        protected AI ai; //AI to controll enemy
        public AI Ai
        {
            get { return ai;}
        }
        protected Player player;
        public Player Player
        {
            set { player = value; }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="er"></param>
        public EnemyController(Enemy er)
        {
            this.character = er;
            this.ai = new EnemyAI(this);
        }

        /// <summary>
        /// Update the controller
        /// </summary>
        /// <param name="evt">Frame event</param>
        public override void Update(FrameEvent evt)
        {
            base.Update(evt);
            if (!character.IsDead)
            {
                ai.Update(evt);
            }
            

        }
        /// <summary>
        /// Fire a projectile
        /// </summary>
        public void Fire() {
            Vector3 angle = character.Position;
            Vector3 pPos = player.Position;
            angle.x = pPos.x;
            angle.z = pPos.z;
            if (angle != Vector3.ZERO)
            {
                ((Robot)character).Shoot(angle);
            }

        }
         /// <summary>
        /// Moving the player according to their input
        /// </summary>
        /// <param name="evt">Movement Event</param>
        public void MovementsControl(FrameEvent evt)
        {
            Vector3 move = Vector3.ZERO; 
            move = player.Position - character.Position;
            if (move != Vector3.ZERO)
            {
                character.Move(move * evt.timeSinceLastFrame);
            }
            Vector3 angle = character.Position;
            Vector3 pPos = player.Position;
            angle.x = pPos.x;
            angle.z = pPos.z;
            if (angle != Vector3.ZERO)
            {
                ((RobotModel)((Robot)character).Model).Rotate(angle);
            }
        }
    }
}
