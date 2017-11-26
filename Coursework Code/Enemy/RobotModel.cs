using System;
using Mogre;
using PhysicsEng;

namespace Coursework
{   

    class RobotModel:CharacterModel
    {

        Radian angle;           // Angle for the mesh rotation
        Vector3 direction;      // Direction of motion of the mesh for a single frame
        float radius;           // Radius of the circular trajectory of the mesh
        
        
        const float maxTime = 2000f;        // Time when the animation have to be changed
        Timer time;                         // Timer for animation changes
        AnimationState animationState;      // Animation state, retrieves and store an animation from an Entity
        bool animationChanged;              // Flag which tells when the mesh animation has changed

        string animationName;               // Name of the animation to use
        public string AnimationName
        {
            set
            {
                HasAnimationChanged(value);
                if (IsValidAnimationName(value))
                    animationName = value;
                else
                    animationName = "Idle";
            }
        }
        

        

        SceneNode robotNode;        // The node of the scene graph for the robot

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr"></param>
        public RobotModel(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;

            Load();
            AnimationSetup();
        }



        /// <summary>
        /// This method loads the mesh and attaches it to a node and to the schenegraph
        /// </summary>
        private void Load()
        {
            gameEntity = mSceneMgr.CreateEntity("robot.mesh");
            robotNode = mSceneMgr.CreateSceneNode();
            robotNode.AttachObject(gameEntity);

            gameNode = mSceneMgr.CreateSceneNode();
            gameNode.AddChild(robotNode);
            mSceneMgr.RootSceneNode.AddChild(gameNode);

            float radius = 10;
            gameNode.Position += radius * Vector3.UNIT_Y;
            robotNode.Position -= radius * Vector3.UNIT_Y;

            physObj = new PhysObj(radius, "Robot", 0.1f, 0.2f, 0.5f);
            physObj.SceneNode = gameNode;
            physObj.Position = gameNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            Physics.AddPhysObj(physObj);
        }

        /// <summary>
        /// This method detaches the robot node from the scene graph and destroies it and the robot enetity
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
                robotNode.Dispose();
            }

        }

        
        /// <summary>
        /// This method moves the robot in the given direction
        /// </summary>
        /// <param name="direction">The direction along which move the robot</param>
        public override void Move(Vector3 direction)
        {
            //direction = radius * new Vector3(direction.x, 0, direction.z);
            //gameNode.Translate(100 * direction);
            physObj.Velocity = (direction * 50);
        }

        /// <summary>
        /// This method rotate the robot accordingly  with the given angles
        /// </summary>
        /// <param name="angles">The angles by which rotate the robot along each main axis</param>
        public void Rotate(Vector3 angles)
        {
            gameNode.LookAt(angles, Node.TransformSpace.TS_WORLD);
            gameNode.Yaw(new Degree(90));
        }

        
        /// <summary>
        /// This method animates the robot mesh
        /// </summary>
        /// <param name="evt">A frame event which can be used to tune the animation timings</param>
        public override void Animate(FrameEvent evt)
        {
            //CircularMotion(evt);

            
            AnimateMesh(evt);
            
        }



        /// <summary>
        /// This method set up all the field needed for animation
        /// </summary>
        private void AnimationSetup()
        {
            radius = 0.01f; //f = floating point number
            direction = Vector3.ZERO; // 0 0 0
            angle = 0f;

            
            time = new Timer();
            PrintAnimationNames();
            animationChanged = false;
            animationName = "Walk"; //animation name in the skeleton file
            LoadAnimation();
            
        }

        /// <summary>
        /// This method this method makes the mesh move in circle
        /// </summary>
        /// <param name="evt">A frame event which can be used to tune the animation timings</param>
        private void CircularMotion(FrameEvent evt)
        {
            angle += (Radian)evt.timeSinceLastFrame;
            direction = radius * new Vector3(Mogre.Math.Cos(angle), 0, Mogre.Math.Sin(angle));
            robotNode.Translate(direction);
            robotNode.Yaw(-evt.timeSinceLastFrame);
        }

        
        /// <summary>
        /// This method sets the animationChanged field to true whenever the animation name changes
        /// </summary>
        /// <param name="newName"> The new animation name </param>
        private void HasAnimationChanged(string newName)
        {
            if (newName != animationName)
                animationChanged = true;
        }

        /// <summary>
        /// This method prints on the console the list of animation tags
        /// </summary>
        private void PrintAnimationNames()
        {
            AnimationStateSet animStateSet = gameEntity.AllAnimationStates;     // Gets the set of animation states in the Entity
            AnimationStateIterator animIterator = animStateSet.GetAnimationStateIterator();  // Iterates through the animation states

            while (animIterator.MoveNext())                                       // Gets the next animation state in the set
            {
                Console.WriteLine(animIterator.CurrentKey);                      // Print out the animation name in the current key
            }
        }

        /// <summary>
        /// This method deternimes whether the name inserted is in the list of valid animation names
        /// </summary>
        /// <param name="newName">An animation name</param>
        /// <returns></returns>
        private bool IsValidAnimationName(string newName)
        {
            bool nameFound = false;

            AnimationStateSet animStateSet = gameEntity.AllAnimationStates;
            AnimationStateIterator animIterator = animStateSet.GetAnimationStateIterator();

            while (animIterator.MoveNext() && !nameFound)
            {
                if (newName == animIterator.CurrentKey)
                {
                    nameFound = true;
                }
            }

            return nameFound;
        }

        /// <summary>
        /// This method changes the animation name randomly
        /// </summary>
        private void changeAnimationName()
        {
            switch ((int)Mogre.Math.RangeRandom(0, 4.5f))       // Gets a random number between 0 and 4.5f
            {
                case 0:
                    {
                        AnimationName = "Walk";                 // I use the porperty here instead of the field to determine whether I am actualy changing the animation
                        break;
                    }
                case 1:
                    {
                        AnimationName = "Shoot";
                        break;
                    }
                case 2:
                    {
                        AnimationName = "Idle";
                        break;
                    }
                case 3:
                    {
                        AnimationName = "Slump";
                        break;
                    }
                case 4:
                    {
                        AnimationName = "Die";
                        break;
                    }
            }
        }


        /// <summary>
        /// This method loads the animation from the animation name
        /// </summary>
        private void LoadAnimation()
        {
            animationState = gameEntity.GetAnimationState(animationName);
            animationState.Loop = true;
            animationState.Enabled = true;

        }

        /// <summary>
        /// This method puts the mesh in motion
        /// </summary>
        /// <param name="evt">A frame event which can be used to tune the animation timings</param>
        private void AnimateMesh(FrameEvent evt)
        {
            if (time.Milliseconds > maxTime)
            {
                //changeAnimationName();
                time.Reset();
            }

            if (animationChanged)
            {
                LoadAnimation();
                animationChanged = false;
            }

            animationState.AddTime(evt.timeSinceLastFrame); //Needed to make the animation run, and Time differs from machine to machine so use timeSinceLastFrame+
        }


    }
}
