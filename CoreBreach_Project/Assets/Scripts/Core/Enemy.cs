using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float damageToCore = 10f;

    private float currentHealth;
    private IMovementStrategy movementStrategy; // Strategy Pattern Kontratı

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    // Observer Pattern Eventleri
    public event Action<float, float> OnHealthChanged;
    public event Action OnDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    // Çalışma anında düşmanın hareket tarzını değiştiren sihirli metot (Strategy Setter)
    public void SetMovementStrategy(IMovementStrategy strategy)
    {
        this.movementStrategy = strategy;
    }

    private void Update()
    {
        // Atanmış bir strateji varsa düşmanı yürüt
        movementStrategy?.Move(transform, speed);
    }

    public void TakeDamage(float amount)
    {
        if (currentHealth <= 0) return;

        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke();
        Debug.Log("Enemy destroyed!");
        Destroy(gameObject); // İleride burayı Object Pool'a bağlayacağız!
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Eğer düşman EnergyCore'a çarparsa ona hasar versin
        EnergyCore core = collision.GetComponent<EnergyCore>();
        if (core != null)
        {
            core.TakeDamage(damageToCore);
            Die(); // Çekirdeğe vurunca kendini yok et
        }
    }
}