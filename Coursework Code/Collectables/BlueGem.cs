using System;
using Mogre;
using PhysicsEng;

namespace Coursework
{
    class BlueGem:Gem
    {
        public BlueGem(SceneManager mSceneMgr, Stat score) : base(mSceneMgr, score)
        {
            this.increase = 10;
            LoadModel();
        }

        protected override void LoadModel()
        {
           
            this.gameNode = mSceneMgr.CreateSceneNode();
            this.gameEntity = mSceneMgr.CreateEntity("Gem.mesh");
            this.gameEntity.GetMesh().BuildEdgeList();
            this.gameNode.AttachObject(gameEntity);
            this.gameNode.Scale(new Vector3(3, 3, 3));
            mSceneMgr.RootSceneNode.AddChild(gameNode);
            base.LoadModel();
            
        }   
    }
}
