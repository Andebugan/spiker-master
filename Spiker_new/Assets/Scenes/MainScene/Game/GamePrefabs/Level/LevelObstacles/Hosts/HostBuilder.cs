using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostBuilder : MonoBehaviour
{
    protected PassiveEnemy[] passiveEnemyList;
    protected System.Random random = new System.Random();
    protected SpawnController spawnController;
    void Start()
    {
        passiveEnemyList = Resources.LoadAll<PassiveEnemy>("EnemyPrefabs");
        spawnController = GetComponent<SpawnController>();
    }

    public void GenerateHost(Host host)
    {
        SpawnPoint[] spawnPoints = host.GetSpawnPointList();
        int index = 0;
        foreach (SpawnPoint spawnPoint in spawnPoints)
        {
            index = random.Next(0, passiveEnemyList.Length);
            if (passiveEnemyList[index].CheckSpawnChance() && passiveEnemyList[index].CheckDifficulty(spawnController.GetDifficulty()))
            {
                GameObject gameObject = Instantiate(passiveEnemyList[index].gameObject, spawnPoint.transform.position,
                 spawnPoint.transform.rotation, host.transform);
                host.AddEnemy(gameObject.GetComponent<Enemy>());
            }
        }
    }
}
