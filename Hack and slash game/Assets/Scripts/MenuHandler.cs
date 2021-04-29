using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public bool pauseMenu;
    public bool inGame;
    public bool inventoryMenu;
    public bool mainMenu;
    public bool optionsMenu;

    public GameObject inGameUI;
    public GameObject dialogUI;
    public GameObject pauseMenuUI;
    public GameObject inventoryUI;
    public GameObject inventoryIcon;
    public GameObject mainMenuUI;
    public GameObject optionsUI;

    public Button returnToGameButton;
    public Button quitButton;
    public Button playButton;
    public Button menuOptionsButton;
    public Button pauseOptionsButton;
    public Button saveButton;
    public Button exitToMenuButton;
    public Button backButton;

    public Camera inGameCamera;
    public Camera mainMenuCamera;

    public int lastState; // 0 = MainMenu ; 1 = PauseMenu

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            mainMenu = true;
            mainMenuUI.SetActive(true);
            inGame = false;
            inGameUI.SetActive(false);
            mainMenuCamera.enabled = true;
            inGameCamera.enabled = false;
        }
        else
        {
            mainMenu = false;
            mainMenuUI.SetActive(false);
            inGame = true;
            inGameUI.SetActive(true);
            mainMenuCamera.enabled = false;
            inGameCamera.enabled = true;
        }
        pauseMenu = false;
        inventoryMenu = false;
        optionsMenu = false;

        pauseMenuUI.SetActive(false);
        inventoryUI.SetActive(false);
        inventoryIcon.SetActive(false);
        optionsUI.SetActive(false);

        returnToGameButton.onClick.AddListener(Resume);
        quitButton.onClick.AddListener(Quit);
        playButton.onClick.AddListener(StartGame);
        menuOptionsButton.onClick.AddListener(Options);
        pauseOptionsButton.onClick.AddListener(Options);
        saveButton.onClick.AddListener(Save);
        exitToMenuButton.onClick.AddListener(Exit);
        backButton.onClick.AddListener(Back);

        lastState = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
            else if (pauseMenu)
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
                Cursor.lockState = CursorLockMode.None;
                return;
            }
            else if(inventoryMenu)
            {
                inventoryMenu = false;
                inventoryUI.SetActive(false);
                inventoryIcon.SetActive(true);
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                return;
            }

        }
        if(dialogUI.activeSelf == true)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(dialogUI.activeSelf == false)
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }
           
    }

    void Resume()
    {
        pauseMenu = false;
        inGame = true;
        pauseMenuUI.SetActive(false);
        inGameUI.SetActive(true);
        inventoryUI.SetActive(false);
        inventoryIcon.SetActive(true);
        dialogUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenu = true;
        inGame = false;
        inGameUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
        dialogUI.SetActive(false);
        inventoryIcon.SetActive(true);

        mainMenuCamera.enabled = false;
        inGameCamera.enabled = true;

        lastState = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Back()
    {
        optionsMenu = false;
        optionsUI.SetActive(false);
        if(lastState == 0) // Main Menu
        {
            mainMenu = true;
            mainMenuUI.SetActive(true);
        }
        else if (lastState == 1) // Pause Menu
        {
            pauseMenu = true;
            pauseMenuUI.SetActive(true);
        }
    }

    void Options()
    {
        if(lastState == 0) // Main Menu
        {
            mainMenu = false;
            mainMenuUI.SetActive(false);
        }
        else if (lastState == 1) // Pause Menu
        {
            pauseMenu = false;
            pauseMenuUI.SetActive(false);
        }
        optionsMenu = true;
        optionsUI.SetActive(true);
    }

    void Save()
    {

    }

    void Exit()
    {
        inGame = false;
        pauseMenu = false;
        mainMenu = true;

        inGameUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);

        inGameCamera.enabled = false;
        mainMenuCamera.enabled = true;

        lastState = 0;

        Time.timeScale = 1f;
    }
}
