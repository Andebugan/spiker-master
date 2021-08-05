using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWallScript : MonoBehaviour
{
    public GameObject deathWallFragment;
    public float deathWallOffcet;
    protected float halfCorridorWidth;
    protected List<GameObject> wallList = new List<GameObject>();

    public void GenerateWall()
    {
        LevelGeneration levelGeneration = this.GetComponent<LevelGeneration>();
        halfCorridorWidth = levelGeneration.halfCorridorWidth;

        Vector3 coordinates = this.transform.position + new Vector3 (0, 0, -levelGeneration.GetSpawnController().generationDestroyOffcet);
        float sizeX = deathWallFragment.GetComponent<MeshFilter>().sharedMesh.bounds.size.x * deathWallFragment.transform.localScale.x;
        float posX = coordinates.x;
        wallList.Add(Instantiate(deathWallFragment, coordinates, Quaternion.identity));
        while(posX < halfCorridorWidth)
        {
            posX += sizeX;
            wallList.Add(Instantiate(deathWallFragment, coordinates + new Vector3(posX, 0, 0), Quaternion.identity));
            wallList.Add(Instantiate(deathWallFragment, coordinates + new Vector3(-posX, 0, 0), Quaternion.identity));
        }
    }

    public void RegenerateWall()
    {
        LevelGeneration levelGeneration = this.GetComponent<LevelGeneration>();
        halfCorridorWidth = levelGeneration.halfCorridorWidth;
        Vector3 coordinates = this.transform.position + new Vector3 (0, 0, -levelGeneration.GetSpawnController().generationDestroyOffcet);

        for (int i = 0; i < wallList.Count; i++)
        {
            wallList[i].transform.position = new Vector3(wallList[i].transform.position.x, 0, coordinates.z);
        }
    }

    public void UpdateWall()
    {
        LevelGeneration levelGeneration = this.GetComponent<LevelGeneration>();
        for (int i = 0; i < wallList.Count; i++)
        {
            float currDestroyOffcet = levelGeneration.GetPlayerController().GetPlayerTransform().position.z - 
            levelGeneration.GetSpawnController().generationDestroyOffcet;
            if (wallList[i].transform.position.z < currDestroyOffcet)
            {
                wallList[i].transform.position = new Vector3(wallList[i].transform.position.x, wallList[i].transform.position.y, currDestroyOffcet);
            }
        }
    }
}
