using System;
using Mogre;
using PhysicsEng;

namespace Coursework
{
    /// <summary>
    /// This class implements the ground of the environment
    /// </summary>
    class Ground
    {
        SceneManager mSceneMgr;

        Entity groundEntity;
        SceneNode groundNode;

        int groundWidth = 0;
        int groundHeight = 0;
        int groundXSegs = 1;
        int groundZSegs = 1;
        int uTiles = 10;
        int vTiles = 10;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference of the scene manager</param>
        public Ground(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            groundWidth = 1500;
            groundHeight = 1500;
            CreateGround();
        }

        /// <summary>
        /// This method initializes the ground mesh and its node
        /// </summary>
        private void CreateGround()
        {
            GroundPlane();
            groundNode = mSceneMgr.CreateSceneNode();
            groundNode.AttachObject(groundEntity);
            mSceneMgr.RootSceneNode.AddChild(groundNode);
        }

        /// <summary>
        /// This method generates a plane in an Entity which will be used as a ground
        /// </summary>
        private void GroundPlane()
        {
            Plane plane = new Plane(Vector3.UNIT_Y, -10);

            MeshPtr groundMeshPtr = MeshManager.Singleton.CreatePlane("ground",
                ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane, groundWidth,
                groundHeight, groundXSegs, groundZSegs, true, 1, uTiles, vTiles,
                Vector3.UNIT_Z);

            groundEntity = mSceneMgr.CreateEntity("ground");
            groundEntity.SetMaterialName("Ground");
            Physics.AddBoundary(plane);
            //groundEntity.SetMaterialName("waterAnimated");
        }

        /// <summary>
        /// This method disposes of the scene node and enitity
        /// </summary>
        public void Dispose()
        {
            if (groundNode != null)
            {
                if (groundNode.Parent != null)
                {
                    groundNode.Parent.RemoveChild(groundNode);
                }
                groundNode.DetachAllObjects();
                groundNode.Dispose();
                groundEntity.Dispose();
            }
            
        }
    }
}
