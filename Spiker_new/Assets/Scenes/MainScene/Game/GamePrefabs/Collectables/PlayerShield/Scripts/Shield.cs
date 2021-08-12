using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : Collectable
{
    public ShieldItem shieldItemPrefab;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerController.GetPlayer().ChangeAmountOfItem(this);
            playerController.GetPlayer().invulnerable = true;
            if (IsOnPlayer())
            {
                shieldItemPrefab.CreatePlayerItem(playerController.GetPlayerTransform());
            }
        }
    }
}
