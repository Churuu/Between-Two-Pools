//Joakim
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour {

    //Lägg till fler knappar om det behövs
    public Button StartButton;
    public Button ExitButton;
    public Button Restart;
    public Button Back;
    public Button Lvl1;
    public Button BackToMainMenu;
    public Button ExitGame;
    public Button ResumeGame;
    public Button SoundOption;
    public Button Options;
    public Button Credits;
    public Button ReturnToPauseMenu;
    public Canvas MainMenu;
    public Canvas Levels;
    public Canvas PauseMenu;
    public Canvas PauseSettings;

    public bool P_Pressed;
    private SceneManagerScript NextScene;

    

    void Start()
    {
        NextScene = GetComponent<SceneManagerScript>();
        P_Pressed = false;
        Time.timeScale = 1;
        StartButton.Select();
        //Main Menu
        StartButton.onClick.AddListener(TaskOnLevel);
        ExitButton.onClick.AddListener(TaskOnExitClick);
        Credits.onClick.AddListener(TaskOnCredits);

        Back.onClick.AddListener(TaskOnBack);
        //Pause menu
        Restart.onClick.AddListener(TaskOnRestart);
        BackToMainMenu.onClick.AddListener(TaskOnBackToMainMenu);
        ExitGame.onClick.AddListener(TaskOnExitClick);
        ResumeGame.onClick.AddListener(TaskOnResumeGame);
        SoundOption.onClick.AddListener(TaskOnSoundOption);
        ReturnToPauseMenu.onClick.AddListener(TaskOnReturnPause);

        
    }
    //Lägg till en knapp för varje level 
    private void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Escape) && P_Pressed == true)
        {
            PauseMenu.gameObject.SetActive(false);
            P_Pressed = false;
            FindObjectOfType<AudioManager>().PauseBool(false);
            Time.timeScale = 1;
        }
        //Aktivera Pause menyn och pausar spelet
        else if (Input.GetKeyDown(KeyCode.Escape) && P_Pressed == false && MainMenu.gameObject.activeInHierarchy == false && Levels.gameObject.activeInHierarchy == false && PauseSettings.gameObject.activeInHierarchy == false)
        {
            PauseMenu.gameObject.SetActive(true);
            ResumeGame.Select();
            FindObjectOfType<AudioManager>().PauseBool(true);
            P_Pressed = true;
            Time.timeScale = 0;
        }

    }
    void TaskOnReturnPause()
    {
        PauseMenu.gameObject.SetActive(true);
        PauseSettings.gameObject.SetActive(false);
        ResumeGame.Select();

    }

    void TaskOnCredits()
    {
        //creditscen
    }

    void TaskOnSoundOption()
    {
        PauseMenu.gameObject.SetActive(false);
        PauseSettings.gameObject.SetActive(true);
        
    }

    void TaskOnResumeGame()
    {
        PauseMenu.gameObject.SetActive(false);
        FindObjectOfType<AudioManager>().PauseBool(false);
        P_Pressed = false;
        Time.timeScale = 1;
    }

    void TaskOnLevel()
    {   
        Levels.gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(false);
        Back.Select();
        Debug.Log("Select lvl");
    }
    void TaskOnExitClick()
    {
        Debug.Log("You have clicked the Exit button!");
        //Stänger av spelet
        Application.Quit();
    }

    void TaskOnBack()
    {
        Levels.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(true);
        StartButton.Select();
    }
    // Laddar level efter vilken knapp som tryckts
    public void LoadLevel(string LevelSceneName)
    {
        NextScene.LoadNextScene(LevelSceneName);
    }

    void TaskOnRestart()
    {
        NextScene.ReloadCurrentScene();
    }

    void TaskOnBackToMainMenu()
    {
     
        P_Pressed = false;
        NextScene.LoadNextScene("MainMenu");
        StartButton.Select();
    }
}
