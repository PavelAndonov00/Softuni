namespace _03BarracksFactory.Models.Units
{
    public class Gunner : Unit
    {
        private const int defaultHealth = 20;
        private const int defaultDamage = 20;

        public Gunner() : base(defaultHealth, defaultDamage)
        {
        }
    }
}
