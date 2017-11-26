using System;
using Mogre;
using System.Collections.Generic;


namespace Coursework
{
    /// <summary>
    /// This class implements an example of interface
    /// </summary>
    class WinScreen : HMD     // Game interface inherits form the Head Mounted Dispaly (HMD) class
    {
        private PanelOverlayElement panel;
        private OverlayElement scoreText;
        private OverlayElement timeText;
        private OverlayElement winLoseText;
        private String leveln = "3";
        public String Leveln
        {
            set { leveln = value; }
        }
        private String time;
        public String Time
        {
            set { time = value; }
            get { return time; }
        }
        private bool winLose;
        private string score = "Score: ";
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference of a scene manager</param>
        /// <param name="playerStats">A reference to a character stats</param>
        public WinScreen(SceneManager mSceneMgr,
            RenderWindow mWindow, CharacterStats playerStats, Boolean winLose, String time)
            : base(mSceneMgr, mWindow, playerStats)  // this calls the constructor of the parent class
        {
            this.time = time;
            this.winLose = winLose;
            Load("Win");
        }

        /// <summary>
        /// This method initializes the element of the interface
        /// </summary>
        /// <param name="name"> A name to pass to generate the overaly </param>
        protected override void Load(string name)
        {
            base.Load(name);


            scoreText = OverlayManager.Singleton.GetOverlayElement("ScoreText2");
            scoreText.Caption = score + ((PlayerStats)characterStats).Score.Value.ToString();
            scoreText.Left = mWindow.Width * 0.65f;

            timeText = OverlayManager.Singleton.GetOverlayElement("TimeText2");
            timeText.Caption = time;
            timeText.Left = mWindow.Width * 0.5f;

            if (winLose)
            {
                winLoseText = OverlayManager.Singleton.GetOverlayElement("Win");
                winLoseText.Caption = "You Win!";
                winLoseText.Left = mWindow.Width * 0.5f;
                winLoseText.Top = mWindow.Height * 0.5f;
            }
            else
            {
                winLoseText = OverlayManager.Singleton.GetOverlayElement("Win");
                winLoseText.Caption = "You Lose! Try Again!";
                winLoseText.Left = mWindow.Width * 0.5f;
                winLoseText.Top = mWindow.Height * 0.5f;
            }
            panel =
           (PanelOverlayElement)OverlayManager.Singleton.GetOverlayElement("BigBackground");
            panel.Width = mWindow.Width;
            panel.Height = panel.Height;
        }


        /// <summary>
        /// This method updates the interface
        /// </summary>
        /// <param name="evt"></param>
        public override void Update(FrameEvent evt)
        {
            base.Update(evt);

        }

        /// <summary>
        /// This method disposes of the elements generated in the interface
        /// </summary>
        public override void Dispose()
        {
            scoreText.Dispose();
            winLoseText.Dispose();
            timeText.Dispose();
            panel.Dispose();
            base.Dispose();
        }
    }
}
