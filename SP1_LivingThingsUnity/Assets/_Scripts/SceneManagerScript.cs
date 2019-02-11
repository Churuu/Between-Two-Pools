using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour {
    private GameObject Seel;
    private GameObject Frog;
    private GameObject Otter;

	public void LoadNextScene(string SceneName)
    {
        Seel = GameObject.Find("Seel");
        Frog = GameObject.Find("Frog");
        Otter = GameObject.Find("Otter");
        Scene CurrentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(SceneName);
        /*SceneManager.MoveGameObjectToScene(Seel, SceneManager.GetSceneByName(SceneName));
        SceneManager.MoveGameObjectToScene(Frog, SceneManager.GetSceneByName(SceneName));
        SceneManager.MoveGameObjectToScene(karaktär3, SceneManager.GetSceneByName(SceneName));*/ // för att skicka med karaktärerna
        SceneManager.UnloadSceneAsync(CurrentScene);
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartScene(string FirstScene)
    {
        SceneManager.LoadScene(FirstScene);
    }
  
}
