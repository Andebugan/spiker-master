using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerPrefab;
    private Player player;
    public Vector3 spawnCoords = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {
        Instantiate(playerPrefab, spawnCoords, Quaternion.identity);
        player = playerPrefab.GetComponent<Player>();
        player.Init();
        Spawn();
    }

    public void Spawn()
    {
        player.transform.position = spawnCoords;
        player.set();
    }

    public void Ressurect()
    {
        player.set();
    }

    public void Kill()
    {
        player.kill();
    }
}
