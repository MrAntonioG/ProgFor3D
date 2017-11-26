using System;
using Mogre;
using System.Collections.Generic;


namespace Coursework
{
    /// <summary>
    /// This class implements an example of interface
    /// </summary>
    class GameInterface : HMD     // Game interface inherits form the Head Mounted Dispaly (HMD) class
    {
        private PanelOverlayElement panel;
        private OverlayElement scoreText;
        private OverlayElement levelText;
        private OverlayElement ammoText;
        private OverlayElement timeText;
        private OverlayElement healthBar;
        private OverlayElement shieldBar;
        private Overlay overlay3D;
        private Entity lifeEntity;
        private List<SceneNode> lives;
        private String leveln = "1";
        public String Leveln
        {
            set { leveln = value; }
        }
        private int maxTime = 0;
        public int MaxTime
        {
            set { maxTime = value; }
            get { return maxTime; }
        }
        private Timer time;
        public Timer Time
        {
            set { time = value; }
            get { return time; }
        }

        private float hRatio;
        private float sRatio;
        private string score = "Score: ";
        private string level = "Level ";
        private string ammo = "Ammo: ";
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference of a scene manager</param>
        /// <param name="playerStats">A reference to a character stats</param>
        public GameInterface(SceneManager mSceneMgr,
            RenderWindow mWindow, CharacterStats playerStats)
            : base(mSceneMgr, mWindow, playerStats)  // this calls the constructor of the parent class
        {
            Load("GameInterface");
        }

        /// <summary>
        /// This method initializes the element of the interface
        /// </summary>
        /// <param name="name"> A name to pass to generate the overaly </param>
        protected override void Load(string name)
        {
            base.Load(name);

            lives = new List<SceneNode>();

            healthBar = OverlayManager.Singleton.GetOverlayElement("HealthBar");
            hRatio = healthBar.Width / (float)characterStats.Health.Max;

            shieldBar = OverlayManager.Singleton.GetOverlayElement("ShieldBar");
            sRatio = shieldBar.Width / (float)characterStats.Shield.Max;

            scoreText = OverlayManager.Singleton.GetOverlayElement("ScoreText");
            scoreText.Caption = score;
            scoreText.Left = mWindow.Width * 0.65f;

            ammoText = OverlayManager.Singleton.GetOverlayElement("AmmoText");
            ammoText.Caption = ammo;
            ammoText.Left = mWindow.Width * 0.65f;

            levelText = OverlayManager.Singleton.GetOverlayElement("LevelText");
            levelText.Caption = level;
            levelText.Left = mWindow.Width * 0.35f;

            timeText = OverlayManager.Singleton.GetOverlayElement("TimeText");
            timeText.Caption = "0:00";
            timeText.Left = mWindow.Width * 0.5f;

            panel =
           (PanelOverlayElement)OverlayManager.Singleton.GetOverlayElement("GreenBackground");
            panel.Width = mWindow.Width;
            LoadOverlay3D();
        }

        /// <summary>
        /// This method initalize a 3D overlay
        /// </summary>
        private void LoadOverlay3D()
        {
            overlay3D = OverlayManager.Singleton.Create("3DOverlay");
            overlay3D.ZOrder = 15000;

            CreateHearts();

            overlay3D.Show();
        }

        /// <summary>
        /// This method generate as many hearts as the number of lives left
        /// </summary>
        private void CreateHearts()
        {
            for (int i = 0; i < characterStats.Lives.Value; i++)
                AddHeart(i);
        }

        /// <summary>
        /// This method add an heart to the 3D overlay
        /// </summary>
        /// <param name="n"> A numeric tag</param>
        private void AddHeart(int n)
        {
            SceneNode livesNode = CreateHeart(n);
            lives.Add(livesNode);
            overlay3D.Add3D(livesNode);
        }

        /// <summary>
        /// This method remove from the 3D overlay and destries the passed scene node
        /// </summary>
        /// <param name="life"></param>
        private void RemoveAndDestroyLife(SceneNode life)
        {
            overlay3D.Remove3D(life);
            lives.Remove(life);
            MovableObject heart = life.GetAttachedObject(0);
            life.DetachAllObjects();
            life.Dispose();
            heart.Dispose();
        }

        /// <summary>
        /// This method initializes the heart node and entity
        /// </summary>
        /// <param name="n"> A numeric tag used to determine the heart postion on sceen </param>
        /// <returns></returns>
        private SceneNode CreateHeart(int n)
        {
            lifeEntity = mSceneMgr.CreateEntity("Heart.mesh");
            lifeEntity.SetMaterialName("HeartHMD");
            SceneNode livesNode;
            livesNode = new SceneNode(mSceneMgr);
            livesNode.AttachObject(lifeEntity);
            livesNode.Scale(new Vector3(0.3f, 0.3f, 0.3f));
            livesNode.Position = new Vector3(9f, 9.5f, -9)  - n * 0.9f * Vector3.UNIT_X; ;
            livesNode.SetVisible(true);
            return livesNode;
        }

        /// <summary>
        /// This method converts milliseconds in to minutes and second format mm:ss
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public string convertTime(float time)
        {
            string convTime;
            float secs;
            if(maxTime != 0){
                secs = maxTime - (time / 1000f);
            }
            else
            {
                secs = time / 1000f;
            }
            int min = (int)(secs / 60);
            secs = (int)secs % 60f;
            if (secs < 10)
                convTime = min + ":0" + secs;
            else
                convTime = min + ":" + secs;
            return convTime;
        }

        /// <summary>
        /// This method updates the interface
        /// </summary>
        /// <param name="evt"></param>
        public override void Update(FrameEvent evt)
        {
            base.Update(evt);

            Animate(evt);

            if (lives.Count > characterStats.Lives.Value && characterStats.Lives.Value >= 0)
            {
                SceneNode life = lives[lives.Count - 1];
                RemoveAndDestroyLife(life);

            }
            if (lives.Count < characterStats.Lives.Value)
            {
                AddHeart(characterStats.Lives.Value - 1);
            }

            healthBar.Width = hRatio * characterStats.Health.Value;
            shieldBar.Width = sRatio * characterStats.Shield.Value;
            scoreText.Caption = score + ((PlayerStats)characterStats).Score.Value;
            levelText.Caption = level + leveln;
            ammoText.Caption = ammo + ((PlayerStats)characterStats).Ammo + "/" + ((PlayerStats)characterStats).MaxAmmo;
            timeText.Caption = convertTime(time.Milliseconds);
        }

        /// <summary>
        /// This method animates the heart rotation
        /// </summary>
        /// <param name="evt"></param>
        protected override void Animate(FrameEvent evt)
        {
            foreach (SceneNode sn in lives)
                sn.Yaw(evt.timeSinceLastFrame);
        }

        /// <summary>
        /// This method disposes of the elements generated in the interface
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            List<SceneNode> toRemove = new List<SceneNode>();
            foreach (SceneNode life in lives)
            {
                toRemove.Add(life);
            }
            foreach (SceneNode life in toRemove)
            {
                RemoveAndDestroyLife(life);
            }
            lifeEntity.Dispose();
            toRemove.Clear();

            shieldBar.Hide();
            healthBar.Hide();
            ammoText.Hide();
            scoreText.Hide();
            levelText.Hide();
            timeText.Hide();
            panel.Hide();
            overlay3D.Hide();


            shieldBar.Dispose();
            healthBar.Dispose();
            ammoText.Dispose();
            scoreText.Dispose();
            levelText.Dispose();
            timeText.Dispose();
            panel.Dispose();
            overlay3D.Dispose();
        }
    }
}
