using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    Text scoreText;
    GameObject Player;
    public int yMax = 0;

    void Start()
    {
        scoreText = GetComponent<Text>();
        Player = GameObject.Find("Player Model");
        scoreText.text = "SCORE: 0";
    }

    void Update()
    {
        if (yMax < Player.transform.position.y)
        {
            yMax = (int)Player.transform.position.y;
        }
        scoreText.text = "SCORE: " + yMax.ToString();
    }
}
