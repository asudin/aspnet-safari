namespace Animals.CodeLibrary.Interfaces
{
    public interface IDamageable
    {
        double Health { get; set; }

        abstract void Die();

        abstract void TakeDamage(double damage);
    }
}
