using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng; 

namespace Coursework
{
    class LifePU : PowerUp
    {
        public LifePU(SceneManager mSceneMgr, Stat stat):base(mSceneMgr)
        {
            this.Stat = stat;
            this.increase = 1;
        }
        protected override void LoadModel()
        {
            base.LoadModel();
            this.gameNode = mSceneMgr.CreateSceneNode();
            this.gameEntity = mSceneMgr.CreateEntity("Heart.mesh");
            this.gameEntity.SetMaterialName("HeartHMD");
            this.gameEntity.GetMesh().BuildEdgeList();
            this.gameNode.AttachObject(gameEntity);
            this.gameNode.Scale(new Vector3(15f,15f, 15f));
            mSceneMgr.RootSceneNode.AddChild(gameNode);
            float radius = 10;
            physObj = new PhysObj(radius, "LifePU", 0.2f, 0.7f, 0.3f);
            physObj.SceneNode = gameNode;
            physObj.Position = gameNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.AddForceToList(new FrictionForce(physObj));
            //physObj.AddForceToList(new ElasticForce(new PhysObj(), physObj));
            Physics.AddPhysObj(physObj);
        }
    }


}
