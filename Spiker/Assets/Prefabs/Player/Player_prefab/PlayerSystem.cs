using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSystem : MonoBehaviour
{
    public bool Dead = false;

    public Transform SpawnPoint;
    public Transform Player;
    public MeshRenderer playerRend;
    public Rigidbody PlayerRb;
    public SpawnScriptUpgraded spawnScript;

    public GameObject shield_pref;

    public GameObject TurretParticlePref;
    public bool TurretParticle = false;
    public float turretParticleCount = 0;

    public GameObject TurretLaserPref;
    public bool TurretLaser = false;
    public float turretLaserCount = 0;

    public bool bomb = false;
    public float bombCount = 0.0f;

    public bool mine = false;
    public float mineCount = 0.0f;

    public bool shield = false;

    public ParticleSystem Death;
    bool deathPlayed = false;

    Scene scene;
    
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {

        if (Dead)
        {
            PlayerRb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            if (deathPlayed == false)
            {
                Death.Play();
                deathPlayed = true;
            }
            playerRend.enabled = false;
        }
        else
        {
            deathPlayed = false;
            playerRend.enabled = true;
        }

        if (shield == true)
        {
            shield_pref.SetActive(true);
        }
        else
        {
            shield_pref.SetActive(false);
        }

        if (TurretParticle == true)
        {
            TurretParticlePref.SetActive(true);
        }
        else
        {
            TurretParticlePref.SetActive(false);
        }

        if (TurretLaser == true)
        {
            TurretLaserPref.SetActive(true);
        }
        else
        {
            TurretLaserPref.SetActive(false);
        }
    }

    public void Respawn()
    {
        PlayerRb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        spawnScript.ClearAll();
        Player.position = SpawnPoint.position;
        Dead = false;
    }
}
