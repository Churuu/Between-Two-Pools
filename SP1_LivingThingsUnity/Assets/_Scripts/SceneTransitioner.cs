//Skapad av Robin Nechovski 07-02-2019

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{

    Animator anim;
    Animation fadeAnim;
    string sceneToLoad;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void LoadScene(string name)
    {
        sceneToLoad = name;
        anim.SetTrigger("Fade");
        Invoke("SwitchScene", fadeAnim.clip.length);
    }


    void SwitchScene()
    {
        SceneManager.LoadScene(sceneToLoad);
        anim.ResetTrigger("Fade");
    }
}
