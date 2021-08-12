using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : LevelObject
{
    public bool hasMaxAmount = true;
    public int amount = 1;
    public int maxAmount = 1;
    protected bool onPlayer = false;

    public string itemName = "default name";

    protected PlayerController playerController;

    public void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
        this.gameObject.GetComponent<Collider>().isTrigger = true;
    }

    public PlayerController GetPlayerController()
    {
        return playerController;
    }
    
    // Check if item is on active scene
    public bool IsOnPlayer()
    {
        return onPlayer;
    }

    public void SetOnPlayer(bool isOnPlayer)
    {
        onPlayer = isOnPlayer;
    }

    public void PickUp()
    {
        this.gameObject.GetComponent<Renderer>().enabled = false;
        this.gameObject.GetComponent<Collider>().enabled = false;
    }

    // Check if item is expired
    public bool CheckExpire()
    {
        return amount == 0;
    }

    public void Use()
    {
        amount -= 1;
    }

    // Get item name
    public string GetItemName()
    {
        return itemName;
    }

    public void SetItemName(string name)
    {
        itemName = name;
    }

    virtual public void IncreaseAmount(int num)
    {
        if (amount + num >= maxAmount && hasMaxAmount)
        {
            amount = maxAmount;
        }
        else
            amount += num;
    }
     
    virtual public int GetAmount()
    {
        return amount;
    }
}
