using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    /*
    public float Range;
    public float KillRange;
    Transform target;

    public Collider selfCol_1;
    public Collider selfCol_2;
    public GameObject light_1;
    public GameObject light_2;

    float timer = 0.0f;
    public int maxTime = 500;
    public bool Triggered = false;
    public float startSpeed;
    PlayerSystem playerSystem;
    public GameObject MinePref;
    public int light_val = 100;
    public int light_k = 10;
    float rot_speed = 3.0f;
    public float speed = 0.0f;
    public float speedIncrease = 0.1f;
    public ParticleSystem Explosion;
    GameObject player;
    Rigidbody rb;
    RaycastHit hit;

    void Start()
    {
        player = GameObject.Find("SpawnPoint");
        playerSystem = player.GetComponent<PlayerSystem>();
        target = (GameObject.Find("Player Model")).GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    { 
        MinePref.transform.Rotate((Random.value*rot_speed + 10) * Time.deltaTime, (Random.value*rot_speed + 10) * Time.deltaTime, (Random.value*rot_speed + 10) * Time.deltaTime);

        bool ray = Physics.Raycast(transform.position, target.position - transform.position, out hit, Range);
        if(ray)
        {
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                Triggered = true;
            }
        }

        if(Triggered == true && MinePref.activeSelf)
        {
            RaycastHit dir;
            Physics.Raycast(transform.position, target.position - transform.position, out dir);
            rb.velocity = Vector3.Normalize(dir.point - transform.position)*speed;
            speed += speedIncrease;

            timer += 1;
            if (timer%light_val == 0)
            {
                if(!light_1.activeSelf)
                {
                    light_1.SetActive(true);
                    light_2.SetActive(true);
                }
                else
                {
                    light_1.SetActive(false);
                    light_2.SetActive(false);
                }
                light_val -= light_k;
            }

            if (timer > maxTime)
            {
                if(Physics.Raycast(transform.position, target.position - transform.position, out hit, KillRange))
                {
                    if(hit.collider.gameObject.CompareTag("Player"))
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
                Explosion.Play();
                MinePref.SetActive(false);
                selfCol_1.enabled = false;
                selfCol_2.enabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && Triggered == true)
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
        Explosion.Play();
        MinePref.SetActive(false);
        selfCol_1.enabled = false;
        selfCol_2.enabled = false;
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
        Gizmos.DrawWireSphere(transform.position, KillRange);
    }
    */
}
