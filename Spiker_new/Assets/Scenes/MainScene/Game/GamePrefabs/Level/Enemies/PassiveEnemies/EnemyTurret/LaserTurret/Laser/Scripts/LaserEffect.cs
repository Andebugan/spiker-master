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

    //FX Variables
    float beamWidthCurrent;
    float beamWidthTarget;

    void Start()
    {
        beamMaterial = beam.material;
        SetParticlesEmission(false);
        UpdateColor();
    }

    void Update()
    {
        if (Input.GetMouseButton(0)){
            TargetMouse();
            EnableBeam();
        }else{
            DisableBeam();
        }
        //DisableBeam();
        beamWidthCurrent = Mathf.Lerp(beamWidthCurrent, beamWidthTarget, 26f * Time.deltaTime);
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

    public void TargetMouse(){
        Vector3 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = new Vector3(mousePos.x - transform.position.x, mousePos.y - transform.position.y, transform.position.z);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction.normalized, out hit, direction.magnitude)) {
            UpdateBeamPosition(hit.point);
        }else{
            UpdateBeamPosition(mousePos);
        }
    }

    public void EnableBeam(){
        beamWidthTarget = 1f;
        SetParticlesEmission(true);
    }

    public void UpdateBeamPosition(Vector3 pos){
        beam.SetPosition(1,  pos - transform.position);
        particles[4].transform.position = pos;
        particles[3].transform.position = pos;
        particles[3].transform.rotation = Quaternion.LookRotation(transform.position - pos, Vector3.up);
        particles[2].transform.position = pos;
        particles[0].transform.rotation = Quaternion.LookRotation(pos - transform.position, Vector3.up);
    }

    public void DisableBeam(){
        beamWidthTarget = 0f;
        SetParticlesEmission(false);
    }
}
