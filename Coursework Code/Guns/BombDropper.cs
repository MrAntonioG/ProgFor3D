using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace Coursework
{
    /// <summary>
    /// Class representing a gun that drops bombs
    /// </summary>
    class BombDropper : Gun
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr"></param>
        public BombDropper(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            this.maxAmmo = 3;
            this.ammo = new Stat();
            ammo.InitValue(maxAmmo);
            liveProjectiles = new List<Projectile>();
            this.gunID = "BombDropper";
            LoadModel();
        }
        /// <summary>
        /// Loading and assembling model
        /// </summary>
        protected override void LoadModel()
        {
            this.gameNode = mSceneMgr.CreateSceneNode();
            this.gameEntity = mSceneMgr.CreateEntity("BombDropperGun.mesh");
            this.gameNode.AttachObject(gameEntity);
        }
        /// <summary>
        /// Firing a bomb
        /// </summary>
        public override void Fire()
        {
            if (ammo.Value == 0)
            {

            }
            else
            {
                this.projectile = new Bomb(mSceneMgr);
                projectile.SetPosition(GunPosition() + 15 * -GunDirection());
                projectile.InitialDirection = (-GunDirection());
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
