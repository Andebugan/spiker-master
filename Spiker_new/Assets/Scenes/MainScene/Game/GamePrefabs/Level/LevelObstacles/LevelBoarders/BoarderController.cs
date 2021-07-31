using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarderController : MonoBehaviour
{
    public GameObject leftWallPrefab;
    public GameObject rightWallPrefab;
    public float wallZOffcet;

    protected float wallSizeZ;
    protected float wallZPosition;
    protected float lowerWallsBoarder;
    protected float upperWallsBoarder;
    
    struct Walls
    {
        public GameObject right;
        public GameObject left;
    };

    Walls wallsNew;
    Walls wallsOld;

    public void SetWalls(float halfCorridorWidth)
    {
        float wallSizeZLeft = leftWallPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size.z * leftWallPrefab.transform.localScale.z;
        float wallSizeZRight = rightWallPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size.z * rightWallPrefab.transform.localScale.z;

        wallSizeZ = Mathf.Max(wallSizeZLeft, wallSizeZRight);

        wallsNew.right = Instantiate(rightWallPrefab, transform.position + new Vector3(halfCorridorWidth, 0, wallZOffcet), Quaternion.identity);
        wallsNew.left = Instantiate(leftWallPrefab, transform.position + new Vector3(-halfCorridorWidth, 0, wallZOffcet), Quaternion.identity);

        wallsOld.right = Instantiate(rightWallPrefab, transform.position + new Vector3(halfCorridorWidth, 0, wallZOffcet - wallSizeZRight), Quaternion.identity);
        wallsOld.left = Instantiate(leftWallPrefab, transform.position + new Vector3(-halfCorridorWidth, 0, wallZOffcet - wallSizeZLeft), Quaternion.identity);

        wallZPosition = transform.position.z;
        lowerWallsBoarder = transform.position.z + wallZOffcet - wallSizeZ - wallSizeZ / 2;
        upperWallsBoarder = transform.position.z + wallZOffcet + wallSizeZ / 2;
    }

    public void UpdateWalls(Vector3 playerPosition)
    {
        if (playerPosition.z > wallZPosition)
        {
            wallsOld.right.transform.position = new Vector3(wallsOld.right.transform.position.x, wallsOld.right.transform.position.y, wallsOld.right.transform.position.z + 2 * wallSizeZ);
            wallsOld.left.transform.position = new Vector3(wallsOld.left.transform.position.x, wallsOld.left.transform.position.y, wallsOld.left.transform.position.z + 2 * wallSizeZ);

            wallZPosition += wallSizeZ;
            upperWallsBoarder += wallSizeZ;
            lowerWallsBoarder += wallSizeZ;

            Walls temp;
            temp = wallsNew;
            wallsNew = wallsOld;
            wallsOld = temp;
        }
    }

    public float GetLowerBoarder()
    {
        return lowerWallsBoarder;
    }

    public float GetUpperBoarder()
    {
        return upperWallsBoarder;
    }
}
