using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform SpawnPoint;
    
    //[SerializeField] Transform[] SpawnPoints = new Transform[5];

    [SerializeField] GameObject nomalEnemy;
    [SerializeField] GameObject bossEnemy;


    private void Awake()
    {
        GameManager.Instance.spawnManager = this;
    }

    /// <summary>
    /// Enemy Spawn from data
    /// </summary>
    /// <param name="data">spawn data</param>
    public void NextSpawn(SpawnData data)
    {
        for(int i = 0; i < data.nomalEnemys; i++)
        {
            GameObject enemy = Instantiate(nomalEnemy, SpawnPoint.position, Quaternion.identity);
            GameManager.Instance.enemies.Add(enemy.GetComponent<EnemyController>());
        }

        for(int i = 0; i < data.BossEnemys; i++)
        {
            GameObject enemy = Instantiate(bossEnemy, SpawnPoint.position, Quaternion.identity);
            GameManager.Instance.enemies.Add(enemy.GetComponent<EnemyController>());
        }
    }
}
