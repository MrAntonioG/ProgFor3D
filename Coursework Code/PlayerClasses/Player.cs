using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
namespace Coursework
{
    class Player:Character
    {
        SceneManager mSceneMgr;
        protected Armoury armoury;
        protected Vector3 direction;
        public Vector3 Direction
        {
            get { return direction; }
        }
        protected Timer time;
        public Timer Time
        {
            get { return time; }
        }
        protected int invulnerable; //invulnerability time
        public int Invulnerable
        {
            get { return invulnerable; }
        }
        
        /// <summary>
        /// Read Only. Player Armoury
        /// </summary>
        public Armoury Armoury
        {
            get { return armoury; }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr"></param>
        public Player(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            model = new PlayerModel(mSceneMgr);
            controller = new PlayerController(this);
            stats = new PlayerStats();
            armoury = new Armoury();
            time = new Timer();
            invulnerable = 1000;
            this.isDead = false;
        }
        /// <summary>
        /// Updates everything in the player class
        /// </summary>
        /// <param name="evt"></param>
        public override void Update(FrameEvent evt)
        {
            if (!isDead)
            {
                if (armoury.ActiveGun != null)
                {
                    ((PlayerStats)stats).Ammo = armoury.ActiveGun.Ammo.Value;
                    ((PlayerStats)stats).MaxAmmo = armoury.ActiveGun.Ammo.Max;
                }
                model.Animate(evt); //Animate model
                controller.Update(evt);//update player controller
                if (armoury.ActiveGun != null)
                {
                    armoury.ActiveGun.Update(evt);
                }
                if (armoury.GunChanged == true) //if the gun has been changed
                {
                    ((PlayerModel)model).AttachGun(armoury.ActiveGun); //attach new gun to the model
                    armoury.GunChanged = false; //update boolean to false as gun is no longer changed
                }
                if (model.IsCollidingWith("Robot"))
                {
                    if (time.Milliseconds > invulnerable)
                    {
                        if (stats.Shield.Value != 0)
                        {
                            stats.Shield.Decrease(20);

                        }
                        else
                        {
                            stats.Health.Decrease(10);
                        }
                        time.Reset();
                    }
                    if (stats.Health.Value == 0)
                    {
                        stats.Lives.Decrease(1);
                        ((PlayerStats)stats).NewLife();
                    }
                }
                if (stats.Lives.Value == 0)
                {
                    this.isDead = true;
                }
            }
            else
            {

            }
            
        }

        
        /// <summary>
        /// Shooting with active gun
        /// </summary>
        public override void Shoot()
        {
            if (armoury.ActiveGun != null)
            {
                armoury.ActiveGun.Fire();
            }
            
        }
        /// <summary>
        /// Dispose of player
        /// </summary>
        public void Dispose()
        {
            model.Dispose();
        }
        /// <summary>
        /// Reload gun
        /// </summary>
        public void Reload()
        {
            if (armoury.ActiveGun != null)
            {
                armoury.ActiveGun.ReloadAmmo();
            }
        }
        /// <summary>
        /// Swapping gun
        /// </summary>
        public void SwapGun()
        {
            if (armoury.CollectedGuns.Count() > 1)
            {
                foreach (Projectile p in armoury.ActiveGun.LiveProjectiles)
                {
                    p.Dispose();
                }
                if (armoury.ActiveGun.GunID.Equals("Cannon"))
                {
                    armoury.ActiveGun.Dispose();
                    armoury.ChangeGun(new BombDropper(mSceneMgr));
                }
                else if (armoury.ActiveGun.GunID.Equals("BombDropper"))
                {
                    armoury.ActiveGun.Dispose();

                    armoury.ChangeGun(new Cannon(mSceneMgr));
                }
            }
            
            
        }
    }
}
