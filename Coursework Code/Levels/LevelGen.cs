using Mogre;
using Mogre.TutorialFramework;
using System;
using PhysicsEng;
using System.Collections.Generic;

namespace Coursework
{
    class LevelGen : Level
    {
        SceneManager mSceneMgr;
        Environment environment;
        Random r; //Random for generating gems and their position
        protected bool win, collgems, deadenemies;
        public bool Win
        {
            get { return win; }
        }
        public LevelGen(Player p, int lvln, LevelStats lvlstats, Environment environment, SceneManager mSceneMgr)
        {
            this.player = p;
            this.levelno = lvln;
            this.levelStats = lvlstats;
            this.environment = environment;
            this.win = false;
            this.mSceneMgr = mSceneMgr;
            this.gems = new List<Gem>();
            this.powerUps = new List<PowerUp>();
            this.enemies = new List<Enemy>();
            this.guns = new List<CollectableGun>();
            r = new Random();
            CreateLevel();
        }
        /// <summary>
        /// Update items in level
        /// </summary>
        /// <param name="evt"></param>
        public override void Update(FrameEvent evt)
        {
            base.Update(evt);

            int clg = 0;
            if (gems.Count > 0)
            {

                foreach (Gem g in gems)
                {
                    g.Update(evt);
                    if (g.RemoveMe == true)
                    {
                        clg++;
                    }
                }
            }
            if (guns.Count > 0)
            {
                foreach (CollectableGun g in guns)
                {
                    if (!g.RemoveMe)
                    {
                        g.Update(evt);
                    }
                    
                }
            }
            int nen = 0;
            if (enemies.Count > 0)
            {

                foreach (Enemy g in enemies)
                {
                    g.Update(evt);
                    if (g.IsDead == true)
                    {
                        nen++;
                    }
                }
            }
            if (powerUps.Count > 0)
            {
                foreach (PowerUp g in powerUps)
                {
                    g.Update(evt);
                }
            }
            if (nen >= enemies.Count && !deadenemies)
            {
                deadenemies = true;
            }
            if (clg >= gems.Count && !collgems)
            {
                collgems = true;
            }
            if (collgems && deadenemies)
            {
                win = true;
            }
        }
        /// <summary>
        /// Creates Level based on stats
        /// </summary>
        private void CreateLevel()
        {
            if (LevelStats.NumGems != 0)
            {
                collgems = false;
                CreateGems(levelStats.NumGems);
            }
            if (levelStats.NumShield != 0)
            {
                CreateShield(levelStats.NumShield);
            }
            if (levelStats.NumHealth != 0)
            {
                CreateHealth(levelStats.NumHealth);
            }
            if (levelStats.NumLives != 0)
            {
                CreateLives(levelStats.NumLives);
            }
            if (levelStats.NumCGs != 0)
            {
                CreateGuns(levelStats.NumCGs);
            }
            if (levelStats.NumEnemies != 0)
            {
                deadenemies = false;
                CreateEnemies(levelStats.NumEnemies);
            }
            else
            {
                deadenemies = true;
            }
        }
        /// <summary>
        /// Create p Enemies in the level
        /// </summary>
        /// <param name="p"></param>
        private void CreateEnemies(int p)
        {
            for (int x = 1; x <= p; x++)
            {
                Enemy g;
                g = new Robot(mSceneMgr);
                ((EnemyController)g.Controller).Player = player;
                g.Model.SetPosition(new Vector3(RNG(-725, 725), 0, RNG(-725, 725)));
                enemies.Add(g);
            }
        }
        /// <summary>
        /// Create p Collectable Guns in the level
        /// </summary>
        /// <param name="p"></param>
        private void CreateGuns(int p)
        {
            CollectableGun g;
            g = new CollectableGun(mSceneMgr, new Cannon(mSceneMgr), player.Armoury);
            g.SetPosition(new Vector3(RNG(-725, 725), 0, RNG(-725, 725)));
            guns.Add(g);
            if (p > 1)
            {
                g = new CollectableGun(mSceneMgr, new BombDropper(mSceneMgr), player.Armoury);
                g.SetPosition(new Vector3(RNG(-725, 725), 0, RNG(-725, 725)));
                guns.Add(g);
            }
        }
        /// <summary>
        /// Create p Health PowerUps in the level
        /// </summary>
        /// <param name="p"></param>
        private void CreateHealth(int p)
        {
            for (int x = 1; x <= p; x++)
            {
                PowerUp g;
                g = new HealthPU(mSceneMgr, player.Stats.Health);
                g.SetPosition(new Vector3(RNG(-725, 725), 0, RNG(-725, 725)));
                powerUps.Add(g);
            }
        }
        /// <summary>
        /// Generate p Life Powerups in level
        /// </summary>
        /// <param name="p"></param>
        private void CreateLives(int p)
        {
            for (int x = 1; x <= p; x++)
            {
                PowerUp g;
                g = new LifePU(mSceneMgr, player.Stats.Lives);
                g.SetPosition(new Vector3(RNG(-725, 725), 0, RNG(-725, 725)));
                powerUps.Add(g);
            }
        }
        /// <summary>
        /// Generate p shield powerups in level
        /// </summary>
        /// <param name="p"></param>
        private void CreateShield(int p)
        {
            for (int x = 1; x <= p; x++)
            {
                PowerUp g;
                g = new ShieldPU(mSceneMgr, player.Stats.Shield);
                g.SetPosition(new Vector3(RNG(-725, 725), 0, RNG(-725, 725)));
                powerUps.Add(g);
            }
        }
        /// <summary>
        /// Generate p gems in level
        /// </summary>
        /// <param name="p"></param>
        private void CreateGems(int p)
        {
            for (int x = 1; x <= p; x++)
            {
                int gemType = RNG(1, 10);

                Gem g;
                if (gemType < 6)
                {
                    g = new BlueGem(mSceneMgr, ((PlayerStats)player.Stats).Score);
                    g.SetPosition(new Vector3(RNG(-725, 725), 0, RNG(-725, 725)));
                    g.Anchor();
                    gems.Add(g);
                }
                else
                {
                    g = new RedGem(mSceneMgr, ((PlayerStats)player.Stats).Score);
                    g.SetPosition(new Vector3(RNG(-725, 725), 0, RNG(-725, 725)));
                    g.Anchor();
                    gems.Add(g);
                }
            }
        }
        /// <summary>
        /// RandomNumber Generator
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private int RNG(int min, int max)
        {
            return r.Next(min, max);
        }
        /// <summary>
        /// Dispose of Level
        /// </summary>
        public void Dispose()
        {

            if (gems.Count > 0)
            {

                foreach (Gem g in gems)
                {
                    g.Dispose();
                }
            }
            if (guns.Count > 0)
            {
                foreach (CollectableGun g in guns)
                {
                    g.Dispose();
                }
            }
            if (enemies.Count > 0)
            {

                foreach (Enemy g in enemies)
                {
                    g.Dispose();
                }
            }
            if (powerUps.Count > 0)
            {
                foreach (PowerUp g in powerUps)
                {
                    g.Dispose();
                }
            
        }
        
    }
    }
}
