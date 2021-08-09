using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject playerObject;
    private Player player;
    private CameraController cameraController;
    public Vector3 spawnCoords = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {
        cameraController = this.GetComponent<CameraController>();
        playerObject = Instantiate(playerPrefab, spawnCoords, Quaternion.identity);
        player = playerObject.GetComponent<Player>();
        player.Init();
        Spawn();
    }

    public void Spawn()
    {
        player.transform.position = spawnCoords;
        player.reset();
        player.set();
        cameraController.CameraInit();
    }

    public void Ressurect()
    {
        player.set();
    }

    public void Kill()
    {
        player.kill();
    }

    public Transform GetPlayerTransform()
    {
        return playerObject.transform;
    }

    public Player GetPlayer()
    {
        return player;
    }
}
