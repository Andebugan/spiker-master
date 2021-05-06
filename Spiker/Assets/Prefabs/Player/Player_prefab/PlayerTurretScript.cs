using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurretTarget : MonoBehaviour
{
    public PlayerSystem playerSystem;

    void OnParticleCollision(GameObject other)
    {
        playerSystem.Dead = true;
    }
}
