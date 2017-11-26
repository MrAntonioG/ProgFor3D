using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace Coursework
{
    /// <summary>
    /// Class representing Players' stats
    /// </summary>
    class PlayerStats:CharacterStats
    {
        protected int ammo;
        public int Ammo
        {
            set { ammo = value; }
            get { return ammo; }
        }
        protected int maxAmmo;
        public int MaxAmmo
        {
            set { maxAmmo = value; }
            get { return maxAmmo; }
        }
        Score score;
        /// <summary>
        /// Read Only method to get score
        /// </summary>
        public Score Score
        {
            get { return score; }
        }
        /// <summary>
        /// constructor
        /// </summary>
        public PlayerStats()
        {
            InitStats();
        }
        /// <summary>
        /// Re-initialise health and shield
        /// </summary>
        public void NewLife()
        {
            health.InitValue(150);
            shield.Decrease(50);
            shield.InitValue(150);
            shield.Decrease(100);
        }
        protected override void InitStats()
        {
            //create new stats
            score = new Score();
            lives = new Stat();
            health = new Stat();
            shield = new Stat();
            ammo = 0;
            maxAmmo = 0;
            //assign values to stats
            score.InitValue(0);
            lives.InitValue(5);
            lives.Decrease(2);
            health.InitValue(150);
            shield.Decrease(50);
            shield.InitValue(150);
            shield.Decrease(100);
        }
    }
}
