using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Host : LevelObject
{
    protected SpawnPoint[] enemySpawnPoints;
    protected List<Enemy> enemyList = new List<Enemy>();
    protected HostBuilder hostBuilder;

    void Start()
    {
        enemySpawnPoints = GetComponentsInChildren<SpawnPoint>();
        hostBuilder = GameObject.FindGameObjectWithTag("SpawnController").GetComponent<HostBuilder>();
        hostBuilder.GenerateHost(this);
    }

    public void AddEnemy(Enemy enemy)
    {
        enemyList.Add(enemy);
    }

    public SpawnPoint[] GetSpawnPointList()
    {
        return enemySpawnPoints;
    }
}
