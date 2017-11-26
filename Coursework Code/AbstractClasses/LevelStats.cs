using Mogre;
using Mogre.TutorialFramework;
using System;
using PhysicsEng;

namespace Coursework
{
    class LevelStats
    {
        protected int numGems;
        /// <summary>
        /// Read Only. Num of Gems in the level
        /// </summary>
        public int NumGems
        {
            get { return numGems;}
        }
        protected int numHealth;
        /// <summary>
        /// Read Only. Num of Health PUs in the level
        /// </summary>
        public int NumHealth
        {
            get { return numHealth; }
        }
        protected int numShield;
        /// <summary>
        /// Read Only. Num of Shield PUs in the level
        /// </summary>
        public int NumShield
        {
            get { return numShield; }
        }
        protected int numLives;
        /// <summary>
        /// Read Only. Num of Lives PUs in the level
        /// </summary>
        public int NumLives
        {
            get { return numLives; }
        }
        protected int  numEnemies ;
        /// <summary>
        /// Read Only. Num of Enemies in the level
        /// </summary>
        public int NumEnemies
        {
            get { return numEnemies; }
        }
        protected int numCGs;
        /// <summary>
        /// Read Only. Num of Collectable Guns in the level
        /// </summary>
        public int NumCGs
        {
            get { return numCGs; }
        }
        /// <summary>
        /// This method initializes the stats of the level
        /// </summary>
        virtual protected void InitStats() { }
    }
}
