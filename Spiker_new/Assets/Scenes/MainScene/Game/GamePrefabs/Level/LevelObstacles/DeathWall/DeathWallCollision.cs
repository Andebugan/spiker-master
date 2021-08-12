using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWallCollision : MonoBehaviour
{
    PlayerController playerController;
    
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            playerController.GetPlayer().invulnerable = false;
            playerController.Kill();
        }
    }
}
