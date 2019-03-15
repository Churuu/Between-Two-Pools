using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OnGameOver : MonoBehaviour
{
    [SerializeField]
    private Canvas gameOverCanvas;

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private float timerDuration;
    private float timerFloat;

    [SerializeField]
    private string restartButton = "RestartButton";

    private bool GameOverActivation = false;

	// Use this for initialization
	void Start ()
    {
        gameOverCanvas.gameObject.SetActive(false);
        
        
    }

    void Update()
    {
        if (EventManager.instance.OnGameOver == null)
        {
            EventManager.instance.OnGameOver += OnGameOverEvent;
        }

        if (GameOverActivation)
            GameOverFunction();
    }

    private void OnGameOverEvent()
    {
        timerFloat = timerDuration;
        gameOverCanvas.gameObject.SetActive(true);
        GameOverActivation = true;
        FindObjectOfType<AudioManager>().OnLoseStinger();

        EventManager.instance.OnGameOver -= OnGameOverEvent;
    }

    private void GameOverFunction()
    {
        
        timerText.text = Mathf.RoundToInt(timerFloat) + " seconds";
        if (timerFloat >= 0)
        {
            timerFloat -= Time.deltaTime;
        }
        else if (timerFloat < 0)
        {
            gameOverCanvas.gameObject.SetActive(false);
            GameOverActivation = false;
            FindObjectOfType<SceneManagerScript>().ReloadCurrentScene();
        }
        if (Input.GetButtonDown(restartButton)) 
        {
            gameOverCanvas.gameObject.SetActive(false);
            GameOverActivation = false;
            FindObjectOfType<SceneManagerScript>().ReloadCurrentScene();
        }
    }
}
