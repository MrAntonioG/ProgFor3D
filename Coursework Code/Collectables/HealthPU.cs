using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace Coursework
{
    class HealthPU:PowerUp
    {
        public HealthPU(SceneManager mSceneMgr, Stat stat):base(mSceneMgr)
        {
            this.Stat = stat;
            this.increase = 10;
        }
        protected override void LoadModel()
        {
            base.LoadModel();
            this.gameNode = mSceneMgr.CreateSceneNode();
            this.gameEntity = mSceneMgr.CreateEntity("Sphere.mesh");
            this.gameEntity.SetMaterialName("RedSphere");
            //this.gameEntity.GetMesh().BuildEdgeList();
            this.gameNode.AttachObject(gameEntity);
            mSceneMgr.RootSceneNode.AddChild(gameNode);
            float radius = 10;
            physObj = new PhysObj(radius, "HealthPU", 0.1f, 0.7f, 0.3f);
            physObj.SceneNode = gameNode;
            physObj.Position = gameNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.AddForceToList(new FrictionForce(physObj));
            Physics.AddPhysObj(physObj);
        }
    }
}
