using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hardLevel = 1.0f;
    protected bool alive;
    protected bool active;

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

    private void CheckPlayer()
    {
        bool playerActive = playerController.GetPlayer().isActive();
        bool playerAlive = playerController.GetPlayer().isAlive();

        if (!playerActive || !playerAlive)
        {
            active = false;
        }

        if (playerActive && playerAlive)
        {
            active = true;
        }
    }
}
