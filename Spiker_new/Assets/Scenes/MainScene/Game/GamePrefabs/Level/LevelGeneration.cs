using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    /*
    Script generates game field in form of corridor in OZ direction
    */
    protected LevelObstacle[] levelObstacles;
    protected Host[] hostList;
    protected Collectable[] collectableList;
    protected ActiveEnemy[] activeEnemyList;
    protected PassiveEnemy[] passiveEnemyList;
    public float halfCorridorWidth = 15.0f;
    protected PlayerController playerController;
    protected BoarderController boarderController;
    public float generationStep = 0.5f;
    void Start()
    {
        activeEnemyList = Resources.LoadAll<ActiveEnemy>("EnemyPrefabs");
        passiveEnemyList = Resources.LoadAll<PassiveEnemy>("EnemyPrefabs");
        hostList = Resources.LoadAll<Host>("LevelPrefabs");
        levelObstacles = Resources.LoadAll<LevelObstacle>("LevelPrefabs");
        collectableList = Resources.LoadAll<Collectable>("CollectablePrefabs");

        playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
        boarderController = this.GetComponent<BoarderController>();
        boarderController.SetWalls(halfCorridorWidth);
    }
    
    void OnDrawGizmosSelected()
    {
        // Draw corridor width
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(halfCorridorWidth, 0, 0));
        Gizmos.DrawLine(transform.position, transform.position - new Vector3(halfCorridorWidth, 0, 0));
        // Draw corridor boarders
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + new Vector3(halfCorridorWidth, 0, 5), new Vector3 (1, 1, 25));
        Gizmos.DrawWireCube(transform.position - new Vector3(halfCorridorWidth, 0, -5), new Vector3 (1, 1, 25));

        Gizmos.color = Color.cyan;
        if (boarderController != null)
        {
            float posZ = boarderController.GetLowerBoarder();
            while (posZ < boarderController.GetUpperBoarder())
            {
                Gizmos.DrawLine(transform.position + new Vector3(-halfCorridorWidth, 0, posZ), transform.position + new Vector3(halfCorridorWidth, 0, posZ));
                posZ += generationStep;
                float posX = -halfCorridorWidth;
                while (posX < halfCorridorWidth)
                {
                    Gizmos.DrawLine(transform.position + new Vector3(posX, 0, boarderController.GetLowerBoarder()),
                     transform.position + new Vector3(posX, 0, boarderController.GetUpperBoarder()));
                    posX += generationStep;
                }
            }
        }
    }

    void Update()
    {
        boarderController.UpdateWalls(playerController.GetPlayerTransform().position);
    }
}
