using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckParticleCollision : MonoBehaviour
{
    PlayerController playerController;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            playerController.Kill();
        }
    }
}
