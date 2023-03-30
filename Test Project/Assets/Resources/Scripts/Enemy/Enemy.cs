using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] public int damage;
    [SerializeField] private Image healthBar;
    private float perHealth;

    [SerializeField] float time = 2f;
    [SerializeField] private float Range;
    [SerializeField] private Transform player;
    private GameObject Effect;
    private GameObject BoomEffect;
    public static Enemy instance;
    private void Start()
    {
        instance = this;    
        perHealth = 1.00f / health;
        Effect = transform.GetChild(1).gameObject;
        BoomEffect= transform.GetChild(3).gameObject;   
        Effect.SetActive(false);
        BoomEffect.SetActive(false);    
    }
    private IEnumerator EffectDamage()
    {
        Effect.SetActive(true);
        BoomEffect.SetActive(true);
        yield return new WaitForSeconds(3f);
        Effect.SetActive(false);
        BoomEffect.SetActive(false) ;
    }
    private IEnumerator BoomEffectCore()
    {
        BoomEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        BoomEffect.SetActive(false) ;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.fillAmount -= perHealth * damage;
        StartCoroutine(BoomEffectCore());
        if (health <= 0)
        {
            Controller.money += 10;
            Spawner.instance.SpawnAfterEnemyDeath();
            Destroy(gameObject);
        }

    }
    public void Damagable()
    {
        health -= damage * 2;
        healthBar.fillAmount -= perHealth * damage;
        StartCoroutine(EffectDamage());
        if (health <= 0)
        {
            Controller.money += 10;
            Spawner.instance.SpawnAfterEnemyDeath();
            Destroy(gameObject);
        }
      
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerStats player))
        {
            player.TakeDamage(damage);
        }
        if(collision.gameObject.TryGetComponent(out PlayerMenu menu))
        {
            menu.TakeDamage(damage);    
        }
    }

  

}
