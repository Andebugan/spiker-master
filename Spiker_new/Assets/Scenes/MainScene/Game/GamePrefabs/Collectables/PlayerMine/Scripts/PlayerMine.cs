using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMine : Collectable
{
    public PlayerMineItem playerMineItem;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerController.GetPlayer().ChangeAmountOfItem(this);
        }
    }

    public void UseMine()
    {
        Use();
        Instantiate(playerMineItem, this.transform.position, Quaternion.identity);
        if (CheckExpire())
        {
            GetPlayerController().GetPlayer().RemoveCollectable(itemName);
            Destroy(this.gameObject);
        }
    }
}
