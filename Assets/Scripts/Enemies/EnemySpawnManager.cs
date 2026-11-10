using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] Pool[] enemyPools;
    public float spawnInterval = 2f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-15,15),1.7f,Random.Range(-15,15));
        enemyPools[0].Spawn(spawnPos);
    }
}
