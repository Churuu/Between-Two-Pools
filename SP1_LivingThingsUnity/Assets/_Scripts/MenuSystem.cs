using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    void Start()
    {
        P_Pressed = false;
        Time.timeScale = 1;
        StartButton.OnSelect(null);
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
        else if (Input.GetKeyDown(KeyCode.Escape) && P_Pressed == false && MainMenu.gameObject.active == false && Levels.gameObject.active == false)
        {
            PauseMenu.gameObject.SetActive(true);
            Restart.Select();
            P_Pressed = true;
            Time.timeScale = 0;
        }
    }
    void TaskOnStart()
    {
        SceneManager.LoadScene(/*Namn på scenen med lvl1*/" ");
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
        SceneManager.LoadScene(LevelSceneName);
    }

    void TaskOnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void TaskOnBackToMainMenu()
    {
        MainMenu.gameObject.SetActive(true);
        PauseMenu.gameObject.SetActive(false);
        P_Pressed = false;
        StartButton.Select();
    }
}
