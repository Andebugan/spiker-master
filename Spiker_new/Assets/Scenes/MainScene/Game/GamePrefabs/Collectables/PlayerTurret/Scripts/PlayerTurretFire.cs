using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurretFire : MonoBehaviour
{
    public ParticleSystem firingSystem;
    public float delay = 1.0f;
    public float deltaT = 0.01f;
    protected PlayerTurret playerTurret;

    protected TurretCollectable turretCollectable;

    void Start()
    {
        playerTurret = GetComponentInParent<PlayerTurret>();
        turretCollectable = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<TurretCollectable>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnemyRaycast();
    }

    void CheckEnemyRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, playerTurret.attackRadius))
        {
            Enemy enemy = hit.collider.gameObject.GetComponentInParent<Enemy>();
            if (enemy != null && playerTurret.IsInRange())
            {
                if (enemy.isTargetable)
                    FireWeapon();
            }
        }
    }

    void FireWeapon()
    {
        if (turretCollectable.CheckExpire())
        {
            turretCollectable.GetPlayerController().GetPlayer().RemoveCollectable(turretCollectable.itemName);
            Destroy(playerTurret.gameObject);
        }
        else if (delay == 1.0f)
        {
            firingSystem.Emit(1);
            turretCollectable.Use();
            delay -= deltaT * Time.deltaTime;
        }
        else
        {
            delay -= deltaT;
            if (delay < 0)
                delay = 1.0f;
        }
    }
}
