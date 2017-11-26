using System;
using Mogre;
using System.Collections.Generic;

namespace Coursework
{
    /// <summary>
    /// This class implements a robot
    /// </summary>
    class Robot:Enemy
    {
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
        public Vector3 Direction()
        {
            SceneNode node = model.GameNode;
            try
            {
                while (node.ParentSceneNode.ParentSceneNode != null)
                {
                    node = node.ParentSceneNode;
                }
            }
            catch (System.AccessViolationException)
            { }

            Vector3 direction = node.LocalAxes * model.GameNode.LocalAxes.GetColumn(2);

            return direction;
        }
        protected Projectile projectile;
        public Projectile Projectile
        {
            set { projectile = value; }
            get { return projectile; }
        }
        protected List<Projectile> liveProjectiles;
        private SceneManager mSceneMgr;
        public List<Projectile> LiveProjectiles
        {
            get { return liveProjectiles; }
        }
         public Robot(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            model = new RobotModel(mSceneMgr);
            controller = new EnemyController(this);
            stats = new EnemyStats();
            liveProjectiles = new List<Projectile>();
            time = new Timer();
            invulnerable = 1000;
        }
        public override void Update(FrameEvent evt)
        {
            model.Animate(evt);
            controller.Update(evt);
            foreach (Projectile p in liveProjectiles)
            {
                if (!p.RemoveMe)
                {
                    p.Update(evt);
                }
            }
            Projectile c;
            if (model.IsCollidingWith("CannonBall"))
            {
                c = new CannonBall(mSceneMgr);
                if (time.Milliseconds > invulnerable)
                {
                    if (stats.Shield.Value != 0)
                    {
                        stats.Shield.Decrease(c.ShieldDamage);

                    }
                    else
                    {
                        stats.Health.Decrease(c.HealthDamage);
                    }
                    time.Reset();
                }
                c.Dispose();

            }
            if (model.IsCollidingWith("Bomb"))
            {
                c = new Bomb(mSceneMgr);
                if (time.Milliseconds > invulnerable)
                {
                    if (stats.Shield.Value != 0)
                    {
                        stats.Shield.Decrease(c.ShieldDamage);

                    }
                    else
                    {
                        stats.Health.Decrease(c.HealthDamage);
                    }
                    time.Reset();
                }
                c.Dispose();
            }

            if (stats.Health.Value == 0)
            {
                isDead = true;
                Dispose();
            }
        }

        public override void Dispose()
        {
            model.Dispose();
        }

        /// <summary>
        /// Shooting with active gun
        /// </summary>
        public void Shoot(Vector3 angle)
        {
            this.projectile = new EnemyShot(mSceneMgr);
            projectile.SetPosition(model.Position() + 20 * Direction());
            projectile.InitialDirection = (model.Position());
            liveProjectiles.Add(projectile);

        }
 
    }
}
