using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public bool pauseMenu;
    public bool inGame;
    public bool inventoryMenu;
    public GameObject inGameUI;
    public GameObject pauseMenuUI;
    public GameObject inventoryUI;
    public Button returnToGameButton;


    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = false;
        inGameUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        inGame = true;
        returnToGameButton.onClick.AddListener(Resume);
        inventoryMenu = false;
        inventoryUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
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
        if (Input.GetKeyDown(KeyCode.E) && inGame)
        {
            if(!inventoryMenu)
            {
                inventoryMenu = true;
                inventoryUI.SetActive(true);
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                return;
            }
            if(inventoryMenu)
            {
                inventoryMenu = false;
                inventoryUI.SetActive(false);
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
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
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        pauseMenu = true;
        inGame = false;
        inGameUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }
}
