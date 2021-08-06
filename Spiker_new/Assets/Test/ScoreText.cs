using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    PlayerController playerController;
    public Text text;
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
    }
    void Update()
    {
        text.text = Mathf.RoundToInt(playerController.GetPlayerTransform().position.z).ToString();
    }
}
