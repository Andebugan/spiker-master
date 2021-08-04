using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public ParticleSystem firingSystem;
    public float delay = 1.0f;
    public float deltaT = 0.01f;
    protected EnemyTurret enemyTurret;

    void Start()
    {
        enemyTurret = GetComponentInParent<EnemyTurret>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerRaycast();
    }

    void CheckPlayerRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, enemyTurret.attackRadius))
        {
            if (hit.collider.tag == "Player" && enemyTurret.IsInRange() && enemyTurret.active)
            {
                FireWeapon();
            }
        }
    }

    void FireWeapon()
    {
        if (delay == 1.0f)
        {
            firingSystem.Emit(1);
            delay -= deltaT;
        }
        else
        {
            delay -= deltaT;
            if (delay < 0)
                delay = 1.0f;
        }
    }
}
