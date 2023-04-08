using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 50;
    [SerializeField] private int currentHealth;
    public bool IsDead;
    public static Enemy instance;
    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        instance = this;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public bool Die()
    {
        // Уничтожаем объект врага
        Destroy(gameObject);
        return true;
    }
   
}
public interface IEnemyService
{
    List<Enemy> GetEnemies();
    void DamageEnemy(Enemy enemy, int damageAmount);
}