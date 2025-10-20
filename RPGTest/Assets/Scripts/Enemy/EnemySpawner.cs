using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemySpawnerConfig[] enemySpawnerConfigs;

    void Start()
    {
        foreach (var config in enemySpawnerConfigs)
        {
            StartCoroutine(SpawnRoutine(config));
        }
    }

    private IEnumerator SpawnRoutine(EnemySpawnerConfig config)
    {
        while (true)
        {
            yield return new WaitUntil(() => config.currentNum < config.maxNum);
            Spawn(config);
            yield return new WaitForSeconds(config.spawnTime);
        }
    }

    private void Spawn(EnemySpawnerConfig config)
    {
        var enemy = GameObject.Instantiate(config.enemyPrefab,
            config.spawnPos ? config.spawnPos.position : transform.position,
            Quaternion.identity);
        if (enemy.TryGetComponent<EnemyStatus>(out var status))
        {
            status.ResignDeathCallback(() => EnemyDie(config));
        }
        config.currentNum++;
    }

    private void EnemyDie(EnemySpawnerConfig config)
    {
        config.currentNum = Mathf.Max(config.currentNum - 1, 0);
    }
}

[Serializable]
public class EnemySpawnerConfig
{
    public Enemy enemyPrefab;
    public int maxNum;
    [HideInInspector] public int currentNum;
    public float spawnTime;
    public Transform spawnPos;
}
