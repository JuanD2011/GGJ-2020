using System.Collections.Generic;

[System.Serializable]
public class WaveData
{
    public float waveTime;
    public float waveDuration;

    public List<EnemyData> waveEnemies;
}