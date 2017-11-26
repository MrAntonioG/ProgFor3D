using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace Coursework
{
    /// <summary>
    /// Class Representing a cannon
    /// </summary>
    class Cannon:Gun
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr"></param>
        public Cannon(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            this.maxAmmo = 10;
            this.ammo = new Stat();
            ammo.InitValue(maxAmmo);
            liveProjectiles = new List<Projectile>();
            this.gunID = "Cannon";
            LoadModel();
        }
        /// <summary>
        /// Loading and assembling model
        /// </summary>
        protected override void LoadModel()
        {
            this.gameNode = mSceneMgr.CreateSceneNode();
            this.gameEntity = mSceneMgr.CreateEntity("CannonGun.mesh");
            this.gameNode.AttachObject(gameEntity);

        }
        /// <summary>
        /// Fire a projectile from gun
        /// </summary>
        public override void Fire()
        {
            if (ammo.Value != 0)
            {
                
                this.projectile = new CannonBall(mSceneMgr);
                projectile.SetPosition(GunPosition()+ 15 * GunDirection());
                projectile.InitialDirection = (GunDirection() );
                liveProjectiles.Add(projectile);
                ammo.Decrease(1);
            }
        }
        public override void Update(FrameEvent evt)
        {
            foreach (Projectile p in liveProjectiles)
            {
                if (!p.RemoveMe)
                {
                    p.Update(evt);
                }
            }
        }
        /// <summary>
        /// Reloading ammo to max
        /// </summary>
        public override void ReloadAmmo()
        {
            foreach (Projectile p in liveProjectiles)
            {
                if (p != null)
                {
                    p.Dispose();
                }
            }
            liveProjectiles.Clear();
            if (ammo.Value < maxAmmo)
            {
                ammo.InitValue(maxAmmo);
            }
        }
    }
}
