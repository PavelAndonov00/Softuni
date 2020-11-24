namespace _03BarracksFactory.Models.Units
{
    public class Horseman : Unit
    {
        private const int defaultHealth = 50;
        private const int defaultDamage = 10;

        public Horseman() : base(defaultHealth, defaultDamage)
        {
        }
    }
}
