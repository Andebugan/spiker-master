using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMineExplosionController : MonoBehaviour
{
    public float explosionTime = 1.0f;
    public float explosionSize = 4.0f;
    public float explosionKoef = 20.0f;
    protected string state = "idle";
    protected bool hitCheck = false;

    public void KillEnemiesInRange()
    {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        for (int i = 0; i < enemies.Length; i++)
        {
            float enemyDist = (enemies[i].transform.position - transform.position).magnitude;
            if (enemyDist < explosionSize/2)
            {
                enemies[i].Kill();
            }
        }
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
            if (!hitCheck)
            {
                KillEnemiesInRange();
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
