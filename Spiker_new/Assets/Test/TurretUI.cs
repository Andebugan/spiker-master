using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretUI : MonoBehaviour
{
    public Text text;
    PlayerController playerController;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Mathf.RoundToInt(playerController.GetPlayer().GetCollectableAmount("turret")).ToString();
    }
}
