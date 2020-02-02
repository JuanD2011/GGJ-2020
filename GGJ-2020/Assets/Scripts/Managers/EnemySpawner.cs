using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private CurrentLevelData levelData;
    private Dictionary<string, float> enemyIdle;
    private Dictionary<string, float> enemyWave;

    private float tick = 1f;
    private float tickTimer = 0f;

    [SerializeField] private GameObject[] enemyPrefabs;

    private bool idle = true;
    private byte waveNumber = 0;

    [SerializeField] private GameObject spawnVolumesParent;
    private Collider[] spawnVolumes;

    public CurrentLevelData LevelData { get => levelData; private set => levelData = value; }

    private void Awake()
    {
        spawnVolumes = spawnVolumesParent.GetComponentsInChildren<Collider>();
        enemyIdle = LevelData.levelData.idleEnemies.ToDictionary(x => x.id, x => x.probability);
        enemyWave = LevelData.levelData.waves[waveNumber].waveEnemies.ToDictionary(x => x.id, x => x.probability);
    }

    private void Update()
    {
        if (!PauseManager.paused)
        {
            if (tickTimer < tick) tickTimer += Time.deltaTime;
            else
            {
                tickTimer = 0f;
                SpawnEnemy();
            } 
        }
    }

    private void SpawnEnemy()
    {
        Collider spawnVolume = spawnVolumes[Random.Range(1, spawnVolumes.Length)];
        Enemy enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)].GetComponent<Enemy>();
        if (idle)
        {
            float probability = enemyIdle[enemy.Id];
            if (Random.Range(0f, 1f) <= probability)
            {
                Instantiate(enemy.gameObject, spawnVolume.GetRandomPointInVolume(), Quaternion.identity);
            }
        }
        else
        {
            float probability = enemyWave[enemy.Id];
            if (Random.Range(0f, 1f) <= probability)
            {
                Instantiate(enemy.gameObject, spawnVolume.GetRandomPointInVolume(), Quaternion.identity);
            }
        }
    }

    public void OnWaveStart()
    {
        idle = false;
        tick = 0.5f;
    }

    public void OnWaveFinished()
    {
        idle = true;
        tick = 1f;
        if (waveNumber < levelData.levelData.waves.Count - 1)
        {
            waveNumber++;
            enemyWave = LevelData.levelData.waves[waveNumber].waveEnemies.ToDictionary(x => x.id, x => x.probability);
        }
    }
}
