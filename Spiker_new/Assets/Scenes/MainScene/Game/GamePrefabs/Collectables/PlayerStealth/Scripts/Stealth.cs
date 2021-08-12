using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : Collectable
{
    public StealthItem stealthItemPrefab;

    public float stealthTime = 5.0f;
    public float currentTime = 0.0f;
    public float deltaT = 1.0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerController.GetPlayer().ChangeAmountOfItem(this);
            playerController.GetPlayer().visible = false;
            if (IsOnPlayer())
            {
                stealthItemPrefab.CreatePlayerItem(playerController.GetPlayerTransform());
            }
        }
    }

    override public void IncreaseAmount(int num)
    {
        currentTime = stealthTime;
    }
     
    override public int GetAmount()
    {
        return amount;
    }
}
