using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScriptUpgraded : MonoBehaviour
{
    [System.Serializable]
    public struct prefabData
    {
        public GameObject prefab;
        public float spawnChance;
        public float spawnRad;
    }

    public prefabData[] prefabsBig;
    public prefabData[] prefabsSmall;
    public float difficulty;
    public float genOffcetUp;
    public float genDelOffcet;
    public ScoreScript score;
    float spawnRate = 1.0f;
    public int prefDif = 3;

    /*
    In posArray is data of spawnPositions for small and big blocks
    it is a two dismentional array with 5 Vector3 coordinates in a row,
    wich represent 4 small coordinates and 1 big
    */

    GameObject Player;
    Vector3 spawnPosition;

    void Start()
    {
        Player = GameObject.Find("Player Model");
        spawnPosition = transform.position;
    }

    void Update()
    {
        if (Player.transform.position.y + genOffcetUp > spawnPosition.y)
        {
            GenerateLevel(spawnPosition);
            spawnPosition.y += 10.0f;
        }

        ClearRedundantParts();
    }

    public void GenerateLevel(Vector3 spawnPositionG)
    {
        int spawnBlockType = (int)Random.Range(1.0f, 4.0f);

        switch (spawnBlockType)
        {
            case 1:
                SpawnObjectInPlace(spawnPositionG + new Vector3(2.5f + 5, 2.5f, 0.0f), prefabsSmall, prefDif);
                SpawnObjectInPlace(spawnPositionG + new Vector3(-2.5f + 5, 2.5f, 0.0f), prefabsSmall, prefDif);
                SpawnObjectInPlace(spawnPositionG + new Vector3(2.5f + 5, -2.5f, 0.0f), prefabsSmall, prefDif);
                SpawnObjectInPlace(spawnPositionG + new Vector3(-2.5f + 5, -2.5f, 0.0f), prefabsSmall, prefDif);

                SpawnObjectInPlace(spawnPositionG + new Vector3(2.5f - 5, 2.5f, 0.0f), prefabsSmall, prefDif);
                SpawnObjectInPlace(spawnPositionG + new Vector3(-2.5f - 5, 2.5f, 0.0f), prefabsSmall, prefDif);
                SpawnObjectInPlace(spawnPositionG + new Vector3(2.5f - 5, -2.5f, 0.0f), prefabsSmall, prefDif);
                SpawnObjectInPlace(spawnPositionG + new Vector3(-2.5f - 5, -2.5f, 0.0f), prefabsSmall, prefDif);
                break;
            case 2:
                SpawnObjectInPlace(spawnPositionG + new Vector3(-5.0f, 0.0f, 0.0f), prefabsBig, 0);
                SpawnObjectInPlace(spawnPositionG + new Vector3(2.5f + 5, 2.5f, 0.0f), prefabsSmall, prefDif);
                SpawnObjectInPlace(spawnPositionG + new Vector3(-2.5f + 5, 2.5f, 0.0f), prefabsSmall, prefDif);
                SpawnObjectInPlace(spawnPositionG + new Vector3(2.5f + 5, -2.5f, 0.0f), prefabsSmall, prefDif);
                SpawnObjectInPlace(spawnPositionG + new Vector3(-2.5f + 5, -2.5f, 0.0f), prefabsSmall, prefDif);
                break;
            case 3:
                SpawnObjectInPlace(spawnPositionG, prefabsBig, 0);
                SpawnObjectInPlace(spawnPositionG + new Vector3(2.5f + 5, -2.5f, 0.0f),  prefabsSmall, prefDif);
                SpawnObjectInPlace(spawnPositionG + new Vector3(2.5f + 5, 2.5f, 0.0f),  prefabsSmall, prefDif);
                SpawnObjectInPlace(spawnPositionG + new Vector3(-2.5f - 5, -2.5f, 0.0f),  prefabsSmall, prefDif);
                SpawnObjectInPlace(spawnPositionG + new Vector3(-2.5f - 5, 2.5f, 0.0f),  prefabsSmall, prefDif);
                break;
            case 4:
                SpawnObjectInPlace(spawnPositionG + new Vector3(5.0f, 0.0f, 0.0f), prefabsBig, 0);
                SpawnObjectInPlace(spawnPositionG + new Vector3(2.5f - 5, 2.5f, 0.0f), prefabsSmall, prefDif);
                SpawnObjectInPlace(spawnPositionG + new Vector3(-2.5f - 5, 2.5f, 0.0f), prefabsSmall, prefDif);
                SpawnObjectInPlace(spawnPositionG + new Vector3(2.5f - 5, -2.5f, 0.0f), prefabsSmall, prefDif);
                SpawnObjectInPlace(spawnPositionG + new Vector3(-2.5f - 5, -2.5f, 0.0f), prefabsSmall, prefDif);
                break;
        }

        if (score.yMax % 200 == 0 && score.yMax <= 1000 && score.yMax > 0)
        {
            spawnRate -= 0.05f;
            if (prefDif > 0)
            {
                prefDif -= 1;
            }
            difficulty += 1;
        }
    }

    public void SpawnObjectInPlace(Vector3 spawnPos, prefabData[] prefs, int prefDifF)
    {
        int num = (int)Random.Range(0, prefs.Length - prefDifF);
        if (Random.Range(0, prefs[num].spawnChance) < spawnRate)
        {
            GameObject pref = Instantiate(prefs[num].prefab);
            Vector3 spawnPosPref = Random.insideUnitCircle * prefs[num].spawnRad;
            spawnPos = spawnPosPref + spawnPos;
            pref.transform.position = spawnPos;
            pref.transform.SetParent(transform);
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

    public void ClearAll()
    {
        spawnPosition = transform.position;
        score.yMax = 0;
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            Destroy(this.gameObject.transform.GetChild(i).gameObject);
        }
    }
}
