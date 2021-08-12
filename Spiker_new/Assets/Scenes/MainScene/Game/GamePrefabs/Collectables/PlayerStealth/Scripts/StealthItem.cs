using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthItem : PlayerItem
{
    protected Stealth stealth;
    protected bool timerStarted;
    
    void Start()
    {
        stealth = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stealth>();
        StartTimer();
    }

    void Update()
    {
        if (timerStarted)
        {
            if (stealth.currentTime > 0)
                stealth.currentTime -= Time.deltaTime * stealth.deltaT;
            else
                OnTimerEnd();
        }
    }

    protected void StartTimer()
    {
        timerStarted = true;
        stealth.currentTime = stealth.stealthTime;
    }

    protected void OnTimerEnd()
    {
        stealth.GetPlayerController().GetPlayer().visible = true;
        stealth.GetPlayerController().GetPlayer().RemoveCollectable(stealth.itemName);
        Destroy(this.gameObject);
    }
}
