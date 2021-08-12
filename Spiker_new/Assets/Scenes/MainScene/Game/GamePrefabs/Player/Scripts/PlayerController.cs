using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject playerObject;
    private Player player;
    private CameraController cameraController;
    public Vector3 spawnCoords = new Vector3(0.0f, 0.0f, 0.0f);
    protected List<PlayerItem> subscribedItems = new List<PlayerItem>();

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
        ClearSubscriptions();
    }

    public void Ressurect()
    {
        player.set();
        ClearSubscriptions();
    }

    public void ClearSubscriptions()
    {
        subscribedItems.Clear();
    }

    public void Subscribe(PlayerItem playerItem)
    {
        subscribedItems.Add(playerItem);
    }

    public void Kill()
    {
        if (!player.invulnerable)
            player.kill();
        else
        {
            for (int i = 0; i < subscribedItems.Count; i++)
            {
                if (subscribedItems[i] != null)
                {
                    subscribedItems[i].OnPlayerKill();
                }
            }
        }
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
