using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : LevelObject
{
    protected int amount = 1;
    protected bool onScene = true;

    public string itemName = "default name";

    protected PlayerController playerController;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
        this.gameObject.GetComponent<Collider>().isTrigger = true;
    }
    // Check if item is on active scene
    public bool IsOnScene()
    {
        return onScene;
    }

    // Actions if item is picked up by player
    public void PickUp()
    {
        onScene = false;
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

    public void IncreaseAmount(int num)
    {
        amount += num;
    }
     
    public int GetAmount()
    {
        return amount;
    }
}
