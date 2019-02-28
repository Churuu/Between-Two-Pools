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

    private bool GameOverActivation = false;

	// Use this for initialization
	void Start ()
    {
        gameOverCanvas.gameObject.SetActive(false);
        EventManager.instance.OnGameOver += OnGameOverEvent;
        
    }

    void Update()
    {
        if (GameOverActivation)
            GameOverFunction();
    }

    private void OnGameOverEvent()
    {
        timerFloat = timerDuration;
        gameOverCanvas.gameObject.SetActive(true);
        GameOverActivation = true;

        EventManager.instance.OnGameOver -= OnGameOverEvent;
    }

    private void GameOverFunction()
    {

        timerText.text = timerFloat + "Game Over";
        if (timerFloat >= 0)
        {
            timerFloat -= Time.deltaTime;
        }
        else if (timerFloat < 0)
        {
            FindObjectOfType<SceneManagerScript>().ReloadCurrentScene();
        }
    }
}
