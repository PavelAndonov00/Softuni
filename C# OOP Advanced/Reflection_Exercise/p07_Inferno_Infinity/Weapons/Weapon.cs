namespace p07_Inferno_Infinity.Weapons
{
    using p07_Inferno_Infinity.Weapons.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using p07_Inferno_Infinity.WeaponRarity;

    public abstract class Weapon : IWeapon
    {
        protected WeaponRarity weaponRarity;

        protected Weapon(WeaponRarity weaponRarity, int minDamage, int maxDamage, int socketsCount)
        {
            this.weaponRarity = weaponRarity;
            this.MinDamage = minDamage;
            this.MaxDamage = maxDamage;
            this.SocketsCount = socketsCount;
        }

        protected int MinDamage { get; }

        public int MaxDamage { get; }

        public int SocketsCount { get; }

        public abstract void SetLevelOfRarity();
    }
}
