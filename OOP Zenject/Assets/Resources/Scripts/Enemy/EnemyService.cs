using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class EnemyService : MonoBehaviour, IEnemyService
{
    [SerializeField]private List<Enemy> enemies;
    public static Enemy nearestEnemy;

    public EnemyService()
    {
        enemies = new List<Enemy>();
    }
    private void Awake()
    {
        enemies.Add(GameObject.Find("Enemy").GetComponent<Enemy>());
        
        Debug.Log(nearestEnemy);
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            float distance = Vector3.Distance(PlayerController.instance.transform.position, enemies[0].transform.position);
            if (distance < 50)
            {
                nearestEnemy = enemies[0];
            }
        }
    }
    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public List<Enemy> GetEnemies()
    {
        return enemies;
    }

    public void DamageEnemy(Enemy enemy, int damageAmount)
    {
        enemy.TakeDamage(damageAmount);
        if (enemy.Die())
        {
            enemies.Remove(enemy);
            Debug.Log("Enemy " + enemy.name + " is dead!");
        }
    }
    public Enemy FindNearestEnemy(Vector3 playerPosition, float maxDistance)
    {
        Enemy enemy = null;
        return enemy;
    }

}

public class EnemyServiceInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IEnemyService>().To<EnemyService>().AsSingle();
    }
}
