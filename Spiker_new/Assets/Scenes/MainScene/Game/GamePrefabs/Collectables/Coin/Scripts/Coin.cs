using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerController.GetPlayer().ChangeAmountOfItem(this);
        }
    }
}
