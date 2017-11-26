using Mogre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coursework
{
    class Armoury
    {
        protected List<Gun> collectedGuns; //list containing all collected guns
        /// <summary>
        /// Read Only. List containing all collected guns.
        /// </summary>
        public List<Gun> CollectedGuns
        {
            get { return collectedGuns; }
        }
        protected Gun activeGun; //Gun currently being used by player
        /// <summary>
        /// Read/Write. Gun being actively used
        /// </summary>
        public Gun ActiveGun
        {
            
            set { activeGun = value; }
            get { return activeGun ; }
        }
        protected bool gunChanged; //Boolean saying if gun changed 
        /// <summary>
        /// Read/Write. Boolean dictating if gun changed
        /// </summary>
        public bool GunChanged
        {
            set { gunChanged = value; }
            get { return gunChanged; }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public Armoury()
        {
            collectedGuns = new List<Gun>();
            activeGun = null;
        }
        /// <summary>
        /// Swap active gun with the gun in collectedguns at x
        /// </summary>
        /// <param name="x">index of gun</param>
        public void SwapGun(int x)
        {
            if (activeGun != null && collectedGuns != null)
            {
                if (x <= collectedGuns.Count()){
                    //AddGun(activeGun); //adds active gun back into the list
                    ChangeGun(collectedGuns[x]); //changes active gun to the desired gun
                    //if (gunChanged) //makes sure that gun changed
                   // {
                   //     collectedGuns.RemoveAt(x); //removes gun that is now active
                   // }
                }
            }
        }

        /// <summary>
        /// change activegun to gun passed through the parameter
        /// </summary>
        /// <param name="gun">Name of the gun to change to</param>
        public void ChangeGun(Gun gun)
        {
            activeGun = gun;
            gunChanged = true;
        }

        /// <summary>
        /// Method to add a gun to the armoury
        /// </summary>
        /// <param name="gun">Gun to be added</param>
        public void AddGun(Gun gun)
        {
            bool add = true; //checks if the gun is to be added

            foreach (Gun g in collectedGuns)
            {
                if (add == true && g.GetType() == gun.GetType()) //if the same gun is found
                {
                    g.ReloadAmmo(); //gun is reloaded
                    add = false; //and not added to the list
                }
            }
            if (add == true) {
                //collectedGuns.Add(gun); //gun is added
                //SwapGun(collectedGuns.Count() - 1);
                ChangeGun(gun);
                collectedGuns.Add(gun);
                add = false;

            }
            else
            {
                gun.Dispose();
            }
        }

        /// <summary>
        /// Dispose of all the guns
        /// </summary>
        public void Dispose() { 
            foreach(Gun g in collectedGuns){
                g.Dispose();
            }
            if (ActiveGun != null)
            {
                ActiveGun.Dispose();
            }
        }
    }
}
