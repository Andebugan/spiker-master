using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSystem : MonoBehaviour
{
    public bool active = false; // Is player active/inactive

    public GameObject playerUI;
    GameObject playerModel;
    Transform spawnPoint;
    Transform playerTransform;
    MeshRenderer playerRend;
    Rigidbody playerRb;

    public ParticleSystem deathParticles;
    bool deathPlayed = false;

    Scene scene;
    
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        playerModel = GameObject.FindGameObjectWithTag("Player");
        spawnPoint = GetComponent<Transform>();
        playerTransform = playerModel.GetComponent<Transform>();
        playerRend = playerModel.GetComponent<MeshRenderer>();
        playerRb = playerModel.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!active)
        {
            playerRb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            if (deathPlayed == false)
            {
                deathParticles.Play();
                deathPlayed = true;
            }
            playerUI.SetActive(false);
        }
        else
        {
            deathPlayed = false;
            playerUI.SetActive(true);
        }
    }

    public void Respawn()
    {
        playerRb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        playerTransform.position = spawnPoint.position;
        active = false;
    }
}
