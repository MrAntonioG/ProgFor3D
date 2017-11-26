using Mogre;
using Mogre.TutorialFramework;
using System;
using PhysicsEng;


namespace Coursework
{
    class Level3Stats : LevelStats
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Level3Stats()
        {
            InitStats();
        }
        protected override void InitStats()
        {
            base.InitStats();
            this.numGems = 20;
            this.numHealth = 3;
            this.numShield = 3;
            this.numLives = 2;
            this.numEnemies = 5;
            this.numCGs = 2;
        }
    }
}
