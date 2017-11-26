using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
namespace Coursework
{
    class PlayerController:CharacterController
    {
        //protected Character character;
        protected bool reload;
        public bool Reload
        {
            set { reload = value; }
        }
        protected bool swapGun;         // This method determines when the charatcer is to swap gun
        
        /// <summary>
        /// Write only. This property allows to set whether the character is to swap gun
        /// </summary>
        public bool SwapGun
        {
            set { swapGun = value; }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pl">Player to control</param>
        public PlayerController(Character pl)
        {
            this.character = pl;
            this.speed = 150;
        }

        /// <summary>
        /// Update the controller
        /// </summary>
        /// <param name="evt">Frame event</param>
        public override void Update(FrameEvent evt)
        {
            base.Update(evt);
            MovementsControl(evt);
            MouseControls();
            ShootingControls();
        }

        /// <summary>
        /// Moving the player according to their input
        /// </summary>
        /// <param name="evt">Movement Event</param>
        private void MovementsControl(FrameEvent evt){
            Vector3 move =  Vector3.ZERO;
            if (this.left == true ){
               
                    move += character.Model.Left;
            }
            if (this.right == true)
            {
                move += -character.Model.Left;
            }
            if (this.forward == true)
            {
                move += character.Model.Forward;
            }
            if (this.backward == true)
            {
                move += -character.Model.Forward;
            }
            if (accellerate)
            {
                move = move.NormalisedCopy * (speed * 3);
            }
            else
            {
                move = move.NormalisedCopy * speed;
            }

            if (move != Vector3.ZERO)
            {
                character.Move(move * evt.timeSinceLastFrame);
            }

        }

        /// <summary>
        /// Rotates the player according to mouse controls
        /// </summary>
        private void MouseControls()
        {
            character.Model.GameNode.Yaw(Mogre.Math.AngleUnitsToRadians(angles.y));
        }

        /// <summary>
        /// Shoots according to player input
        /// </summary>
        private void ShootingControls()
        {
            if (shoot)
            {
                ((Player)character).Shoot();
                shoot = false;
            }
            if (swapGun)
            {
                ((Player)character).SwapGun();
                swapGun = false;
            }
            if (reload)
            {
                ((Player)character).Reload();
                reload = false;
            }
        }
    }
}
