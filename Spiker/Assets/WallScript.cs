using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    GameObject Player;
    GameObject Spawn;
    PlayerSystem spawnPosition;

    void Start()
    {
        Player = GameObject.Find("Player Model");
        Spawn = GameObject.Find("SpawnPoint");
        spawnPosition = Spawn.GetComponent<PlayerSystem>();
    }

    void Update()
    {
        if (transform.position.y < Player.transform.position.y)
        {
            transform.position = new Vector3(0.0f, Player.transform.position.y, 0.0f);
        }

        if (spawnPosition.Dead == true)
        {
            transform.position = spawnPosition.SpawnPoint.position;
        }
    }
}
