using System;
using Mogre;
using PhysicsEng;
//using PhysicsEng;

namespace Coursework
{
    abstract class PowerUp:Collectable
    {
        protected Stat stat;
        public Stat Stat
        {
            set { stat = value; }
        }

        protected PowerUp(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            LoadModel();
        }

        protected int increase;
        virtual protected void LoadModel() { }

        public override void Update(FrameEvent evt)
        {
            this.remove = this.IsCollidingWith("Player");
            if (remove)
            {
                if (stat.Value < stat.Max)
                {
                    stat.Increase(increase);
                    Dispose();
                }
                else
                {
                    remove = false;
                }
                
            }
            gameNode.Yaw(evt.timeSinceLastFrame);
        }
        public override void Dispose()
        {
            base.Dispose();
            if (physObj != null)
            {
                Physics.RemovePhysObj(physObj);
                physObj = null;
            }
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
