using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEffect : MonoBehaviour
{
    //Tweaks
    [ColorUsageAttribute(true,true)]
    public Color beamColor;
    public float maxDistance = 15.0f;

    //FX Objects
    public LineRenderer beam;
    public ParticleSystem[] particles;
    Material beamMaterial;

    //FX Variables
    float beamWidthCurrent;
    public float beamWidthTarget;

    protected EnemyTurret enemyTurret;

    void Start()
    {
        enemyTurret = GetComponentInParent<EnemyTurret>();
        beamMaterial = beam.material;
        SetParticlesEmission(false);
        UpdateColor();
    }

    void FixedUpdate()
    {
        beamWidthCurrent = Mathf.Lerp(beamWidthCurrent, beamWidthTarget, 26f * Time.deltaTime);
        beamMaterial.SetFloat("_Thickness", 10f + (1f - beamWidthCurrent) * 100f);
        beamMaterial.SetFloat("_Alpha", beamWidthCurrent);
        TargetPlayer();
    }

    void UpdateColor()
    {
        float intensity = (beamColor.r + beamColor.g + beamColor.b) / 3f;
        beamMaterial.SetColor("_Color", beamColor);
        foreach (ParticleSystem p in particles)
        {
            var main = p.main;
            main.startColor = new Color(beamColor.r / intensity, beamColor.g / intensity, beamColor.b / intensity, beamColor.a);
        }
    }

    void SetParticlesEmission(bool emit)
    {
        foreach (ParticleSystem p in particles)
        {
            var emission = p.emission;
            emission.enabled = emit;
        }
    }

    public void TargetPlayer()
    {
        RaycastHit hit;

        Vector3 direction = transform.forward;

        if (Physics.Raycast(transform.position, direction, out hit))
        {
            UpdateBeamPosition(hit.point);
        }
        else
        {
            UpdateBeamPosition(transform.position + direction * maxDistance);
        }
    }

    public void EnableBeam()
    {
        beamWidthTarget = 1f;
        SetParticlesEmission(true);
    }

    public void UpdateBeamPosition(Vector3 pos)
    {
        Vector3 distance = transform.position - pos;
        beam.SetPosition(1,  new Vector3(distance.magnitude, 0, 0));
        particles[4].transform.position = pos;
        particles[3].transform.position = pos;
        particles[2].transform.position = pos;
        if (transform.position - pos != new Vector3 (0, 0, 0))
        {
            particles[3].transform.rotation = Quaternion.LookRotation(transform.position - pos, Vector3.up);
            particles[0].transform.rotation = Quaternion.LookRotation(pos - transform.position, Vector3.up);
        }
    }

    public void DisableBeam()
    {
        beamWidthTarget = 0f;
        SetParticlesEmission(false);
    }
}
