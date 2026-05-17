using System;

public interface IDamageable
{
    float CurrentHealth { get; }
    float MaxHealth { get; }

    event Action<float, float> OnHealthChanged; // Observer pattern için [cite: 96, 97]
    event Action OnDeath;

    void TakeDamage(float amount);
}