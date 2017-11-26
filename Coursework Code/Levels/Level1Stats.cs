using Mogre;
using Mogre.TutorialFramework;
using System;
using PhysicsEng;


namespace Coursework
{
    class Level1Stats: LevelStats
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Level1Stats()
        {
            InitStats();
        }
        protected override void InitStats()
        {
            base.InitStats();
            this.numGems = 15;
            this.numHealth = 0;
            this.numShield = 0;
            this.numLives = 0;
            this.numEnemies = 0;
            this.numCGs = 0;
        }
    }
}
