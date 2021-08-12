using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretItem : PlayerItem
{
    protected TurretCollectable turret;
    
    void Start()
    {
        turret = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<TurretCollectable>();
    }
}
