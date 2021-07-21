using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    public Vector3 cameraOffcet;
    public float cameraSmoothSpeed = 0.25f;
    public void CameraInit()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Camera.main.transform.position = player.transform.position + cameraOffcet;
        Camera.main.transform.rotation = Quaternion.Euler(90, 0, 0);
    }
    private void CameraFollow()
    {
        Vector3 desiredPosition = player.transform.position + cameraOffcet;
        Vector3 smoothedPosition = Vector3.Lerp(Camera.main.transform.position, desiredPosition, cameraSmoothSpeed);

        Camera.main.transform.position = smoothedPosition;
    }
    void FixedUpdate()
    {
        CameraFollow();
    }
}
