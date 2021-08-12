using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LevelObject
{
    public bool alive;
    public bool active;
    public bool isTargetable = false;
    protected PlayerController playerController;
    void Start()
    {
        alive = true;
        active = true;
        playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
    }

    public void Kill()
    {
        alive = false;
        active = false;
        Destroy(this.gameObject);
    }

    protected float GetPlayerDistance()
    {
        return Mathf.Abs((playerController.GetPlayerTransform().position - transform.position).magnitude);
    }

    public PlayerController GetPlayerController()
    {
        return playerController;
    }

    protected void CheckPlayer()
    {
        bool playerActive = playerController.GetPlayer().isActive();
        bool playerAlive = playerController.GetPlayer().isAlive();
        bool playerVisible = playerController.GetPlayer().isVisible();

        if (!playerActive || !playerAlive || !playerVisible)
        {
            active = false;
        }

        if (playerActive && playerAlive && playerVisible)
        {
            active = true;
        }
    }
}
