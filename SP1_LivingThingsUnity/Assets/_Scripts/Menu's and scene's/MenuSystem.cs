//Joakim
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour {

    //Lägg till fler knappar om det behövs
    public Button StartButton;
    public Button ExitButton;
    public Button Level;
    public Button Restart;
    public Button Back;
    public Button Lvl1;
    public Button BackToMainMenu;
    public Canvas MainMenu;
    public Canvas Levels;
    public Canvas PauseMenu;
    private bool P_Pressed;
    private SceneManagerScript NextScene;

    void Start()
    {
        NextScene = GetComponent<SceneManagerScript>();
        P_Pressed = false;
        Time.timeScale = 1;
        StartButton.Select();
        //Main Menu
        StartButton.onClick.AddListener(TaskOnStart);
        ExitButton.onClick.AddListener(TaskOnExitClick);
        Level.onClick.AddListener(TaskOnLevel);
        Back.onClick.AddListener(TaskOnBack);
        //Pause menu
        Restart.onClick.AddListener(TaskOnRestart);
        BackToMainMenu.onClick.AddListener(TaskOnBackToMainMenu);
    }
    //Lägg till en knapp för varje level 
    private void Update()
    {   // visar pause menyn och Pausar också spelet och omvänt
        if (Input.GetKeyDown(KeyCode.Escape) && P_Pressed == true)
        {
            PauseMenu.gameObject.SetActive(false);
            P_Pressed = false;
            Time.timeScale = 1;
        }
        //Aktivera Pause menyn och pausar spelet
        else if (Input.GetKeyDown(KeyCode.Escape) && P_Pressed == false && MainMenu.gameObject.activeInHierarchy == false && Levels.gameObject.activeInHierarchy == false)
        {
            PauseMenu.gameObject.SetActive(true);
            Restart.Select();
            P_Pressed = true;
            Time.timeScale = 0;
        }
    }
    void TaskOnStart()
    {
        NextScene.StartScene(/*Namn på scenen med lvl1*/" ");
        MainMenu.gameObject.SetActive(false);
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
        MainMenu.gameObject.SetActive(true);
        PauseMenu.gameObject.SetActive(false);
        P_Pressed = false;
        NextScene.LoadNextScene("MainMenu");
        StartButton.Select();
    }
}
