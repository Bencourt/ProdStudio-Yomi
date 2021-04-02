using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public bool pauseMenu;
    public bool inGame;
    public bool inventoryMenu;
    public bool mainMenu;
    public bool optionsMenu;

    public GameObject inGameUI;
    public GameObject pauseMenuUI;
    public GameObject inventoryUI;
    public GameObject inventoryIcon;
    public GameObject mainMenuUI;

    public Button returnToGameButton;
    public Button quitButton;
    public Button playButton;
    public Button menuOptionsButton;
    public Button pauseOptionsButton;
    public Button saveButton;
    public Button exitToMenuButton;

    public Camera inGameCamera;
    public Camera mainMenuCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu = true;
        pauseMenu = false;
        inGame = false;
        inventoryMenu = false;
        optionsMenu = false;

        mainMenuUI.SetActive(true);
        inGameUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        inventoryUI.SetActive(false);
        inventoryIcon.SetActive(false);

        returnToGameButton.onClick.AddListener(Resume);
        quitButton.onClick.AddListener(Quit);
        playButton.onClick.AddListener(StartGame);
        menuOptionsButton.onClick.AddListener(Options);
        pauseOptionsButton.onClick.AddListener(Options);
        saveButton.onClick.AddListener(Save);
        exitToMenuButton.onClick.AddListener(Exit);

        mainMenuCamera.enabled = true;
        inGameCamera.enabled = false;

        //Cursor.lockState = CursorLockMode.Locked;
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
                inventoryIcon.SetActive(false);
                Time.timeScale = 0f;
                //Cursor.lockState = CursorLockMode.None;
                return;
            }
            if(inventoryMenu)
            {
                inventoryMenu = false;
                inventoryUI.SetActive(false);
                inventoryIcon.SetActive(true);
                Time.timeScale = 1f;
                //Cursor.lockState = CursorLockMode.Locked;
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
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        pauseMenu = true;
        inGame = false;
        inGameUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        //Cursor.lockState = CursorLockMode.None;
    }

    void Quit()
    {
        Application.Quit();
    }

    void StartGame()
    {
        mainMenu = false;
        inGame = true;

        mainMenuUI.SetActive(false);
        inGameUI.SetActive(true);

        mainMenuCamera.enabled = false;
        inGameCamera.enabled = true;
    }

    void Options()
    {

    }

    void Save()
    {

    }

    void Exit()
    {
        inGame = false;
        mainMenu = true;

        inGameUI.SetActive(false);
        mainMenuUI.SetActive(true);

        inGameCamera.enabled = false;
        mainMenuCamera.enabled = true;
    }
}
