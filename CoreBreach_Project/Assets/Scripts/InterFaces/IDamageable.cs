using System;

public interface IDamageable
{
    float CurrentHealth { get; }
    float MaxHealth { get; }

    // Observer Pattern: Can değiştiğinde ve ölüm anında tetiklenecek event'ler
    event Action<float, float> OnHealthChanged; // (mevcutCan, maksimumCan)
    event Action OnDeath;

    void TakeDamage(float amount);
}