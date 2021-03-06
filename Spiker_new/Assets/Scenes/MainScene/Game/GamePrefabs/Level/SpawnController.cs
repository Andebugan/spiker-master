using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    protected LevelObject[] levelObjectPrefabs;
    protected List<Type> levelObjectTypes = new List<Type>();
    public float generationStep = 1.0f;
    public float difficultyStep = 100.0f;
    public float generationSpawnOffcet = 15.0f;
    public float generationDestroyOffcet = 10.0f;
    protected float difficulty = 1.0f;
    protected float maxDifficulty = 1.0f;
    protected System.Random random = new System.Random();
    protected List<List<LevelObject>> spawningObjectsList = new List<List<LevelObject>>();
    protected List<List<List<LevelObject>>> spawningObjectsListByDifficutly = new List<List<List<LevelObject>>>();
    protected List<GameObject> generatedLevelObjects = new List<GameObject>();
    public bool generating = false;
    protected Vector3 currGenPosition;
    // Value to describe chance of hull spawning at step
    public int hullChance = 20;
    public LevelObject hull;
    protected float halfCorridorWidth;

    protected LevelGeneration levelGeneration;

    void Start()
    {
        levelObjectPrefabs = Resources.LoadAll<LevelObject>("LevelObjects");
        halfCorridorWidth = this.GetComponent<LevelGeneration>().halfCorridorWidth;
        levelGeneration = GetComponent<LevelGeneration>();
        FormDifficultyList();
    }

    void FormDifficultyList()
    {
        GetMaxDifficutly();
        for (int i = 1; i <= Mathf.RoundToInt(maxDifficulty); i++)
        {
            spawningObjectsList = new List<List<LevelObject>>();
            FormLevelObjectTypesList(Mathf.RoundToInt(i));
            FormSpawningObjectsList(Mathf.RoundToInt(i));
            spawningObjectsListByDifficutly.Add(spawningObjectsList);
        }
        spawningObjectsList = spawningObjectsListByDifficutly[Mathf.RoundToInt(difficulty - 1)];
    }

    void GetMaxDifficutly()
    {
        foreach (LevelObject levelObject in levelObjectPrefabs)
        {
            if (levelObject.difficultyLevel > maxDifficulty)
            {
                maxDifficulty = levelObject.difficultyLevel;
            }
        }
    }

    public void SetStartGenPosition(float halfCorridorWidth)
    {
        currGenPosition = new Vector3 (-halfCorridorWidth + 1.0f, 0, generationSpawnOffcet);
    }

    public void ClearGeneratedObjects()
    {
        for (int i = 0; i < generatedLevelObjects.Count; i++)
        {
            Destroy(generatedLevelObjects[i]);
        }
        generatedLevelObjects.Clear();
    }

    void FormLevelObjectTypesList(int difficulty)
    {
        foreach (LevelObject levelObject in levelObjectPrefabs)
        {
            Type type;
            type = levelObject.GetType();
            if (!levelObjectTypes.Contains(type) && levelObject.difficultyLevel <= difficulty)
            {
                levelObjectTypes.Add(type);
            }
        }
    }

    void FormSpawningObjectsList(int difficulty)
    {
        for (int i = 0; i < levelObjectTypes.Count; i++)
        {
            List<LevelObject> objectList = new List<LevelObject>();
            foreach (LevelObject levelObject in levelObjectPrefabs)
            {
                Type type = levelObject.GetType();
                if (type == levelObjectTypes[i] && levelObject.difficultyLevel <= difficulty)
                {
                    objectList.Add(levelObject);
                }
            }
            if (objectList.Count != 0)
                spawningObjectsList.Add(objectList);
        }
    }

    protected bool CheckSpawnConditions(Vector3 pos, LevelObject levelObject)
    {
        for (int i = 0; i < generatedLevelObjects.Count; i++)
        {
            float distance = Vector3.Distance(currGenPosition, generatedLevelObjects[i].transform.position);
            if (levelObject.spawnRadius + generatedLevelObjects[i].GetComponent<LevelObject>().spawnRadius > distance)
                return false;
        }
        if (Mathf.Abs(currGenPosition.x) + levelObject.spawnRadius > halfCorridorWidth)
            return false;
        return true;
    }

    protected bool SpawnObject(Vector3 pos, LevelObject levelObject)
    {
        if (CheckSpawnConditions(pos, levelObject))
        {
            if (levelObject.CheckHullChances(hullChance))
            {
                generatedLevelObjects.Add(Instantiate(levelObject.gameObject, pos, Quaternion.identity));
            }
            else
            {
                GameObject hullObject = Instantiate(hull.gameObject, pos, Quaternion.identity);
                hullObject.GetComponent<LevelObject>().spawnRadius = 0.0f;
                generatedLevelObjects.Add(hullObject);
            }
            return true;
        }
        return false;
    }

    public void Generate(float halfCorridorWidth, float currPlayerPosZ)
    {
        int typeIndex = random.Next(0, spawningObjectsList.Count);
        int itemIndex = random.Next(0, spawningObjectsList[typeIndex].Count);
        bool generatedObject = false;

        while (generating && (currGenPosition.z < currPlayerPosZ + generationSpawnOffcet) && !generatedObject)
        {
            generatedObject = SpawnObject(currGenPosition, spawningObjectsList[typeIndex][itemIndex]);
            currGenPosition += new Vector3(generationStep, 0, 0);
            if (currGenPosition.x > halfCorridorWidth)
                currGenPosition += new Vector3(-2 * halfCorridorWidth + 1.0f, 0, generationStep);
        }
    }

    public void DestroyExpiredObjects(float currPlayerPosZ)
    {
        if (generatedLevelObjects.Count != 0)
        {
            for (int i = 0; i < generatedLevelObjects.Count; i++)
            {
                if (generatedLevelObjects[i] != null)
                {
                    if (currPlayerPosZ - generatedLevelObjects[i].transform.position.z > generationDestroyOffcet)
                    {
                        GameObject tempObject = generatedLevelObjects[i];
                        generatedLevelObjects.Remove(generatedLevelObjects[i]);
                        Destroy(tempObject);
                    }
                }
                else
                {
                    generatedLevelObjects.Remove(generatedLevelObjects[i]);
                }
            }
        }
    }

    public void UpdateDifficulty()
    {
        int new_difficulty = Mathf.CeilToInt(levelGeneration.GetPlayerController().GetPlayerTransform().position.z / difficultyStep);
        if (difficulty < new_difficulty)
        {
            difficulty = new_difficulty;
            Debug.Log(difficulty);
            spawningObjectsList = spawningObjectsListByDifficutly[Mathf.RoundToInt(difficulty - 1)];
        }
    }

    public float GetDifficulty()
    {
        return difficulty;
    }
}