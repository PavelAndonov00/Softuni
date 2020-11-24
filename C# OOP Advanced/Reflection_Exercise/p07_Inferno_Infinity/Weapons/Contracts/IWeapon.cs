namespace p07_Inferno_Infinity.Weapons.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IWeapon
    {
        int MinDamage { get; }
        int MaxDamage { get; }
        int SocketsCount { get; }
        void SetLevelOfRarity();
    }
}
