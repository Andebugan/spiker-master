using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWallScript : MonoBehaviour
{
    PlayerSystem playerSystem; 
    GameObject player;

    void Start()
    {
        player = GameObject.Find("SpawnPoint");
        playerSystem = player.GetComponent<PlayerSystem>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (playerSystem.shield == false && playerSystem.Dead != true)
            {
                playerSystem.Dead = true;
            }
            else
            {
                playerSystem.shield = false;
            }
        }
    }
}
