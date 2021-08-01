using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSystem : MonoBehaviour
{
    private Vector3 mousePoint;
    private ArrowController arrow;

    private float currentDistance;
    private float safeSpace;
    public float maxDistance = 1.5f;
    private float shootPower;
    public float borderVal = 1.0f;
    public float shootVar = 1.0f;
    public bool active = true;

    private Vector3 shootDirection;

    /*
     * Initialize player UI elements
     */
    private void Awake()
    {
        arrow = GameObject.FindGameObjectWithTag("Arrow").GetComponent<ArrowController>();
    }

    private void GetMousePosition()
    {
        mousePoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
    }

    private void OnMouseDrag()
    {
        if (!active)
            return;
        currentDistance = Vector3.Distance(mousePoint, transform.position);
        if(currentDistance <= maxDistance)
            safeSpace = currentDistance;
        else
            safeSpace = maxDistance;

        shootPower = Mathf.Abs(safeSpace)*shootVar;

        Vector3 dimxz = mousePoint - transform.position;
        float difference = dimxz.magnitude;

        shootDirection = -Vector3.Normalize(mousePoint - transform.position);
        
        float shootCoef = Mathf.Abs(safeSpace / maxDistance);
        arrow.TransformArrow(shootDirection, transform.position, shootCoef);
    }

    private void OnMouseUp()
    {   
        if (!active)
            return;
        arrow.SetEnabled(false);
        Vector3 push = shootDirection * shootPower;
        if (Mathf.Abs(GetComponent<Rigidbody>().velocity.x) < borderVal && Mathf.Abs(GetComponent<Rigidbody>().velocity.y) < borderVal)
            GetComponent<Rigidbody>().AddForce(push, ForceMode.Impulse);
        else
            GetComponent<Rigidbody>().velocity = push;
    }
    void Update()
    {
        if (!active)
            return;
        GetMousePosition();
    }
}
