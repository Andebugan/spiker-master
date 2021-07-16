using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    GameObject Player;

    public GameObject[] SmallPrefabArr;
    public GameObject[] BigPrefabArr;

    int[,] spawnBlock;
    int spawnBlockType = 0;

    public float genDelOffcet;
    public float smallRange;
    public float bigRange;
    public float genOffcetUp;
    public Vector3 spawnPosition;

    void Start()
    {
        Player = GameObject.Find("Player Model");
        spawnPosition = transform.position;
    }

    void Update()
    {
        if (Player.transform.position.y + genOffcetUp > spawnPosition.y)
        {
            GenerateLevel(spawnPosition + new Vector3(5.0f, 0.0f, 0.0f));
            GenerateLevel(spawnPosition + new Vector3(-5.0f, 0.0f, 0.0f));
            spawnPosition.y += 10.0f;
        }

        ClearRedundantParts();
    }

    public void GenerateLevel(Vector3 spawnPositionG)
    {
        spawnBlockType = (int)Random.Range(0.0f, 4.0f);
        if (spawnBlockType > 2)
        {
            SpawnObjectInPlace(spawnPositionG, bigRange, BigPrefabArr);
        }
        else
        {
            SpawnObjectInPlace(spawnPositionG + new Vector3(2.5f, 2.5f, 0.0f), smallRange, SmallPrefabArr);
            SpawnObjectInPlace(spawnPositionG + new Vector3(-2.5f, 2.5f, 0.0f), smallRange, SmallPrefabArr);
            SpawnObjectInPlace(spawnPositionG + new Vector3(2.5f, -2.5f, 0.0f), smallRange, SmallPrefabArr);
            SpawnObjectInPlace(spawnPositionG + new Vector3(-2.5f, -2.5f, 0.0f), smallRange, SmallPrefabArr);
        }
    }

    public void ClearRedundantParts()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            if (this.gameObject.transform.GetChild(i).position.y < Player.transform.position.y - genDelOffcet)
            {
                Destroy(this.gameObject.transform.GetChild(i).gameObject);
            }
        }
    }

    public void SpawnObjectInPlace(Vector3 spawnPos, float spawnRad, GameObject[] prefArr)
    {
        Vector3 spawnPosPref = Random.insideUnitCircle * spawnRad;
        spawnPos = spawnPosPref + spawnPos;
        GameObject pref = Instantiate(prefArr[(int)Random.Range(0, prefArr.Length - 1)]);
        pref.transform.position = spawnPos;
        pref.transform.SetParent(transform);
    }
}
