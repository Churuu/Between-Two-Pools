//Joakim
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour {

	public void LoadNextScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        
    }

    public void ReloadCurrentScene()
    {
        FindObjectOfType<StemsManager>().RestartStems();
        FindObjectOfType<AudioManager>().PauseBool(true);
        FindObjectOfType<AudioManager>().PauseBool(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartScene(string FirstScene)
    {
        SceneManager.LoadScene(FirstScene);
    }
  
}
