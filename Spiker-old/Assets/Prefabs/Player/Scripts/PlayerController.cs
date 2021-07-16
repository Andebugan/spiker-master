using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject mousePointA;
    private GameObject mousePointB;
    private GameObject arrow;
    private GameObject circle;

    private float currentdistance;
    private float safeSpace;
    public float maxdistance = 1.5f;
    private float shootpower;
    public float borderVal = 1.0f;
    public float ShootVar = 1.0f;

    private Vector3 ShootDirection;

    /*
     * Initialize player UI elements
     */
    private void Awake()
    {
        mousePointA = GameObject.FindGameObjectWithTag("PointA");
        mousePointB = GameObject.FindGameObjectWithTag("PointB");
        arrow = GameObject.FindGameObjectWithTag("Arrow");
        circle = GameObject.FindGameObjectWithTag("Circle");
    }

    private void OnMouseDrag()
    {
        doArrowandCircleStuff();
        currentdistance = Vector3.Distance(mousePointA.transform.position, transform.position);
        if(currentdistance <= maxdistance)
            safeSpace = currentdistance;
        else
            safeSpace = maxdistance;

        shootpower = Mathf.Abs(safeSpace)*ShootVar;

        Vector3 dimxy = mousePointA.transform.position - transform.position;
        float difference = dimxy.magnitude;
        mousePointB.transform.position = transform.position + ((dimxy/difference)*currentdistance*-1);
        mousePointB.transform.position = new Vector3(mousePointB.transform.position.x, mousePointB.transform.position.y, 0.0f);

        ShootDirection = Vector3.Normalize(mousePointA.transform.position - transform.position);
    }

    private void OnMouseUp()
    {
        arrow.GetComponent<Renderer>().enabled = false;
        circle.GetComponent<Renderer>().enabled = false;
        Vector3 push = ShootDirection*shootpower*-1;
        if (Mathf.Abs(GetComponent<Rigidbody>().velocity.x) < borderVal && Mathf.Abs(GetComponent<Rigidbody>().velocity.y) < borderVal)
            GetComponent<Rigidbody>().AddForce(push, ForceMode.Impulse);
        else
            GetComponent<Rigidbody>().velocity = push;
    }

    private void doArrowandCircleStuff()
    {
        arrow.GetComponent<Renderer>().enabled = true;
        circle.GetComponent<Renderer>().enabled = true;
        if(currentdistance <= maxdistance)
        {
            arrow.transform.position = new Vector3((transform.position.x)*2 - mousePointA.transform.position.x, (transform.position.y)*2 - mousePointA.transform.position.y, 0.0f); 
        }
        else
        {
            Vector3 dimxy = mousePointA.transform.position - transform.position;
            float difference = dimxy.magnitude;
            arrow.transform.position = transform.position + ((dimxy/difference)*maxdistance*-1);
            arrow.transform.position = new Vector3(arrow.transform.position.x, arrow.transform.position.y, 0.0f);

        }

        circle.transform.position = transform.position + new Vector3(0, 0, 0.0f);
        Vector3 dir  = mousePointA.transform.position - transform.position + new Vector3(0, 0, 0.8f);
        float rot;
        if((dir.x > 0 && dir.y > 0) || (dir.x < 0 && dir.y > 0))
            rot = Vector3.Angle(dir, transform.right);
        else
            rot = Vector3.Angle(dir, transform.right) * -1;

        arrow.transform.eulerAngles = new Vector3(0, 0, rot);

        float scaleXcircle = Mathf.Log(1 + safeSpace / 2, 2) * 1.6f;
        float scaleYcircle = Mathf.Log(1 + safeSpace / 2, 2) * 1.6f;
        float scaleXarrow = Mathf.Log(1 + safeSpace / 2, 2) * 1f;
        float scaleYarrow = Mathf.Log(1 + safeSpace / 2, 2) * 0.6f;

        arrow.transform.localScale = new Vector3(1 + scaleXarrow, 1 + scaleYarrow, 0.001f);
        circle.transform.localScale = new Vector3(1 + scaleXcircle, 1 + scaleYcircle, 0.001f);
    }
}
