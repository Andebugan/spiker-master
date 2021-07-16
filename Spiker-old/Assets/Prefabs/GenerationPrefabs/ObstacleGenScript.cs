using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenScript : MonoBehaviour
{
    public int prefCol;
    public int sideCol;
    GameObject[] prefabs;

    public bool regenerate = false; 

    int num = 0;

    void Start()
    {
        prefabs = new GameObject[prefCol * sideCol];
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            prefabs[i] = this.gameObject.transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        if (regenerate == true)
        {
            SetAllOff();
            SetRandomMeshes();
            RotateBase();
            regenerate = false;
        }
    }

    public void SetRandomMeshes()
    {
        var buffPref = new GameObject[prefCol];
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            for (int j = 0; j < prefCol; j++)
            {
                buffPref[j] = prefabs[i + j];
            }
            
            num = (int)Random.Range(0, prefCol * 2);

            if (num < prefCol)
            {
                prefabs[num + i].SetActive(true);
            }

            i += prefCol - 1;
        }
    }

    public void RotateBase()
    {
        transform.Rotate(0, 0, Random.Range(0, 90), Space.World);
    }

    public void SetAllOff()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            prefabs[i].SetActive(false);
        }
    }
}