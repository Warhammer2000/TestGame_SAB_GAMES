using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private int maxSpawnCount = 10;
    public static Spawner instance;
    private float _lastSpawnTime;
    private int _spawnCount;
    private Transform spawnPos;

    private void Awake()
    {
        instance = this;
    }
    public void Spawn()
    {
        spawnPos = GameObject.FindGameObjectWithTag("Spawn").transform;
        if (_spawnCount >= maxSpawnCount) return;

        Instantiate(prefabToSpawn, spawnPos.position, Quaternion.identity);

        _lastSpawnTime = Time.time;
        _spawnCount++;
    }
    public void SpawnAfterEnemyDeath()
    {
        Instantiate(prefabToSpawn, spawnPos.position, Quaternion.identity);
    }
    private void Update()
    {
        if (Time.time - _lastSpawnTime >= spawnInterval)
        {
            Spawn();
        }
    }
}


  

//public GameObject SpawngameObject;
//public static Spawner instance;
//private void Start()
//{
//    instance = this;
//    SpawngameObject = Resources.Load<GameObject>("Prefabs/Enemy");
//}
//public void Spawn()
//{
//    Instantiate(SpawngameObject, transform.position, Quaternion.identity);  
//}