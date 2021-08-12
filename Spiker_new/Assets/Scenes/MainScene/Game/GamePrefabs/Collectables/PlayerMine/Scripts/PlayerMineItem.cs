using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMineItem : PlayerItem
{
    protected bool timerStarted;
    public float chargeTime = 5.0f;
    public float currentTime = 0.0f;
    public float deltaT = 1.0f;

    protected PlayerMineExplosionController playerMineExplosionController;
    
    void Start()
    {
        playerMineExplosionController = GetComponentInChildren<PlayerMineExplosionController>();
        StartTimer();
    }

    void Update()
    {
        if (timerStarted)
        {
            if (currentTime > 0)
                currentTime -= Time.deltaTime * deltaT;
            else
                OnTimerEnd();
        }
    }

    protected void StartTimer()
    {
        timerStarted = true;
        currentTime = chargeTime;
    }

    protected void OnTimerEnd()
    {   
        playerMineExplosionController.Explode();
        if (playerMineExplosionController.CheckExplosion())
            Destroy(this.gameObject);
        else
        {
            RandomRotation randomRotation = GetComponentInChildren<RandomRotation>();
            if (randomRotation != null)
            {
                randomRotation.gameObject.SetActive(false);
            }
        }
    }
}
