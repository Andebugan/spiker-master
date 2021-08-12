using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    protected GameObject instance;
    public void CreatePlayerItem(Transform playerTransform)
    {
        instance = Instantiate(this.gameObject, playerTransform);
    }

    public virtual void OnPlayerKill()
    {
        
    }
}
