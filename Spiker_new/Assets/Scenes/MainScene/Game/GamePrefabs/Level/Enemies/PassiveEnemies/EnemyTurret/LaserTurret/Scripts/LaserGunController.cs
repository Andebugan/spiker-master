using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunController : MonoBehaviour
{
    /*
        Player death is checked by the particles on the end of the beam
        Name: HitGlow
        Script: CheckParticleCollision
    */
    public float chargeTime = 1.0f;
    public float fireTime = 1.0f;
    public float deltaT = 0.01f;

    protected float scaleCoef = 0.0f;
    protected float currentChargeTime = 0.0f;
    protected float currentFireTime = 0.0f;
    protected float currentIdleTime = 0.0f;
    
    protected EnemyTurret enemyTurret;
    protected LaserEffect laserEffect;
    protected RotateTowardsPlayer rotateTowardsPlayer;
    public ParticleSystem ChargeGlow;
    protected string state = "idle";

    void Start()
    {
        currentFireTime = fireTime;
        currentChargeTime = chargeTime;
        laserEffect = GetComponentInChildren<LaserEffect>();
        enemyTurret = GetComponentInParent<EnemyTurret>();
        rotateTowardsPlayer = GetComponentInParent<RotateTowardsPlayer>();
    }

    void SetChargeGlow()
    {
        var emission = ChargeGlow.emission;
        emission.enabled = false;
        ChargeGlow.transform.localScale = new Vector3(0, 0, 0);
    }

    void StartCharging()
    {
        var emission = ChargeGlow.emission;
        emission.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerRaycast();
        
        if (state == "charging")
            ChargeWeapon();
        if (state == "fire")
            FireWeapon();
    }

    void CheckPlayerRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, enemyTurret.attackRadius))
        {
            if (hit.collider.tag == "Player" && enemyTurret.IsInRange() && enemyTurret.active)
            {
                state = "charging";
                StartCharging();
            }
        }
    }

    void ChargeWeapon()
    {
        if (currentChargeTime > 0)
        {
            if (chargeTime - currentChargeTime != 0)
                scaleCoef = (chargeTime - currentChargeTime)/chargeTime;
            if (currentChargeTime < chargeTime / 2)
                rotateTowardsPlayer.enabled = false;
            ChargeGlow.gameObject.transform.localScale = new Vector3(scaleCoef, scaleCoef, scaleCoef);
            currentChargeTime -= Time.deltaTime * deltaT;
        }
        else
        {
            state = "fire";
            currentChargeTime = chargeTime;
            laserEffect.EnableBeam();
        }
    }

    void FireWeapon()
    {
        if (currentFireTime > 0)
        {
            currentFireTime -= Time.deltaTime * deltaT;
            if (currentFireTime < fireTime / 10)
            {      
                laserEffect.DisableBeam();
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, enemyTurret.attackRadius))
                {
                    if (hit.collider.tag == "Player" && enemyTurret.active)
                    {
                        enemyTurret.GetPlayerController().Kill();
                    }
                }
            }
        }
        else
        {
            currentFireTime = fireTime;
            SetChargeGlow();
            state = "idle";
            rotateTowardsPlayer.enabled = true;
        }
    }
}
