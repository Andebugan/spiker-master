using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldUI : MonoBehaviour
{
    PlayerController playerController;
    public GameObject[] sprites;
    Shield shield;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].SetActive(false);
        }

        shield = playerController.GetPlayer().GetComponentInChildren<Shield>();
        if (shield != null)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                if (i < shield.amount)
                    sprites[i].SetActive(true);
            }
        }
    }
}
