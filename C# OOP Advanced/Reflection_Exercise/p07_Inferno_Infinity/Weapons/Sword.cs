namespace p07_Inferno_Infinity.Weapons
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Sword : Weapon
    {
        private const int defaultMinDamage = 4;
        private const int defaultMaxDamage = 6;
        private const int defaultSocketsCount = 3;

        public Sword()
            : base(defaultMinDamage, defaultMaxDamage, defaultSocketsCount)
        {
        }

        public override void SetLevelOfRarity()
        {
            throw new NotImplementedException();
        }
    }
}
