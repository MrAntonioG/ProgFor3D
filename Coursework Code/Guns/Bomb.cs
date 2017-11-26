using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace Coursework
{
    class Bomb:Projectile
    {
        PhysObj p2;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr"></param>
           public Bomb(SceneManager mSceneMgr):base()
        {
            
            this.maxTime = 5000;
            this.time = new Timer();
            this.mSceneMgr = mSceneMgr;
            this.healthDamage = 50;
            this.shieldDamage = 50;
            this.speed = 15;
            this.target = "Robot";
            Load();
        }
        /// <summary>
        /// Load model and physiscs
        /// </summary>
        protected override void Load()
        {
            this.gameNode = mSceneMgr.CreateSceneNode();
            this.gameEntity = mSceneMgr.CreateEntity("Bomb.mesh");
            this.gameNode.AttachObject(gameEntity);
            this.gameNode.Scale(new Vector3(3, 3, 3));
            mSceneMgr.RootSceneNode.AddChild(this.gameNode);
            
            this.physObj = new PhysObj(5, "Bomb", 0.2f, 0.5f);
            this.physObj.SceneNode = gameNode;
            this.physObj.AddForceToList(new WeightForce(physObj.InvMass));
            this.physObj.AddForceToList(new FrictionForce(physObj));
            //this.physObj.AddForceToList(new ElasticForce(physObj, p2,1f,0.05f,0.1f));

            Physics.AddPhysObj(physObj);
        }
        public override void Update(FrameEvent evt)
        {
            base.Update(evt);
            if (p2 != null)
            {
                //physObj.Velocity = speed * initialDirection;
                
            }

        }
        /// <summary>
        /// Dispose of Bomb
        /// </summary>
        public override void Dispose()
        {
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
        }


    }
}
