using System;
using Mogre;
using PhysicsEng;
namespace Coursework
{
    /// <summary>
    /// This class implements the game environment
    /// </summary>
    class Environment
    {
        SceneManager mSceneMgr;             // This field will contain a reference of the scene manages
        RenderWindow mWindow;               // This field will contain a reference to the rendering window
        Light light;                        // This field will contain a reference of a light
        Wall wall;
        SceneNode wallnode;
        Entity wallentitiy;

        Ground ground;                      // This field will contain an istance of the ground object

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference to the scene manager</param>
        public Environment(SceneManager mSceneMgr, RenderWindow mWindow)
        {
            this.mSceneMgr = mSceneMgr;
            this.mWindow = mWindow;

            Load();                                 // This method loads  the environment
        }
        /// <summary>
        /// Loads everything in the environment
        /// </summary>
        private void Load()
        {
            SetLights();
            SetSky();
            SetFog();
            SetShadows();
            SetWall();
            foreach (Plane p in wall.getPlanes()){
                Physics.AddBoundary(p);
            }
            ground = new Ground(mSceneMgr);
        }
        /// <summary>
        /// Creates a wall
        /// </summary>
        private void SetWall()
        {

            wall = new Wall(mSceneMgr);
            MeshPtr wlptr = wall.getCube("wall", "Wall", 1500, 70, 1500);
            wallentitiy = mSceneMgr.CreateEntity("wall_entity","wall");
            wallnode = mSceneMgr.RootSceneNode.CreateChildSceneNode("wall_node");
            wallnode.AttachObject(wallentitiy);

            //using (MaterialPtr wallmat = MaterialManager.Singleton.Create("wallMaterial",
            //    ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME))
            //{
            //    using(TextureUnitState walltxt = wallmat.GetTechnique(0).GetPass(0).CreateTextureUnitState("Purple.png"))
            //    {
            //        wallentitiy.SetMaterialName("wallMaterial");
            //    }
            //}

        }

        /// <summary>
        /// This method sets the lights in the environment
        /// </summary>
        private void SetLights()
        {
            mSceneMgr.AmbientLight = new ColourValue(0.6f, 0.6f, 0.6f);                 // Set the ambient light in the scene

            float range = 1000;                                                         // Sets the light range
            float constantAttenuation = 1;                                              // Sets the constant attenuation of the light [0, 1]
            float linearAttenuation = 0.0001f;                                                // Sets the linear attenuation of the light [0, 1]
            float quadraticAttenuation = 0.00001f;      

            light = mSceneMgr.CreateLight();                                            // Set an instance of a light;

            light.DiffuseColour = ColourValue.Blue;                                      // Sets the color of the light
            light.Position = new Vector3(0, 100, 0);

            light.Type = Light.LightTypes.LT_POINT;

            light.SetAttenuation(range, constantAttenuation,
                      linearAttenuation, quadraticAttenuation); 

        }

        /// <summary>
        /// Method to create ADDITIVE shadows
        /// </summary>
        private void SetShadows()
        {
            mSceneMgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_ADDITIVE;
        }

        /// <summary>
        /// This method dispose of any object instanciated in this class
        /// </summary>
        public void Dispose()
        {
            ground.Dispose();
        }

        private void SetSky()
        {
            mSceneMgr.SetSkyDome(true, "Sky", 1f, 10, 500, true);

            //Plane sky = new Plane(Vector3.NEGATIVE_UNIT_Y, -100);
            //mSceneMgr.SetSkyPlane(true, sky, "Sky", 10, 5, true, 0.5f, 100, 100,
            //    ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);

            //mSceneMgr.SetSkyBox(true, "SkyBox", 10, true);
        }

        private void SetFog()
        {
            ColourValue fadeColour = new ColourValue(0.8f, 0.6f, 0.6f);
            //mSceneMgr.SetFog(FogMode.FOG_LINEAR, fadeColour, 0.1f, 100, 1000);
            mSceneMgr.SetFog(FogMode.FOG_EXP, fadeColour, 0.0013f); //sets the fog, which grows exponansionally
            //mSceneMgr.SetFog(FogMode.FOG_EXP2, fadeColour, 0.0015f);
            mWindow.GetViewport(0).BackgroundColour = fadeColour;
        }
    }
}
