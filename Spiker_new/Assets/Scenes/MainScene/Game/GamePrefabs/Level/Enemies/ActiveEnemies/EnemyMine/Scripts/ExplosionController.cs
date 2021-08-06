using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    ParticleSystem explosion;
    protected bool exploded = false;
    void Start()
    {
        explosion = GetComponent<ParticleSystem>();
    }

    public void Explode()
    {
        if (!exploded)
        {
            exploded = true;
            explosion.Play();
        }
    }

    public bool CheckExplosion()
    {
        return !explosion.isPlaying && exploded;
    }
}
