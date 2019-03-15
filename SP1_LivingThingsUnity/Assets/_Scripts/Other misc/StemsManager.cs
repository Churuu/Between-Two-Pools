using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StemsManager : MonoBehaviour
{
    private AudioSource mainStemAudioSource;
    [SerializeField] private AudioSource sealStemAudioSource;

    [SerializeField] private AudioSource otterStemAudioSource;

    [SerializeField] private AudioSource frogStemAudioSource;

    [SerializeField]
    private List<AudioClip> music11 = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> music12 = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> music13 = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> music21 = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> music22 = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> music31 = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> music32 = new List<AudioClip>();

    private List<AudioClip> thisLevelStems = new List<AudioClip>();

    private List<AudioSource> AudioSources = new List<AudioSource>();


    private float mainStemVolume;
    private float sealStemVolume;
    private float otterStemVolume;
    private float frogStemVolume;

    private float savedMapIndex = 4;

    public enum StemFocus
    {
        Otter, Seal, Frog
    }
    [SerializeField]
    StemFocus stemFocus;

    MenuSystem menuSystem;
    // Use this for initialization
    void Start ()
    {
        sealStemVolume = sealStemAudioSource.volume;
        otterStemVolume = otterStemAudioSource.volume;
        frogStemVolume = frogStemAudioSource.volume;
        menuSystem = FindObjectOfType<MenuSystem>();
        mainStemAudioSource = GetComponent<AudioSource>();

        ToOtter();

        savedMapIndex = SceneManager.GetActiveScene().buildIndex;

        AudioSources.Add(mainStemAudioSource);
        AudioSources.Add(sealStemAudioSource);
        AudioSources.Add(otterStemAudioSource);
        AudioSources.Add(frogStemAudioSource);
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (savedMapIndex != SceneManager.GetActiveScene().buildIndex)
        {
            UpdateStems();
            savedMapIndex = SceneManager.GetActiveScene().buildIndex;
        }
        for (int i = 0; i < AudioSources.Count; i++)
        {
            print(AudioSources[i].name + " " + AudioSources[i].isPlaying);
        }
    }

    public void UpdateStems()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 4:
                thisLevelStems = music11;
                break;
            case 5:
                thisLevelStems = music12;
                break;
            case 6:
                thisLevelStems = music13;
                break;
            case 7:
                thisLevelStems = music21;
                break;
            case 8:
                thisLevelStems = music22;
                break;
            case 9:
                thisLevelStems = music31;
                break;
            case 10:
                thisLevelStems = music32;
                break;
        }
        mainStemAudioSource.clip = thisLevelStems[0];
        sealStemAudioSource.clip = thisLevelStems[1];
        otterStemAudioSource.clip = thisLevelStems[2];
        frogStemAudioSource.clip = thisLevelStems[3];

        for (int i = 0; i < AudioSources.Count; i++)
        {
            AudioSources[i].clip = thisLevelStems[i];
        }
        RestartStems();
        ToOtter();
    }

    public void RestartStems()
    {
        for (int i = 0; i < AudioSources.Count; i++)
        {
            OnMenuUnPause();
            AudioSources[i].Play();
        }
    }

    public void OnMenuPause()
    {
        //mainStemAudioSource.Pause();
        //sealStemAudioSource.Pause();
        //otterStemAudioSource.Pause();
        //frogStemAudioSource.Pause();
        for (int i = 0; i < AudioSources.Count; i++)
        {
            AudioSources[i].Pause();
        }
    }

    public void OnMenuUnPause()
    {
        //mainStemAudioSource.UnPause();
        //sealStemAudioSource.UnPause();
        //otterStemAudioSource.UnPause();
        //frogStemAudioSource.UnPause();
        for (int i = 0; i < AudioSources.Count; i++)
        {
            AudioSources[i].UnPause();
        }
        //switch (stemFocus)
        //{
        //    case StemFocus.Otter:
        //        ToOtter();
        //        break;
        //    case StemFocus.Seal:
        //        ToSeal();
        //        break;
        //    case StemFocus.Frog:
        //        ToFrog();
        //        break;
        //}
    }

    public void ToSeal()
    {
        sealStemAudioSource.volume = sealStemVolume;
        otterStemAudioSource.volume = 0;
        frogStemAudioSource.volume = 0;
        stemFocus = StemFocus.Seal;
    }
    public void ToOtter()
    {
        sealStemAudioSource.volume = 0;
        otterStemAudioSource.volume = otterStemVolume;
        frogStemAudioSource.volume = 0;
        stemFocus = StemFocus.Otter;
    }
    public void ToFrog()
    {
        sealStemAudioSource.volume = 0;
        otterStemAudioSource.volume = 0;
        frogStemAudioSource.volume = frogStemVolume;
        stemFocus = StemFocus.Frog;
    }
}
