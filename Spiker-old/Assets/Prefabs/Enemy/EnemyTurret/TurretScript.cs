using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public float Range = 5.0f;
    Transform target;

    public bool Detected = false;
    public bool Fire = false;
    public float slowRot = 70.0f;
    RaycastHit hit;
    public Transform Gun;

    void Start()
    {
        target = (GameObject.Find("Player Model")).GetComponent<Transform>();
    }

    void Update()
    { 
        bool ray = Physics.Raycast(transform.position, target.position - transform.position, out hit, Range);
        if(ray)
        {
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                Detected = true;
            }
            else
            {
                Detected = false;
            }
        }
        else
        {
            Detected = false;
        }

        if(Detected && !Fire)
        {
            Vector3 dir_fwd = new Vector3(0, -1, 0);
            Vector3 dir  = Gun.position - target.position;
            Quaternion currentRotation = Gun.rotation;
            Quaternion desiredRotation = Quaternion.FromToRotation(dir_fwd , -dir);
            Gun.rotation = Quaternion.RotateTowards(currentRotation, desiredRotation, slowRot * Time.deltaTime);
        }
    }
}
