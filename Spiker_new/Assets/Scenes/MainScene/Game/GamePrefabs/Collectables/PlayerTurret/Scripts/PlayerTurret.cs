using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurret : MonoBehaviour
{
    public float attackRadius = 7.0f;

    protected bool inRange = false;

    protected Vector3 enemyDir;
    protected Enemy currentTarget;

    public void SetPlayerDir(Vector3 dir)
    {
        enemyDir = dir;
    }

    public Vector3 GetPlayerDir()
    {
        return enemyDir;
    }

    void GetNearestEnemy()
    {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        float minDist = Mathf.Infinity;
        inRange = false;
        for (int i = 0; i < enemies.Length; i++)
        {
            float enemyDist = (enemies[i].transform.position - transform.position).magnitude;
            if (enemyDist < attackRadius)
            {
                if (minDist > enemyDist && enemies[i].isTargetable)
                {
                    minDist = enemyDist;
                    inRange = true;
                    currentTarget = enemies[i];
                }
            }
        }
    }

    public Vector3 CountEnemyDirection(Vector3 pos)
    {   
        GetNearestEnemy();
        Vector3 dir = new Vector3(0, 0, 0);
        if (inRange)
        {
            dir = currentTarget.transform.position - pos;
        }
        return dir;
    }

    public bool IsInRange()
    {
        return inRange;
    }
}
