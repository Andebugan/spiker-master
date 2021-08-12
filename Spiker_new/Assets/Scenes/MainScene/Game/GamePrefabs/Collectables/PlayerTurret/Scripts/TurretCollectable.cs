using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCollectable : Collectable
{
    public TurretItem turretItem;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerController.GetPlayer().ChangeAmountOfItem(this);
            if (IsOnPlayer())
            {
                turretItem.CreatePlayerItem(playerController.GetPlayerTransform());
            }
        }
    }
}
