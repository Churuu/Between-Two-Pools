//Skapad av Robin Nechovski 07-02-2019

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    public AnimationClip fadeAnim;

    Animator anim;
    string sceneToLoad;

    void Start()
    {
        if(GameObject.Find(gameObject.name) == null)
            DontDestroyOnLoad(gameObject);

        


        anim = GetComponent<Animator>();
    }

    public void LoadScene(string name)
    {
        sceneToLoad = name;
        anim.SetTrigger("Fade");
        Invoke("SwitchScene", fadeAnim.length + 1);
    }


    void SwitchScene()
    {
        SceneManager.LoadScene(sceneToLoad);
        anim.SetTrigger("FadeOut");
        Invoke("Reset", 1.5f);

    }

    void Reset()
    {
        anim.ResetTrigger("Fade");
        anim.ResetTrigger("FadeOut");
    }
}
