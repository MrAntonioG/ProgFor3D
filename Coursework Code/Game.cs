using Mogre;
using Mogre.TutorialFramework;
using System;
using PhysicsEng;

namespace Coursework
{
    class Game : BaseApplication
    {
        Player player;              //instance of the player model
        SceneNode cameraNode;       //Scenenode containign camera
        Physics physics;            //Physics engine
        InputsManager inputmngr = InputsManager.Instance; //Handle input from player
        Environment environment;    
        Vector3 cmpos;              
        HMD gameHUD;
        LevelGen level;
        int LvlN;
        bool win, won = false;
        public static void Main()
        {
            new Game().Go();            // This method starts the rendering loop
        }

        /// <summary>
        /// This method create the initial scene
        /// </summary>
        protected override void CreateScene()
        {

            physics = new Physics();
            physics.StartSimTimer();
            player = new Player(mSceneMgr);
            cameraNode = mSceneMgr.CreateSceneNode();
            cameraNode.AttachObject(mCamera);
            player.Model.GameNode.AddChild(cameraNode);

            //creating the ground
            environment = new Environment(mSceneMgr, mWindow);

            inputmngr.Controller = (PlayerController)player.Controller;
            gameHUD = new GameInterface(mSceneMgr, mWindow, player.Stats);
            ((GameInterface)gameHUD).Leveln = LvlN.ToString();
            LvlN = 1;
            createNextLevel();
            
        }


        /// <summary>
        /// This method destrois the scene
        /// </summary>
        protected override void DestroyScene()
        {
            if (player != null)
            {
                player.Dispose();
            }
            if (environment != null)
            {
                environment.Dispose();
            }
            if (level != null)
            {
                level.Dispose();
            }
            physics.StopSimTimer();
            if (gameHUD != null)
            {
                gameHUD.Dispose();
            }
            
        }

        /// <summary>
        /// This method create a new camera and sets it up
        /// </summary>
        protected override void CreateCamera()
        {
            cmpos = new Vector3(0, 60, -60);
            mCamera = mSceneMgr.CreateCamera("PlayerCam");
            mCamera.Position = cmpos;
            mCamera.LookAt(new Vector3(0, 0, 0));
            mCamera.NearClipDistance = 5;
            mCamera.FarClipDistance =  1000;
            mCamera.FOVy = new Degree(100);
            mCameraMan = new CameraMan(mCamera);
            mCameraMan.Freeze = true;
        }

        /// <summary>
        /// This method create a new viewport
        /// </summary>
        protected override void CreateViewports()
        {
            Viewport viewport = mWindow.AddViewport(mCamera);
            mCamera.AspectRatio = viewport.ActualWidth / viewport.ActualHeight;
        }

        /// <summary>
        /// This method update the scene after a frame has finished rendering
        /// </summary>
        /// <param name="evt"></param>
        protected override void UpdateScene(FrameEvent evt)
        {
            
            physics.UpdatePhysics(0.01f);
            base.UpdateScene(evt);
            if (!player.IsDead)
            {
                player.Update(evt);
            }
            
            if(gameHUD != null){
                gameHUD.Update(evt);
            }
            level.Update(evt);
            if (level.Win)
            {
                LvlN++;
                createNextLevel();
            }
            if (!won)
            {
                if ((((GameInterface)gameHUD).MaxTime * 1000) < ((GameInterface)gameHUD).Time.Milliseconds)
                {
                    win = false;
                    WinLose();
                }
                if (player.Stats.Lives.Value == 0)
                {
                    win = false;
                    WinLose();
                }

            }
            
        }

        /// <summary>
        /// Creates next Level
        /// </summary>
        private void createNextLevel()
        {
            if (level != null)
            {
                level.Dispose();
            }
            player.Model.SetPosition(new Vector3(0, 0, 0));
            
            switch (LvlN)
            {
                case 1:
                    level = new LevelGen(player, LvlN, new Level1Stats(), environment, mSceneMgr);
                    ((GameInterface)gameHUD).MaxTime = 180;
                    ((GameInterface)gameHUD).Leveln = LvlN.ToString();
                    ((GameInterface)gameHUD).Time = new Timer();
                    break;
                case 2:
                    level = new LevelGen(player, LvlN, new Level2Stats(), environment, mSceneMgr);
                    ((GameInterface)gameHUD).MaxTime = 240;
                    ((GameInterface)gameHUD).Leveln = LvlN.ToString();
                    ((GameInterface)gameHUD).Time = new Timer();
                    break;
                case 3:
                    level = new LevelGen(player, LvlN, new Level3Stats(), environment, mSceneMgr);
                    ((GameInterface)gameHUD).MaxTime = 360;
                    ((GameInterface)gameHUD).Leveln = LvlN.ToString();
                    ((GameInterface)gameHUD).Time = new Timer();
                    break;
                case 4:
                    win = true;
                    WinLose();
                    break;
            }

        }
        /// <summary>
        /// Win/Lose the Game
        /// </summary>
        private void WinLose()
        {
            String time = ((GameInterface)gameHUD).convertTime(((GameInterface)gameHUD).Time.Milliseconds);
            gameHUD.Dispose();
            gameHUD = new WinScreen(mSceneMgr, mWindow, player.Stats, win, time);
            won = true;
            player.IsDead = true;
        }

        /// <summary>
        /// This method set create a frame listener to handle events before, during or after the frame rendering
        /// </summary>
        protected override void CreateFrameListeners()
        {
            base.CreateFrameListeners();
            mRoot.FrameRenderingQueued += new FrameListener.FrameRenderingQueuedHandler(inputmngr.ProcessInput);
        }

        /// <summary>
        /// This method initilize the inputs reading from keyboard adn mouse
        /// </summary>
        protected override void InitializeInput()
        {
            base.InitializeInput();
            int windowHandle;
            mWindow.GetCustomAttribute("WINDOW", out windowHandle);
            inputmngr.InitInput(ref windowHandle);
        }
        
    }
}