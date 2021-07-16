using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    /*
    public GameObject[] images;
    public float moveTime;
    public Vector2[] movePos;

    PlayerSystem playerSystem;
    PlayerController playerController;

    public GameObject[] DeathMenu;
    public float DeathMenuScale;
    public bool DeathMenuActive = false;

    public GameObject[] MainMenu;
    public float MainMenuScale;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("SpawnPoint");
        playerSystem = player.GetComponent<PlayerSystem>();
        GameObject playerModel = GameObject.Find("Player Model");
        playerController = playerModel.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerSystem.Dead == true && DeathMenuActive == false)
        {
            GameEnd();
            DeathMenuActive = true;
        }
    }

    public void GameEnd()
    {
        CloseGameField();
        DeathMenuOpen();
        playerController.active = false;
    }

    public void MenuRestart()
    {
        DeathMenuClose();
        OpenGameField();
        DeathMenuActive = false;
    }

    public void StartGame()
    {
        OpenGameField();
        playerController.active = true;
    }

    public void DeathMenuOpen()
    {
        foreach (var DmenuEl in DeathMenu)
        {
            LeanTween.scale(DmenuEl, new Vector3(1.0f, 1.0f, 1.0f), DeathMenuScale);
        }
    }

    public void DeathMenuClose()
    {
        foreach (var item in DeathMenu)
        {
            LeanTween.scale(item, new Vector3(0.0f, 0.0f, 0.0f), DeathMenuScale).setOnComplete(PlayerOn);
        }
        DeathMenuActive = false;
    }

    void PlayerOff()
    {
        playerController.active = false;
    }
    
    void PlayerOn()
    {
        playerController.active = true;
    }

    public void OpenGameField()
    {
        LeanTween.move(images[0].GetComponent<RectTransform>(), movePos[0], moveTime);
        LeanTween.move(images[1].GetComponent<RectTransform>(), movePos[1], moveTime);
        LeanTween.move(images[2].GetComponent<RectTransform>(), movePos[4], moveTime);
        LeanTween.move(images[3].GetComponent<RectTransform>(), movePos[5], moveTime);
    }

    public void CloseGameField()
    {
        LeanTween.move(images[0].GetComponent<RectTransform>(), movePos[2], moveTime);
        LeanTween.move(images[1].GetComponent<RectTransform>(), movePos[3], moveTime);
        LeanTween.move(images[2].GetComponent<RectTransform>(), movePos[6], moveTime);
        LeanTween.move(images[3].GetComponent<RectTransform>(), movePos[7], moveTime);
    }
    */
}
