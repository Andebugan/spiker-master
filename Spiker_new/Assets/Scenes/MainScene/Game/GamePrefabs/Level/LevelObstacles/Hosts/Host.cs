using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Host : LevelObstacle
{
    public float SpawnRadius = 1.0f;
    public List<GameObject> enemySpawnPoints;

    public void AssembleHostObject()
    {
        Debug.Log("Host object");
    }
}
