using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private GameObject arrowSprite;

    public float spriteLength = 1.0f;
    void Start()
    {
        arrowSprite = GameObject.FindGameObjectWithTag("ArrowSprite");
    }
    public void SetEnabled(bool enabled)
    {
        arrowSprite.GetComponent<Renderer>().enabled = enabled;
    }
    public void TransformArrow(Vector3 shootDirection, Vector3 playerPosition, float shootCoef)
    {
        SetEnabled(true);
        RotateArrow(shootDirection);
        MoveArrow(shootDirection, playerPosition, shootCoef);
    }

    private void RotateArrow(Vector3 shootDirection)
    { 
        transform.rotation = Quaternion.LookRotation(shootDirection);
    }

    private void MoveArrow(Vector3 shootDirection, Vector3 playerPosition, float shootCoef)
    {
        transform.position = playerPosition + shootDirection * spriteLength * shootCoef;
    }
    
}
