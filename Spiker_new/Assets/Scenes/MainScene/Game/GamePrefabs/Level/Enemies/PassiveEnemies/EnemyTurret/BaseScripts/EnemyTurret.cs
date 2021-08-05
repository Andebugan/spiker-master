using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : PassiveEnemy
{
    public float attackRadius = 7.0f;

    protected bool inRange = false;
    
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
    }

    void Update()
    {   
        CheckPlayer();
        CheckPlayerDistance();
    }

    void CheckPlayerDistance()
    {
        if (Vector3.Distance(playerController.GetPlayerTransform().position, this.transform.position) < attackRadius)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
    }

    public bool IsInRange()
    {
        return inRange;
    }

    public PlayerController GetPlayerController()
    {
        return playerController;
    }

    public Vector3 GetPlayerDirection(Vector3 pos)
    {
        Vector3 playerPos = playerController.GetPlayerTransform().position;
        Vector3 dir = playerPos - pos;
        return dir;
    }
}
