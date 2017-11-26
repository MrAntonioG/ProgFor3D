using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coursework
{
    class EnemyStats:CharacterStats
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EnemyStats(){
            InitStats();
        }
        /// <summary>
        /// Initialise stats
        /// </summary>
        protected override void InitStats()
        {
            //create new stats
            lives = new Stat();
            health = new Stat();
            shield = new Stat();

            //assign values to stats
            lives.InitValue(1);
            health.InitValue(50);
            shield.InitValue(50);
        }
    }
}
