using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthUI : MonoBehaviour
{
    PlayerController playerController;
    public GameObject sprite;
    Stealth stealth;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        stealth = playerController.GetPlayer().GetComponentInChildren<Stealth>();
        if (stealth != null)
        {
            float stealthCoef = stealth.currentTime / stealth.stealthTime;
            sprite.transform.localScale = new Vector3(1, stealthCoef, 1);
        }
        else
        {
            sprite.transform.localScale = new Vector3(1, 0, 1);
        }
    }
}
