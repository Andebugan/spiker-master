using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : ActiveEnemy
{
    public float endSpeed = 3.0f;
    protected float currentMoveSpeed;
    protected float startRotation;
    public float endRotation = 2.0f;
    protected float currendRotation;
    public float activationRadius = 6.0f;
    public float detonationTime = 4.0f;
    public float deltaT = 1.0f;
    protected float currentDetonationTime;
    public float instantDetonationRadius = 1.5f;

    protected ExplosionController explosionController;
    protected RandomRotation randomRotation;
    protected string state = "idle";
    protected Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        randomRotation = GetComponentInChildren<RandomRotation>();
        explosionController = GetComponentInChildren<ExplosionController>();
        playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();

        startRotation = randomRotation.rotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();
        if (active)
        {
            CheckInstantExplosion();
            if (state == "idle")
                CheckPlayerDistance();
            if (state == "charging")
                ChargeMine();
            if (state == "detonation")
                Detonate();
        }
    }

    void CheckPlayerDistance()
    {
        Vector3 distance = playerController.GetPlayerTransform().position - this.transform.position;
        if (distance.magnitude < activationRadius)
        {
            state = "charging";
            currentDetonationTime = detonationTime;
            currentMoveSpeed = 0;
            currendRotation = startRotation;
        }
    }

    void CheckInstantExplosion()
    {
        Vector3 distance = playerController.GetPlayerTransform().position - this.transform.position;
        if (distance.magnitude < instantDetonationRadius)
        {
            state = "detonation";
        }
    }

    void ChargeMine()
    {
        if (currentDetonationTime > 0)
        {
            currendRotation = endRotation * (1 - currentDetonationTime / detonationTime);
            randomRotation.rotationSpeed = currendRotation + startRotation;
            currentMoveSpeed = endSpeed * (1 - currentDetonationTime / detonationTime);
            currentDetonationTime -= Time.deltaTime * deltaT;
            Vector3 moveDirection = playerController.GetPlayerTransform().position - this.transform.position;
            moveDirection.Normalize();
            rb.velocity = moveDirection * currentMoveSpeed;
        }
        else
        {
            state = "detonation";
        }
    }

    void Detonate()
    {
        randomRotation.gameObject.SetActive(false);
        
        if (explosionController.CheckExplosion())
        {
            Destroy(this.gameObject);
        }
        
        explosionController.Explode();
    }
}
