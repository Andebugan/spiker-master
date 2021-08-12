using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyCollision : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.Kill();
        }
    }
}
