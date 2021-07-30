using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    /*
    Script generates game field in form of corridor in OZ direction
    */
    public float halfCorridorWidth = 15.0f;
    protected PlayerController playerController;
    protected BoarderController boarderController;
    protected SpawnController spawnController;
    protected DeathWallScript deathWallScript;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
        boarderController = this.GetComponent<BoarderController>();
        boarderController.SetWalls(halfCorridorWidth);

        spawnController = this.GetComponent<SpawnController>();
        spawnController.SetStartGenPosition(halfCorridorWidth);

        deathWallScript = this.GetComponent<DeathWallScript>();
        deathWallScript.GenerateWall();
    }

    void Update()
    {
        spawnController.Generate(halfCorridorWidth, playerController.GetPlayerTransform().position.z);
        spawnController.DestroyExpiredObjects(playerController.GetPlayerTransform().position.z);
        boarderController.UpdateWalls(playerController.GetPlayerTransform().position);
    }

    public PlayerController GetPlayerController()
    {
        return playerController;
    }

    public SpawnController GetSpawnController()
    {
        return spawnController;
    }
}
