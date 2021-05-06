using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEffect : MonoBehaviour
{
    //Tweaks
    [ColorUsageAttribute(true,true)]
    public Color beamColor;

    //FX Objects
    public LineRenderer beam;
    public ParticleSystem[] particles;
    Material beamMaterial;
    public GameObject gunBarrel;
    GameObject player;
    public PlayerSystem playerSystem;
    public TurretScript turretScript; 

    public float chargeTime;
    public float chargeWidth;
    public float fireWidth;
    public float fireTime = 1.0f;
    public float fTime = 1.0f;

    //FX Variables
    public float beamWidthCurrent;

    void Start()
    {
        player = GameObject.Find("SpawnPoint");
        playerSystem = player.GetComponent<PlayerSystem>();
        beamMaterial = beam.material;
        SetParticlesEmission(false);
        UpdateColor();
    }

    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(gunBarrel.transform.position, gunBarrel.transform.forward, out hit);
        if (turretScript.Detected == true && turretScript.Fire == false && playerSystem.Dead == false)
        {
            EnableBeam();
            UpdateBeamPosition(hit.point);
            beamWidthCurrent += chargeWidth/chargeTime;
            if (beamWidthCurrent > chargeWidth)
            {
                turretScript.Fire = true;
            }
        }
        else if (turretScript.Fire == true)
        {
            UpdateBeamPosition(hit.point);
            fireTime -= Time.deltaTime;
            Fire();
            beamWidthCurrent = fireWidth;
            if (fireTime <= 0)
            {
                turretScript.Fire = false;
                fireTime = fTime;
                DisableBeam();
            }
        }
        else
        {
            UpdateBeamPosition(hit.point);
            turretScript.Fire = false;
            fireTime = fTime;
            DisableBeam();
        }
        //DisableBeam();
        beamMaterial.SetFloat("_Thickness", 10f + (1f - beamWidthCurrent) * 100f);
        beamMaterial.SetFloat("_Alpha", beamWidthCurrent);
    }

    void UpdateColor(){
        float intensity = (beamColor.r + beamColor.g + beamColor.b) / 3f;
        beamMaterial.SetColor("_Color", beamColor);
        foreach (ParticleSystem p in particles){
            var main = p.main;
            main.startColor = new Color(beamColor.r / intensity, beamColor.g / intensity, beamColor.b / intensity, beamColor.a);
        }
    }

    void SetParticlesEmission(bool emit){
        foreach (ParticleSystem p in particles){
            var emission = p.emission;
            emission.enabled = emit;
        }
    }

    public void EnableBeam(){
        SetParticlesEmission(true);
    }

    public void UpdateBeamPosition(Vector3 pos){
        beam.SetPosition(1, new Vector3(Vector3.Distance(pos, gunBarrel.transform.position)/20, 0.0f, 0.0f));
        particles[4].transform.position = pos;
        particles[3].transform.position = pos;
        particles[2].transform.position = pos;
    }

    public void DisableBeam(){
        beamWidthCurrent = 0f;
        SetParticlesEmission(false);
    }

    void Fire()
    {
        RaycastHit hit;

        if (Physics.Raycast(gunBarrel.transform.position, gunBarrel.transform.forward, out hit))
        {
            if (hit.collider.tag == "Player")
            {
                if (playerSystem.shield == false)
                {
                    playerSystem.Dead = true;
                }
                else
                {
                    playerSystem.shield = true;
                }
            }
        }
    }
}
