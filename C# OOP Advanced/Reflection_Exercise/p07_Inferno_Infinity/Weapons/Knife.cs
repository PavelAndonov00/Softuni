namespace p07_Inferno_Infinity.Weapons
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Knife : Weapon 
    {
        private const int defaultMinDamage = 3;
        private const int defaultMaxDamage = 4;
        private const int defaultSocketsCount = 2;

        public Knife()
            : base(defaultMinDamage, defaultMaxDamage, defaultSocketsCount)
        {
        }

        public override void SetLevelOfRarity()
        {
            throw new NotImplementedException();
        }
    }
}
