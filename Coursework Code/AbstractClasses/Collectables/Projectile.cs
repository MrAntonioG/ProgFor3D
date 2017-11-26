using System;
using System.Collections.Generic;
using Mogre;
using PhysicsEng;
namespace Coursework
{
    abstract class Projectile:MovableElement
    {
        protected Timer time;
        protected int maxTime = 1000;
        protected Vector3 initialVelocity;
        protected String target;
        public String Target
        {
            get { return target; }
        }
        protected float speed;
        
        public float Speed
        {
            get { return speed; }
        }
        protected Vector3 initialDirection;
        
        public Vector3 InitialDirection
        {
            set { initialDirection = value;
                  physObj.Velocity = speed * initialDirection;
                }

        }
        protected int healthDamage;
        public int HealthDamage
        {
            get { return healthDamage; }
        }

        protected int shieldDamage;
        public int ShieldDamage
        {
            get { return shieldDamage; }
        }

        /// <summary>
        /// To be Overriden. Load Method
        /// </summary>
        virtual protected void Load() { }
        /// <summary>
        /// Constructor
        /// </summary>
        protected Projectile()
        {
        }
        /// <summary>
        /// Dispose method
        /// </summary>
        public override void Dispose()
        {
            this.remove = true;
        }
        
        virtual public void Update(FrameEvent evt) 
        {
            
            this.remove = this.IsCollidingWith(target);
            
            if (remove || time.Milliseconds > maxTime)
            {
                Dispose();
                remove = true;
            }
        }
    }
}
