using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    protected int amount = 0;
    protected bool onScene = true;

    protected string itemName = "default name";

    // Check if item is on active scene
    public bool IsOnScene()
    {
        return onScene;
    }

    // Actions if item is picked up by player
    public void PickUp()
    {
        onScene = false;
        this.gameObject.SetActive(false);
    }

    // Check if item is expired
    public bool CheckExpire()
    {
        if (amount == 0)
        {
            Destroy(this.gameObject);
        }
        return amount == 0;
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

    void Start()
    {
        CheckExpire();
    }
}
