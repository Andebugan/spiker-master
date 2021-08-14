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

    public float targetBeamWidth = 0.05f;

    public float maxDistance = 8.0f;

    protected float scaleCoef = 0.0f;
    protected float currentChargeTime = 0.0f;
    protected float currentFireTime = 0.0f;
    protected float currentIdleTime = 0.0f;
    
    protected EnemyTurret enemyTurret;
    protected LaserEffect laserEffect;
    public ParticleSystem ChargeGlow;
    public LineRenderer chargeBeam;
    protected string state = "idle";

    void Start()
    {
        currentFireTime = fireTime;
        currentChargeTime = chargeTime;
        laserEffect = GetComponentInChildren<LaserEffect>();
        enemyTurret = GetComponentInParent<EnemyTurret>();
    }

    void SetChargeGlow()
    {
        var emission = ChargeGlow.emission;
        emission.enabled = false;
        ChargeGlow.transform.localScale = new Vector3(0, 0, 0);
        chargeBeam.startWidth = 0;
        chargeBeam.endWidth = 0;
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

    void CheckPlayerDistance()
    {
        Vector3 distance = enemyTurret.GetPlayerController().GetPlayerTransform().position - this.transform.position;
        if (distance.magnitude < maxDistance)
        {   
            laserEffect.DisableBeam();
            currentFireTime = fireTime;
            currentChargeTime = chargeTime;
            SetChargeGlow();
            state = "idle";
        }
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

    void UpdateTargetBeamPosition()
    {
        RaycastHit hit;
        Vector3 pos;
        Vector3 direction = transform.forward;

        if (Physics.Raycast(transform.position, direction, out hit))
        {
            pos = hit.point;
        }
        else
        {
            pos = transform.position + direction * laserEffect.maxDistance;
        }
        Vector3 distance = transform.position - pos;
        chargeBeam.SetPosition(1,  new Vector3(distance.magnitude, 0, 0) * 1.5f);
    }

    void ChargeWeapon()
    {
        if (currentChargeTime > 0)
        {
            if (chargeTime - currentChargeTime != 0)
                scaleCoef = (chargeTime - currentChargeTime)/chargeTime;
            UpdateTargetBeamPosition();
            ChargeGlow.gameObject.transform.localScale = new Vector3(scaleCoef, scaleCoef, scaleCoef);
            chargeBeam.startWidth = scaleCoef * targetBeamWidth;
            chargeBeam.endWidth = scaleCoef * targetBeamWidth;
            currentChargeTime -= Time.deltaTime * deltaT;
        }
        else
        {
            state = "fire";
            currentChargeTime = chargeTime;
            laserEffect.EnableBeam();
            chargeBeam.startWidth = 0;
            chargeBeam.endWidth = 0;
        }
    }

    void FireWeapon()
    {
        if (currentFireTime > 0 && enemyTurret.IsInRange() && enemyTurret.GetPlayerController().GetPlayer().alive)
        {
            currentFireTime -= Time.deltaTime * deltaT;
            if (currentFireTime < fireTime / 10)
            {      
                laserEffect.DisableBeam();
            }
        }
        else
        {
            laserEffect.DisableBeam();
            currentFireTime = fireTime;
            SetChargeGlow();
            state = "idle";
        }
    }
}
