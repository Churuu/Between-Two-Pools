//Skapad av Robin Nechovski 07-02-2019

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SceneTransitioner : MonoBehaviour
{
    public RawImage fadeImage;
    public float fadeSpeed;
    public bool playCutscene;

    string sceneToLoad;
    static SceneTransitioner scene;
    float target = 0;
    VideoStreamer videoStreamer;

    float alpha
    {
        get
        {
            return fadeImage.color.a;
        }
        set
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, value);
        }

    }

    void Start()
    {
        DontDestroyOnLoad(this);

        if (scene == null)
            scene = this;
        else
            Destroy(gameObject);

        videoStreamer = FindObjectOfType<VideoStreamer>();

    }

    void Update()
    {
        Fade(target);
    }

    public void LoadScene(string name)
    {
        sceneToLoad = name;
        Fade(1f);
        if (playCutscene)
            InvokeRepeating("PlayEndCutscene", 0, Time.deltaTime);
        else
            InvokeRepeating("SwitchScene", 0, Time.deltaTime);
    }

    void PlayEndCutscene()
    {
        videoStreamer.PrepareVideo();
        if (alpha >= .99f && videoStreamer.isVideoPrepared())
        {
            videoStreamer.PlayVideo();
            Fade(0f);
            InvokeRepeating("SwitchScene", videoStreamer.GetVideoLength(), Time.deltaTime);
        }
    }

    void SwitchScene()
    {
        Fade(1f);
        if (alpha >= .99f)
        {
            videoStreamer.image.gameObject.SetActive(false);
            SceneManager.LoadScene(sceneToLoad);
            Fade(0f);
            CancelInvoke();
        }
    }

    public void Fade(float target)
    {
        this.target = target;
        alpha = Mathf.Lerp(alpha, target, fadeSpeed * Time.deltaTime);
    }
}

