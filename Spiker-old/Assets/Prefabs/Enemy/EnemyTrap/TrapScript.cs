using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    /*
    float origScale = 60.0f;
    float currScale = 0.0f;
    public float scaleVar = 1.0f;
    bool inside = false;

    PlayerSystem playerSystem;
    public GameObject TrapPref;
    float interpolationRatio;

    GameObject player;

    void Start()
    {
        TrapPref.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        player = GameObject.Find("SpawnPoint");
        playerSystem = player.GetComponent<PlayerSystem>();
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            inside = true;
            currScale += scaleVar;
            if (origScale < currScale)
            {
                if (playerSystem.shield == false)
                {
                    playerSystem.Dead = true;
                    currScale = 0.0f;
                    TrapPref.transform.localScale = new Vector3(currScale, currScale, currScale);
                }
                else
                {
                    playerSystem.shield = false;
                }
                inside = false;
                interpolationRatio = 0.0f;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        // Destroy everything that leaves the trigger
        if (col.tag == "Player")
        {
            inside = false;
            interpolationRatio = currScale / origScale;
        }
    }

    void Update()
    {
        if (inside == false && currScale > 0)
        {
            currScale -= scaleVar * 3;
        }
        TrapPref.transform.localScale = new Vector3(currScale, currScale, currScale);
    }
    */
}
