using System;
using Mogre;
using PhysicsEng;

namespace Coursework
{
    /// <summary>
    /// Class that allows guns to be collected
    /// </summary>
    class CollectableGun:Collectable
    {
        //collected gun
        Gun gun;
        /// <summary>
        /// Read Only. Gun value 
        /// </summary>
        public Gun Gun
        {
            get { return gun; }
        }

        //Armoury that gun is attached to
        Armoury playerArmoury;
        /// <summary>
        /// Write Only. Player's armoury.
        /// </summary>
        public Armoury PlayerArmoury
        {
            set { playerArmoury = value; }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">Scene Manager</param>
        /// <param name="gun">Gun</param>
        /// <param name="playerArmoury">Player's Armoury</param>
        public CollectableGun(SceneManager mSceneMgr, Gun gun, Armoury playerArmoury)
        {
            //Initializing values
            this.mSceneMgr = mSceneMgr;
            this.gun = gun;
            this.playerArmoury = playerArmoury;

            //Creating and attaching scenenode containing the gun model
            this.gameNode = mSceneMgr.CreateSceneNode();
            this.gameNode.Scale(new Vector3(1.5f, 1.5f, 1.5f));
            this.gameNode.AddChild(gun.GameNode);
            mSceneMgr.RootSceneNode.AddChild(this.gameNode);


            //Link to physics engine
            this.physObj = new PhysObj(10, "Gun", 0.1f, 0.5f);
            physObj.SceneNode = gameNode;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.AddForceToList(new FrictionForce(physObj));
            Physics.AddPhysObj(physObj);
        }

        public override void Update(FrameEvent evt)
        {
            base.Update(evt);
            Animate(evt);
            this.remove = this.IsCollidingWith("Player");
            if (remove)
            {
                (gun.GameNode.Parent).RemoveChild(gun.GameNode.Name);
                playerArmoury.AddGun(gun);
                Dispose();
            }
            
        }
        public override void Dispose()
        {
            base.Dispose();
            this.remove = true;
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
            }
        }

        public override void Animate(FrameEvent evt)
        {
            gameNode.Rotate(new Quaternion(Mogre.Math.AngleUnitsToRadians(evt.timeSinceLastFrame*10), Vector3.UNIT_Y));
        }
    }
}
