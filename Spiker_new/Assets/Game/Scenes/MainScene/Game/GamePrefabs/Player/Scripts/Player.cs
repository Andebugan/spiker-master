using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected bool active = false;
    protected bool visible = false;
    protected bool alive = true;

    protected List<Collectable> collectables;

    // Checks if player is active on field
    public bool isActive()
    {
        return active;
    }

    // Checks if player is visible to other objects on field
    public bool isVisible()
    {
        return visible;
    }

    // Checks if player is alive
    public bool isAlive()
    {
        return alive;
    }

    public void SetVisible(bool visibleParam)
    {
        visible = visibleParam;
    }

    public void SetActive(bool activeParam)
    {
        active = activeParam;
    }

    public void setAlive(bool aliveParam)
    {
        alive = aliveParam;
        if (!alive)
        {
            SetVisible(false);
            SetActive(false);
        }
    }

    // Returns how much collectables player is holding
    public int CollectableCount()
    {
        return collectables.Count;
    }

    // Adds collectable if there is none in list with it's name
    public void AddCollectable(Collectable collectable)
    {
        foreach (Collectable col in collectables)
        {
            if (col.GetItemName() == collectable.GetItemName())
                break;
            else
            {
                collectable.PickUp();
                collectables.Add(collectable);
            }
        }
    }

    // Removes collectables with specified name
    public void RemoveCollectable(string name)
    {
        foreach (Collectable col in collectables)
        {
            if (col.GetItemName() == name)
            {
                collectables.Remove(col);
            }
        }
    }

    // Init params
    void Start()
    {
        SetActive(false);
        SetVisible(false);
        setAlive(true);
    }

    // Sets paremeters to the starting values
    public void set()
    {
        SetActive(true);
        SetVisible(true);
        setAlive(true);
    }

    public void kill()
    {
        SetActive(false);
        SetVisible(false);
        setAlive(false);
    }
}
