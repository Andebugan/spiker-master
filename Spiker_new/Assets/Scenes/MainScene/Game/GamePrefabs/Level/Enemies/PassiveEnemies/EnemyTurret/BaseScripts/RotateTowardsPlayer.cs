using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    public float rotateSpeed = 0.1f;
    private float timeCount = 0.0f;
    protected EnemyTurret enemyTurret;
    void Start()
    {
        enemyTurret = this.transform.GetComponentInParent<EnemyTurret>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTurret.IsInRange() && enemyTurret.active)
            RotateToPlayer();
    }

    void RotateToPlayer()
    {
        if (enemyTurret.IsInRange() && enemyTurret.active)
        {
            Vector3 dir = enemyTurret.CountPlayerDirection(this.transform.position).normalized;

            if (Vector3.Dot(dir, enemyTurret.transform.forward) >= 0)
            {
                Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
                transform.rotation = Quaternion.Lerp(this.transform.rotation, rotation, timeCount);
                timeCount = timeCount + Time.deltaTime * rotateSpeed;
            }
        }
    }
}
