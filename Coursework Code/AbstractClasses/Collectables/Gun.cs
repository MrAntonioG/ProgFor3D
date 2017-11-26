using System;
using Mogre;
using System.Collections.Generic;

namespace Coursework
{
    class Gun:MovableElement
    {
        protected int maxAmmo;
        protected String gunID;
        public String GunID
        {
            get { return gunID; }
        }
        protected Projectile projectile;
        public Projectile Projectile
        {
            set { projectile = value; }
            get { return projectile; }
        }
        protected List<Projectile> liveProjectiles;
        public List<Projectile> LiveProjectiles
        {
            get { return liveProjectiles; }
        }
        protected Stat ammo;
        public Stat Ammo
        {
            get { return ammo; }
        }

        public Vector3 GunPosition()
        {
            SceneNode node = gameNode;
            try
            {
                while (node.ParentSceneNode.ParentSceneNode != null)
                {
                    node = node.ParentSceneNode;
                }
            }
            catch (System.AccessViolationException)
            { }

            return node.Position;
        }

        public Vector3 GunDirection()
        {
            SceneNode node = gameNode;
            try
            {
                while (node.ParentSceneNode.ParentSceneNode != null)
                {
                    node = node.ParentSceneNode;
                }
            }
            catch (System.AccessViolationException)
            { }

            Vector3 direction = node.LocalAxes * gameNode.LocalAxes.GetColumn(2);

            return direction;
        }
        virtual public void Update(FrameEvent evt){ }
        virtual protected void LoadModel() { }
        virtual public void ReloadAmmo() { }
        virtual public void Fire() { }        
    }
}
