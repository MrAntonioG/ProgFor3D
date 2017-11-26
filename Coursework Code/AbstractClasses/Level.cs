using Mogre;
using Mogre.TutorialFramework;
using System;
using PhysicsEng;
using System.Collections.Generic;


namespace Coursework
{
    class Level
    {
        protected int levelno;
        /// <summary>
        /// Read Only. Level Number
        /// </summary>
        public int LevelNo
        {
            get { return levelno; }
        }
        protected LevelStats levelStats;
        /// <summary>
        /// Read Only. Level Stats 
        /// </summary>
        public LevelStats LevelStats
        {
            get { return levelStats; }
        }
        protected Player player;
        /// <summary>
        /// Read Only. Player
        /// </summary>
        public Player Player
        {
            get { return player; }
        }
        protected List<Gem> gems;
        /// <summary>
        /// Read Only. List of gems in the level
        /// </summary>
        public List<Gem> Gems
        {
            get { return gems; }
        }
        protected List<PowerUp> powerUps;
        /// <summary>
        /// Read Only. List of PowerUps in the level.
        /// </summary>
        public List<PowerUp> PowerUps
        {
            get { return powerUps; }
        }
        protected List<CollectableGun> guns;
        /// <summary>
        /// Read Only. List of Enemies  in the level
        /// </summary>
        public List<CollectableGun> Guns
        {
            get { return guns; }
        }
        protected List<Enemy> enemies;
        /// <summary>
        /// Read Only. List of Enemies  in the level
        /// </summary>
        public List<Enemy> Enemies
        {
            get { return enemies; }
        }
        

        /// <summary>
        /// This method is to update the Level's state
        /// </summary>
        /// <param name="evt">A frame event which can be used to tune the level update</param>
        virtual public void Update(FrameEvent evt) { }
    }
}
