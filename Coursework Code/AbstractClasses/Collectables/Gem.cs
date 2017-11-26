using System;
using Mogre;
using PhysicsEng;

namespace Coursework
{
    class Gem : Collectable
    {
        PhysObj p2;
        protected Stat score;
        protected int increase;

        protected Gem(SceneManager mSceneMgr, Stat score)
        {
            this.mSceneMgr = mSceneMgr;
            this.score = score;
        }

        protected virtual void LoadModel()
        {

            float radius = 5;
            p2 = physObj = new PhysObj(radius, "Gem", 0.1f, 0.5f, 0.0001f);
            physObj.SceneNode = gameNode;
            physObj.Position = gameNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.AddForceToList(new FrictionForce(physObj));
            //physObj.AddForceToList(new ElasticForce(p2, physObj));
            Physics.AddPhysObj(physObj);

        }
        public void Anchor()
        {
            p2.Position = physObj.Position;
        }
        public override void Update(FrameEvent evt)
        {
            Animate(evt);

            
            if (this.IsCollidingWith("Player"))
            {
                    ((Score)score).Increase(increase);
                    Dispose();
             }
            base.Update(evt);
        }
        /// <summary>
        /// Dispose of object
        /// </summary>
        public override void Dispose()
        {
            this.remove = true;
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

         
        public override void Animate(FrameEvent evt)
        {
            gameNode.Yaw(evt.timeSinceLastFrame);
        }
    }
}
