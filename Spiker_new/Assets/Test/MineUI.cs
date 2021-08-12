using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineUI : MonoBehaviour
{
    public Button button;
    PlayerController playerController;
    PlayerMine playerMine;

    bool pickedUp = false;
    bool detonating = false;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerController.GetPlayer() != null)
        {
            playerMine = playerController.GetPlayer().GetComponentInChildren<PlayerMine>();
            if (playerMine != null && !pickedUp)
            {
                pickedUp = true;
                button.gameObject.SetActive(true);
            }
            
            if (detonating == true && pickedUp == true)
            {
                pickedUp = false;
                detonating = false;
            }
        }
    }

    public void UseMineCollectable()
    {
        button.gameObject.SetActive(false);
        playerMine.UseMine();
        detonating = true;
    }
}
