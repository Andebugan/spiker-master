using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerController : ActiveEnemy
{
    public float activationRadius = 6.0f;
    public float minChargeTime = 0.0f;
    public float maxChargeTime = 2.0f;
    public float maxJumpForce = 3.0f;
    public float minJumpForce = 1.0f;
    protected string state = "idle";

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckPlayerDistance()
    {
        Vector3 distance = playerController.GetPlayerTransform().position - this.transform.position;
        if (distance.magnitude < activationRadius)
        {
            state = "charging";
        }
    }
}
