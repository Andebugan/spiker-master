using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float explosionTime = 1.0f;
    public float explosionSize = 2.0f;
    public float explosionKoef = 10.0f;
    protected string state = "idle";
    protected bool hitCheck = false;
    protected PlayerController playerController;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
    }

    public void Explode()
    {
        if (state == "idle")
        {
            LeanTween.scale(this.gameObject, new Vector3(1, 1, 1) * explosionSize, explosionTime / explosionKoef);
            state = "fase_1";
        }
        else if (state == "fase_1" && !LeanTween.isTweening(this.gameObject))
        {
            if ((playerController.GetPlayerTransform().position - this.transform.position).magnitude < explosionSize/2 && !hitCheck)
            {
                playerController.Kill();
                hitCheck = true;
            }
            LeanTween.scale(this.gameObject, new Vector3(0, 0, 0), explosionTime);
            state = "fase_2";
        }
        else if (state == "fase_2" && !LeanTween.isTweening(this.gameObject))
        {
            state = "exploded";
        }
    }

    public bool CheckExplosion()
    {
        return state == "exploded";
    }
}
