using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public bool pauseMenu;
    public bool inGame;
    public GameObject inGameUI;
    public GameObject pauseMenuUI;
    public Button returnToGameButton;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = false;
        inGameUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        inGame = true;
        returnToGameButton.onClick.AddListener(ReturnToGame);
    }

    // Update is called once per frame
    void Update()
    {
        MenuSwap();
    }


    void MenuSwap()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inGame)
            {
                pauseMenu = true;
                inGame = false;
                inGameUI.SetActive(false);
                pauseMenuUI.SetActive(true);
                return;
            }
            if (pauseMenu)
            {
                ReturnToGame();
                return;
            }
        }
    }

    void ReturnToGame()
    {
        pauseMenu = false;
        inGame = true;
        pauseMenuUI.SetActive(false);
        inGameUI.SetActive(true);
    }
}
