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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartScene(string FirstScene)
    {
        SceneManager.LoadScene(FirstScene);
    }
  
}
