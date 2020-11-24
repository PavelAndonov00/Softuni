namespace p07_Inferno_Infinity.Weapons
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Axe : Weapon
    {
        private const int defaultMinDamage = 5;
        private const int defaultMaxDamage = 10;
        private const int defaultSocketsCount = 4;

        public Axe()
            : base(defaultMinDamage, defaultMaxDamage, defaultSocketsCount)
        {
        }

        public override void SetLevelOfRarity(string level)
        {
           
        }
    }
}
