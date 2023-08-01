using Animals.CodeLibrary.Interfaces;

namespace Animals.CodeLibrary.Configurations
{
    public abstract class Animal : IMoveable, IDamageable, IBirthable
    {
        private Type _class => this.GetType();

        public string Name { get; set; }
        public char AnimalSymbol { get; set; }
        public int MovementRange { get; set; }
        public int Speed { get; set; }
        public int MatingTimer { get; set; }
        public int MatingCooldown { get; set; }
        public double MaxHealth { get; set; }
        public double Health { get; set; }
        public bool IsAlive { get; set; }
        public bool IsMating { get; set; }
        public bool IsRecentlyMated { get; set; }
        public Type Class => _class;
        public AnimalType AnimalType { get; set; }
        public (int Row, int Column) BirthPosition { get; set; }

        protected Animal()
        {
            Name = string.Empty;
            IsAlive = false;
            IsMating = false;
            IsRecentlyMated = false;
            MatingCooldown = 0;
            Health = MaxHealth;
        }

        public void Die()
        {
            IsAlive = false;
            Health = MaxHealth;
        }

        public void TakeDamage(double damage)
        {
            Health -= damage;
        }
    }
}
