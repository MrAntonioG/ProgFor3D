using Mogre;
using Mogre.TutorialFramework;
using System;
using PhysicsEng;


namespace Coursework
{
    class Level2Stats : LevelStats
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Level2Stats()
        {
            InitStats();
        }
        protected override void InitStats()
        {
            base.InitStats();
            this.numGems = 20;
            this.numHealth = 2;
            this.numShield = 2;
            this.numLives = 2;
            this.numEnemies = 0;
            this.numCGs = 2;
        }
    }
}
