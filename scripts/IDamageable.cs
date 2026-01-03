using Godot;

namespace Algonquin1;

public interface IDamageable
{
  int Health { get; set; }
  int MaxHealth { get; set; }

  void TakeDamage(int amount)
  {
    Health -= amount;
    if (Health <= 0) OnDeath();
  }

  void OnDeath();
}