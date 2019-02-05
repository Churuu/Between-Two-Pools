using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Level_Button : MonoBehaviour {
    //Lägg till fler knappar om det behövs
    //Make sure to attach these Buttons in the Inspector
    public Button ExitButton;
    public Button Level;
    public Button Restart;
    public Button Back;
    public Button Lvl1;
    public Canvas Buttons;
    public Canvas Levels;
    private bool P_Pressed; 

    void Start()
    {
        P_Pressed = true;
        Time.timeScale = 1;
        Level.OnSelect(null);
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        ExitButton.onClick.AddListener(TaskOnExitClick);
        Level.onClick.AddListener(TaskOnLevel);
        Restart.onClick.AddListener(TaskOnRestart);
        Back.onClick.AddListener(TaskOnBack);
        Lvl1.onClick.AddListener(Lvl1Selected);
    }
    //Lägg till en knapp för varje level 
    private void Update()
    {   // visar menyn och Pausar också spelet och omvänt
        if (Input.GetKeyDown(KeyCode.P) && P_Pressed == false)
        {
            Buttons.gameObject.SetActive(false);
            P_Pressed = true;
            Time.timeScale = 1;
        }
        else if (Input.GetKeyDown(KeyCode.P) && P_Pressed == true)
        {
            Buttons.gameObject.SetActive(true);
            P_Pressed = false;
            Time.timeScale = 0;
        }
    }

    void TaskOnLevel()
    {   //Starta spelet på Level 1 eller annan level
        Levels.gameObject.SetActive(true);
        Buttons.gameObject.SetActive(false);
        Back.Select();
        Debug.Log("Select lvl");
    }

    void TaskOnExitClick()
    {
        //Stänger av spelet
        Application.Quit();
        Debug.Log("You have clicked the Exit button!");
    }

    void TaskOnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void TaskOnBack()
    {
        Level.Select();
        Levels.gameObject.SetActive(false);
        Buttons.gameObject.SetActive(true);
    }

    //En sån här per level
    void Lvl1Selected()
    {
        SceneManager.LoadScene(/*Namn på scenen med lvl1*/"Enemy");
    }
}
