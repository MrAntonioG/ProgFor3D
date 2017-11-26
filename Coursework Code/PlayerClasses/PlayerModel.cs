
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace Coursework
{
    class PlayerModel:CharacterModel
     {

        ModelElement mainHull, power, sphere;              // Parts of the player model

      

        SceneNode model, gunGroupNode;                            // Root for the sub-graph

        /// <summary>
        /// This method returns the root node for the sub-scenegraph representing the compound model
        /// </summary>
        public SceneNode Model
        {
            get { return model; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference to the scene graph</param>
        public PlayerModel(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            LoadModelElements();
            AssembleModel();
        }

      
        /// <summary>
        /// Attaches a gun to the model
        /// </summary>
        /// <param name="g">Gun to be attached</param>
        public void AttachGun(Gun g)
        {
            if (gunGroupNode.NumChildren() != 0) //checks if there is/are any gun/s already
            {
                gunGroupNode.RemoveAllChildren();//Removes previous gun(s)
            }
            gunGroupNode.AddChild(g.GameNode); //attaches new gun
        }
        /// <summary>
        /// This method loads the nodes and entities needed by the compound model
        /// </summary>
        protected override void LoadModelElements()
        {

            mainHull = new ModelElement(mSceneMgr, "Main.mesh");
            sphere = new ModelElement(mSceneMgr, "Sphere.mesh");
            power = new ModelElement(mSceneMgr, "PowerCells.mesh");

            gunGroupNode = mSceneMgr.CreateSceneNode(); //SceneNode containing the attached gun

        

            model = mSceneMgr.CreateSceneNode(); //Base node for the entire model
        }

        /// <summary>
        /// This method assemble the model attaching the entities to 
        /// each node and appending the nodes to each other
        /// </summary>
        protected override void AssembleModel()
        {
            mainHull.AddChild(sphere.getModel());
            mainHull.AddChild(power.getModel());
            model.AddChild(mainHull.getModel());
            model.AddChild(gunGroupNode);
            mSceneMgr.RootSceneNode.AddChild(model);
            GameNode = model;
            float radius = 5;
            physObj = new PhysObj(radius, "Player", 1f, 0.7f, 1f);
            physObj.SceneNode = gameNode;
            physObj.Position = gameNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));

            physObj.AddForceToList(new FrictionForce(physObj));
            Physics.AddPhysObj(physObj);
        }

        /// <summary>
        /// This method moves the model as a whole
        /// </summary>
        /// <param name="direction">The direction along which move the model</param>
        public override void Move(Vector3 direction)
        {
            physObj.Velocity = (direction * 100);        // Notice that only the root od the sub-scenegraph is transformed, 
            sphere.GameNode.Rotate(direction/50, 1f);
            // all the sub-nodes are tranformed as a consequence of this transformation
        }
        /// <summary>
        /// Animates the model
        /// </summary>
        /// <param name="evt"></param>
        public override void Animate(FrameEvent evt)
        {
        }
        /// <summary>
        /// This method rotate the model as a whole
        /// </summary>
        /// <param name="quaternion">The quaternion describing the rotation</param>
        /// <param name="transformSpace">The transformation on which rotate the model</param>
        public override void Rotate(Quaternion quaternion,
                     Node.TransformSpace transformSpace = Node.TransformSpace.TS_LOCAL)
        {
            gameNode.Rotate(quaternion, transformSpace);
        }

        /// <summary>
        /// This method detaches and dispode of all the elements of the compound model
        /// </summary>
        public override void DisposeModel()
        {
            if (sphere != null)                     
            {
                if (sphere.getModel().Parent != null)
                    sphere.getModel().Parent.RemoveChild(sphere.getModel());
                sphere.getModel().DetachAllObjects();
                sphere.Dispose();
                //sphereEntity.Dispose();
            }

            if (mainHull != null)
            {
                if (mainHull.getModel().Parent != null)
                    mainHull.getModel().Parent.RemoveChild(mainHull.getModel());
                mainHull.getModel().DetachAllObjects();
                mainHull.Dispose();
                //hullEntity.Dispose();
            }

            if (gameNode != null)                    
            {
                if (gameNode.Parent != null){
                    gameNode.Parent.RemoveChild(model);
                }
                gameNode.DetachAllObjects();
                gameNode.RemoveAllChildren();
                gameNode.Dispose();
            }
            if (physObj != null)
            {
                Physics.RemovePhysObj(physObj);
                physObj = null;
            }
            

        }
     }

}

