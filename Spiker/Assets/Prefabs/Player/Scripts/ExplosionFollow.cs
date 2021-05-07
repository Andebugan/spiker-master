using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionFollow : MonoBehaviour
{
    public Transform target;

    void FixedUpdate()
    {
        transform.position = target.position;
    }
}
