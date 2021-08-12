using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : PlayerItem
{
    protected Shield shield;

    void Start()
    {
        shield = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Shield>();
        shield.GetPlayerController().Subscribe(this);
    }

    override public void OnPlayerKill()
    {
        UseShieldItem();
    }

    public void UseShieldItem()
    {
        shield.Use();
        if (shield.CheckExpire())
        {
            shield.GetPlayerController().GetPlayer().invulnerable = false;
            shield.GetPlayerController().GetPlayer().RemoveCollectable(shield.itemName);
            Destroy(this.gameObject);
        }
    }
}
