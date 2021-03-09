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
        returnToGameButton.onClick.AddListener(Resume);
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
                Pause();
                return;
            }
            if (pauseMenu)
            {
                Resume();
                return;
            }
        }
    }

    void Resume()
    {
        pauseMenu = false;
        inGame = true;
        pauseMenuUI.SetActive(false);
        inGameUI.SetActive(true);
        Time.timeScale = 1f;
    }

    void Pause()
    {
        pauseMenu = true;
        inGame = false;
        inGameUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
