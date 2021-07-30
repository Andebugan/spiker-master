using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWallScript : MonoBehaviour
{
    public GameObject deathWallFragment;
    protected float halfCorridorWidth;
    protected LevelGeneration levelGeneration;
    protected List<GameObject> wallList = new List<GameObject>();

    void Start()
    {
        levelGeneration = this.GetComponent<LevelGeneration>();
        halfCorridorWidth = levelGeneration.halfCorridorWidth;
    }

    public void GenerateWall()
    {
        Vector3 coordinates = this.transform.position + new Vector3 (0, 0, -levelGeneration.GetSpawnController().generationDestroyOffcet);
        float sizeX = deathWallFragment.GetComponent<MeshFilter>().sharedMesh.bounds.size.x * deathWallFragment.transform.localScale.x;
        float posX = coordinates.x;
        Instantiate(deathWallFragment, coordinates, Quaternion.identity);
        while(posX < halfCorridorWidth)
        {
            posX += sizeX;
            Instantiate(deathWallFragment, coordinates + new Vector3(posX, 0, 0), Quaternion.identity);
            Instantiate(deathWallFragment, coordinates + new Vector3(-posX, 0, 0), Quaternion.identity);
        }
    }

    public void UpdateWall()
    {

    }
}
