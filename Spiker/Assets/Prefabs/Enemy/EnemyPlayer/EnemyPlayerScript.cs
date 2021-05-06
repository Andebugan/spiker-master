using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerScript : MonoBehaviour
{
    Rigidbody rb;

    public PlayerSystem playerSystem;
    public Transform target;

    public float thrust = 0.5f;
    public float Range = 5.0f;
    public float ReschargeTime = 100;
    bool reloaded = true;
    public float timer = 0;

    RaycastHit hit;

    void Start()
    {
        GameObject player = GameObject.Find("SpawnPoint");
        playerSystem = player.GetComponent<PlayerSystem>();
        target = (GameObject.Find("Player Model")).GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        bool ray = Physics.Raycast(transform.position, target.position - transform.position, out hit, Range);
        if(ray && reloaded)
        {
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                rb.AddForce(Vector3.Normalize(hit.point - transform.position) * thrust, ForceMode.Impulse);
                reloaded = false;
            }
        }

        if (reloaded == false)
        {
            timer += 1;
            if (timer > ReschargeTime)
            {
                timer = 0;
                reloaded = true;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (playerSystem.shield == false)
            {
                playerSystem.Dead = true;
            }
            else
            {
                playerSystem.shield = false;
            }
        }
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
