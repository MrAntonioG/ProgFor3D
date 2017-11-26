using System;
using Mogre;
using PhysicsEng;

namespace Coursework
{
    
    class RedGem:Gem
    {
        public RedGem(SceneManager mSceneMgr, Stat score) : base(mSceneMgr, score)
        {
            this.increase = 50;
            LoadModel();
        }

        protected override void LoadModel()
        {
            this.gameNode = mSceneMgr.CreateSceneNode();
            this.gameEntity = mSceneMgr.CreateEntity("Gem.mesh");
            this.gameEntity.SetMaterialName("RedGem");
            this.gameEntity.GetMesh().BuildEdgeList();
            this.gameNode.Scale(new Vector3(3, 3, 3));
            this.gameNode.AttachObject(gameEntity);
            mSceneMgr.RootSceneNode.AddChild(gameNode);

            base.LoadModel();

        }   
        
    }
}
