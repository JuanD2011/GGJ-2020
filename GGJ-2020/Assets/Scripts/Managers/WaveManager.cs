﻿using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private CurrentLevelData levelData = null;
    [SerializeField] private Image timeFillBar = null;

    public OwnEventSystem.GameEvent OnWaveStart, OnWaveFinished, OnLevelFinished;

    private float gameTime = 0f, waveTime = 0f;
    private byte waveNumber = 0;

    private bool inWave = false;

    private void Update()
    {
        if (!PauseManager.paused)
        {
            gameTime += Time.deltaTime;
            timeFillBar.fillAmount = gameTime / levelData.levelData.duration;
            if (gameTime >= levelData.levelData.waves[waveNumber].waveTime)
            {
                waveTime += Time.deltaTime;
                if (!inWave)
                {
                    OnWaveStart.Raise();
                    inWave = true;
                }
                if (waveTime >= levelData.levelData.waves[waveNumber].waveDuration)
                {
                    if (waveNumber < levelData.levelData.waves.Count - 1) waveNumber++;
                    waveTime = 0f;
                    if (inWave)
                    {
                        OnWaveFinished.Raise();
                        inWave = false;
                    }
                }
            }
            if (gameTime > levelData.levelData.duration) OnLevelFinished.Raise(); 
        }
    }
}
