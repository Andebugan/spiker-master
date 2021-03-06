using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleTrap : PassiveEnemy
{
    public GameObject InnerCircle;
    public GameObject InnerCircleBottom;
    public float time;
    protected Vector3 originalScale;
    protected bool charging = false;
    protected bool playerIsInside = false;
    protected bool hitCheck = false;

    void Start()
    {
        originalScale = InnerCircle.transform.localScale;
        InnerCircle.transform.localScale = new Vector3(0, InnerCircle.transform.localScale.y, 0);
        InnerCircleBottom.transform.localScale = new Vector3(0, InnerCircleBottom.transform.localScale.y, 0);
        playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (playerController.GetPlayer() != null)
            {
                if (playerController.GetPlayer().isVisible())
                {
                    playerIsInside = true;
                    StartTimer();
                }
            }
        }
    }

    void StartTimer()
    {
        LeanTween.scale(InnerCircle, originalScale, time);
        LeanTween.scale(InnerCircleBottom, originalScale, time / 15);
        charging = true;
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            playerIsInside = false;
        }
    }

    void Update()
    {
        CheckPlayer();
        if (active)
        {
            if (charging && !LeanTween.isTweening(InnerCircle))
            {
                if (playerIsInside && !hitCheck)
                {
                    GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>().Kill();
                    hitCheck = true;
                }
                Detonate();
            }
        }
    }

    void Detonate()
    {
        InnerCircle.SetActive(false);
    }
}
