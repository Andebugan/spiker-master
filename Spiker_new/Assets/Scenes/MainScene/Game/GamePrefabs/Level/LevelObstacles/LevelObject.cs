using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    public float spawnRadius = 1.0f;
    public float difficultyLevel = 1.0f;
    public int spawnChance = 70;
    protected System.Random random = new System.Random();

    public bool CheckSpawnChance()
    {
        int spawnChanceValue = 0;
        spawnChanceValue = random.Next(0, 100);
        if (spawnChanceValue > 100 - spawnChance)
            return true;
        else
            return false;
    }
    
    public bool CheckHullChances(int hullChance)
    {
        int hullChanceValue = 0;
        hullChanceValue = random.Next(0, 100);
        if (hullChanceValue > hullChance && CheckSpawnChance())
            return true;
        return false;
    }
    public bool CheckDifficulty(float difficulty)
    {
        if (difficulty < difficultyLevel)
        {
            return false;
        }
        return true;
    }
}
