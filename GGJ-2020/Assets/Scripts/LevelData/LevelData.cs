using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    public float duration;
    public int pavementModulesNumber;
    public List<EnemyData> idleEnemies;
    public List<WaveData> waves;
}
