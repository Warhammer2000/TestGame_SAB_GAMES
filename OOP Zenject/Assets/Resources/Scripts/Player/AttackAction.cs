using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : PlayerAction
{
    private  IEnemyService IenemyService;
    private readonly float attackRange = 50;
    private  EnemyService enemyService;


    [SerializeField] private AudioClip playerSwordSound;
    private AudioSource source;
   
    
    public override void Execute(GameObject playerObject)
    {
        source = playerObject.GetComponent<AudioSource>();
        playerSwordSound = Resources.Load<AudioClip>("Audio/fb5b527adcfa08b");
        Debug.Log(playerSwordSound);
        enemyService = GameObject.Find("Enemy").GetComponent<EnemyService>();   
        
        if (EnemyService.nearestEnemy != null)
        {
            // Атаковать врага
            Enemy.instance.TakeDamage(10);
            Debug.Log("NearestEnemy != null");
            // Воспроизвести звук удара меча
            source.PlayOneShot(playerSwordSound);
            //SoundManager.PlaySwordSound();

            Debug.Log("Игрок атаковал врага");
        }
    }
   
}










//public override void Execute(GameObject playerObject)
//{
//    // Находим ближайшего врага в радиусе 5 метров от игрока
//    Collider[] colliders = Physics.OverlapSphere(playerObject.transform.position, 5f, LayerMask.GetMask("Enemies"));
//    if (colliders.Length > 0)
//    {
//        // Атакуем ближайшего врага
//        Enemy enemy = colliders[0].GetComponent<Enemy>();
//        if (enemy != null)
//        {
//            enemy.TakeDamage(10);
//            Debug.Log("Игрок атаковал врага");
//        }
//    }
//} 
//public AttackAction(IEnemyService enemyService, float attackRange)
//{
//    this.IenemyService = enemyService;
//    this.attackRange = attackRange;
//}