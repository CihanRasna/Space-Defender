﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> waveConfigs;
    private int startingWave = 0;

    [SerializeField] private bool isLooping;
    IEnumerator Start()
    {
        do
        { 
            yield return StartCoroutine(SpawnAllWaves());
        } while (isLooping);
        
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemy(); enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());
        }
    }
}
