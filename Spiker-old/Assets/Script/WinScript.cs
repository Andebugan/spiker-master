using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    public GameObject WinMenu;
    public Rigidbody Player;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            WinMenu.SetActive(true);
            Player.velocity = new Vector3(0, 0, 0);
        }
    }
}
