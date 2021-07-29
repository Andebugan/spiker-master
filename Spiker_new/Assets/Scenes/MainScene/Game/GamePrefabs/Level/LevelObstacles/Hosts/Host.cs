using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Host : LevelObject
{
    protected PassiveEnemy[] passiveEnemyList;
    public List<GameObject> enemySpawnPoints;
    protected PassiveEnemy[] enemyList;

    void Start()
    {
        passiveEnemyList = Resources.LoadAll<PassiveEnemy>("EnemyPrefabs");
    }
    public void SetPassiveEnemyList(PassiveEnemy[] enemies)
    {
        enemyList = enemies;
    }
    public void AssembleHostObject()
    {
        Debug.Log("Host object");
    }
}
