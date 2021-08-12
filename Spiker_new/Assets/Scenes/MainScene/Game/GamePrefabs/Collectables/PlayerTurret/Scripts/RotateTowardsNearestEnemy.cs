using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsNearestEnemy : MonoBehaviour
{
    public float rotateSpeed = 0.1f;
    private float timeCount = 0.0f;
    protected PlayerTurret playerTurret;
    void Start()
    {
        playerTurret = this.transform.GetComponentInParent<PlayerTurret>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateToEnemy();
    }

    void RotateToEnemy()
    {
        Vector3 dir = playerTurret.CountEnemyDirection(this.transform.position).normalized;
        if (playerTurret.IsInRange())
        {
            Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.Lerp(this.transform.rotation, rotation, timeCount);
            timeCount = timeCount + Time.deltaTime * rotateSpeed;
        }
    }
}
