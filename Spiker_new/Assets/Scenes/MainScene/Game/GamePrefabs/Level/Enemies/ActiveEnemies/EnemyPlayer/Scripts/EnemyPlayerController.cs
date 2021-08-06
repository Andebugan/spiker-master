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
    public float deltaT = 1.0f;
    protected string state = "idle";
    protected float jumpForce;
    protected float chargeTime;
    protected float currentChargeTime;
    protected Rigidbody rb;
    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
    }

    void Update()
    {
        CheckPlayer();
        if (active)
        {
            if (state == "idle")
                CheckPlayerDistance();
            if (state == "charging")
                Jump();
        }
    }

    void CheckPlayerDistance()
    {
        Vector3 distance = GetPlayerController().GetPlayerTransform().position - this.transform.position;
        if (distance.magnitude < activationRadius)
        {
            state = "charging";
            
            jumpForce = Random.Range(minJumpForce, maxJumpForce);
            chargeTime = Random.Range(minChargeTime, maxChargeTime);
        }
    }

    void Jump()
    {
        Vector3 jumpDir = playerController.GetPlayerTransform().position - this.transform.position;
        jumpDir.Normalize();
        if (chargeTime > 0)
        {
            chargeTime -= Time.deltaTime * deltaT;
        }
        else
        {
            state = "idle";
            rb.AddForce(jumpDir * jumpForce, ForceMode.Impulse);
        }
    }
}
