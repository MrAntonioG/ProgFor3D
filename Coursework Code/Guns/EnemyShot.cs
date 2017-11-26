using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;
namespace Coursework
{
    class EnemyShot : Projectile
    {
        public EnemyShot(SceneManager mSceneMgr)
            : base()
        {
            base.maxTime = 3000;
            this.time = new Timer();
            this.mSceneMgr = mSceneMgr;
            this.healthDamage = 10;
            this.shieldDamage = 10;
            this.speed = 50;
            this.target = "Player";
            Load();
        }
        protected override void Load()
        {
            this.gameNode = mSceneMgr.CreateSceneNode();
            this.gameEntity = mSceneMgr.CreateEntity("Sphere.mesh");
            this.gameEntity.SetMaterialName("EnemyShot");
            this.gameNode.AttachObject(gameEntity);
            this.gameNode.Scale(new Vector3(0.5f, 0.5f, 0.5f));
            mSceneMgr.RootSceneNode.AddChild(this.gameNode);

            this.physObj = new PhysObj(2, "EnemyShot", 0.1f, 0f);
            physObj.SceneNode = gameNode;
            //physObj.AddForceToList(new WeightForce(physObj.InvMass));

            Physics.AddPhysObj(physObj);
        }
        public override void Update(FrameEvent evt)
        {
            base.Update(evt);
            if (physObj != null)
            {
                physObj.Velocity = speed * initialDirection;
            }

        }
        /// <summary>
        /// Dispose of CannonBall
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            Physics.RemovePhysObj(physObj);
            physObj = null;
            if (gameNode != null)
            {
                if (gameNode.Parent != null)
                {
                    gameNode.Parent.RemoveChild(gameNode);
                }
                gameNode.RemoveAllChildren();
                gameNode.DetachAllObjects();
                gameNode.Dispose();
                gameEntity.Dispose();
            }
            this.remove = true;
        }
    }
}
