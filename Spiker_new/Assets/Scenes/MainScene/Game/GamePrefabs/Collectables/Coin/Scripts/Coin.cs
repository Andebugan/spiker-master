using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
        this.gameObject.GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerController.GetPlayer().ChangeAmountOfItem(this);
        }
    }
}
