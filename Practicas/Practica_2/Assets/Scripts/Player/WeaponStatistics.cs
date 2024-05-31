using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player
{
    public class WeaponStatistics
    {
        private int ammo;
        private int maxAmmo;

        private bool infiniteAmmo;

        private List<WeaponStatusListener> listeners;

        public WeaponStatistics(int ammo, int maxAmmo, bool infiniteAmmo)
        {
            this.ammo = ammo;
            this.maxAmmo = maxAmmo;
            this.infiniteAmmo = infiniteAmmo;
        }

        public int Ammo
        {
            get { return ammo; }
            set { ammo = value; }
        }

        public int MaxAmmo
        {
            get { return maxAmmo; }
            set { maxAmmo = value; }
        }

        public bool InfiniteAmmo
        {
            get { return infiniteAmmo; }
            set { infiniteAmmo = value; }
        }
    }
}
