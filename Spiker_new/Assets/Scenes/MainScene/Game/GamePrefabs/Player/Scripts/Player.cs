using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool active = false;
    public bool visible = false;
    public bool alive = true;
    public bool invulnerable = false;

    public float maxVelocity = 5.0f;

    protected List<Collectable> collectables = new List<Collectable>();
    private PlayerMovementSystem movementSystem;
    private Rigidbody playerRigidbody;

   // Init params
    public void Init()
    {
        movementSystem = GetComponent<PlayerMovementSystem>();
        playerRigidbody = GetComponent<Rigidbody>();
        SetActive(false);
        SetVisible(false);
        setAlive(true);
    }
   
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
        if (active)
        {
            movementSystem.enabled = true;
            playerRigidbody.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            movementSystem.enabled = false;
            playerRigidbody.velocity = new Vector3(0, 0, 0);
        }
    }

    public void setAlive(bool aliveParam)
    {
        alive = aliveParam;
        if (!alive)
        {
            SetVisible(false);
            SetActive(false);
            movementSystem.active = false;
        }
        else
        {
            movementSystem.active = true;
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
        bool repeat = false;
        if (collectables.Count != 0)
        {
            for (int i = 0; i < collectables.Count; i++)
            {
                if (collectables[i].GetItemName() == collectable.GetItemName())
                {
                    repeat = true;
                }
            }
            
            if (!repeat)
            {
                collectables.Add(collectable);
                collectable.transform.SetParent(this.transform);
                collectable.transform.position = this.transform.position;
                collectable.SetOnPlayer(true);
            }
            collectable.PickUp();
        }
        else
        {
            collectable.PickUp();
            collectable.transform.SetParent(this.transform);
            collectable.transform.position = this.transform.position;
            collectable.SetOnPlayer(true);
            collectables.Add(collectable);
        }
    }

    public void ChangeAmountOfItem(Collectable collectable)
    {
        if (collectables.Count != 0)
        {
            bool isInPlayer = false;
            for (int i = 0; i < collectables.Count; i++)
            {
                if (collectables[i].GetItemName() == collectable.GetItemName())
                {
                    collectables[i].IncreaseAmount(collectable.GetAmount());
                    collectable.PickUp();
                    isInPlayer = true;
                }
            }
            if (isInPlayer == false)
            {
                AddCollectable(collectable);
            }
        }
        else
        {
            AddCollectable(collectable);
        }
    }

    public int GetCollectableAmount(string name)
    {
        for (int i = 0; i < collectables.Count; i++)
        {
            if (collectables[i].GetItemName() == name)
                return collectables[i].GetAmount();
        }
        return 0;
    }

    // Removes collectables with specified name
    public void RemoveCollectable(string name)
    {
        for (int i = 0; i < collectables.Count; i++)
        {
            if (collectables[i].GetItemName() == name)
            {
                Collectable temp = collectables[i];
                if (temp != null)
                {
                    collectables.Remove(collectables[i]);
                    Destroy(temp.gameObject);
                }
            }
        }
    }

    public void ClearCollectables()
    {
        if (collectables.Count != 0)
        {
            for (int i = 0; i < collectables.Count; i++)
            {
                if (collectables[i] != null)
                {
                    Destroy(collectables[i].gameObject);
                }
            }
        }

        PlayerItem[] playerItems = GetComponentsInChildren<PlayerItem>();
        for (int i = 0; i < playerItems.Length; i++)
        {
            Destroy(playerItems[i].gameObject);
        }

        collectables.Clear();
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
        setAlive(false);
    }

    public void reset()
    {
        setAlive(false);
        ClearCollectables();
    }

    void FixedUpdate()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        if (Mathf.Abs(rb.velocity.magnitude) > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }
}
