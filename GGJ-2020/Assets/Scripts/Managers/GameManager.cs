using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CurrentLevelData levelData;
    private Dictionary<string, float> enemyIdle;
    private Dictionary<string, float> enemyWave;

    private readonly float tick = 1f;
    private float tickTimer = 0f;

    [SerializeField] private GameObject[] enemyPrefabs;

    private int currentEnemyPrefab = 0;

    private bool idle = false;

    private void Awake()
    {
        enemyIdle = levelData.levelData.idleEnemies.ToDictionary(x => x.id, x => x.probability);
    }

    private void Update()
    {
        if (tickTimer < tick) tickTimer += Time.deltaTime;
        else
        {
            tickTimer = 0f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        currentEnemyPrefab = Random.Range(0, enemyPrefabs.Length);
        Enemy enemy = enemyPrefabs[currentEnemyPrefab].GetComponent<Enemy>();
        if (idle)
        {
            float probability = enemyIdle[enemy.Id]; 
        }


    }
}
