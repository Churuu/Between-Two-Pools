using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StemsManager : MonoBehaviour
{
    [SerializeField]
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

    private List<AudioClip> thisLevelStems = new List<AudioClip>();

    private List<AudioSource> AudioSources = new List<AudioSource>();

    [SerializeField]
    private List<int> worldList = new List<int>();

    private float mainStemVolume;
    private float sealStemVolume;
    private float otterStemVolume;
    private float frogStemVolume;

    private float savedMapIndex = 0;

    [SerializeField]
    private int stemCount = 0;

    public enum StemFocus
    {
        Otter, Seal, Frog, Stairs
    }
    [SerializeField]
    StemFocus stemFocus;

    private bool stemStairBool = false;

    private bool menuPauseBool = false;

    MenuSystem menuSystem;
    // Use this for initialization
    void Start()
    {
        mainStemVolume = mainStemAudioSource.volume;
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

        thisLevelStems = music11;



        UpdateStems();
        print("stems updated");
    }

    // Update is called once per frame
    void Update()
    {
        print("Saved Index: " + savedMapIndex);
        print("Build Index: " + SceneManager.GetActiveScene().buildIndex);
        if (savedMapIndex != SceneManager.GetActiveScene().buildIndex)
        {
            //for (int i = 0; i < worldList.Count; i++)
            //{
            //    if (worldList[i])
            //    {

            //    }
            //}
            UpdateStems();
            savedMapIndex = SceneManager.GetActiveScene().buildIndex;
            print("SaveIndex Updated");
        }
        //for (int i = 0; i < AudioSources.Count; i++)
        //{
        //    print(AudioSources[i].name + " " + AudioSources[i].isPlaying);
        //}
        if (SceneManager.GetActiveScene().buildIndex < 3)
        {
            OnMenuPause();
        }
    }

    public void UpdateStems()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                //OnMenuPause();
                menuPauseBool = true;
                stemStairBool = false;
                break;
            case 1:
                //OnMenuPause();
                menuPauseBool = true;
                stemStairBool = false;
                break;
            case 2:
                FindObjectOfType<AudioManager>().PauseBool(true);
                stemStairBool = false;
                break;
            case 3:
                menuPauseBool = false;
                FindObjectOfType<AudioManager>().PauseBool(true);
                FindObjectOfType<AudioManager>().PauseBool(false);
                thisLevelStems = music11;
                stemStairBool = false;
                break;
            case 4:
                menuPauseBool = false;
                FindObjectOfType<AudioManager>().PauseBool(true);
                FindObjectOfType<AudioManager>().PauseBool(false);
                thisLevelStems = music12;
                stemStairBool = false;
                break;
            case 5:
                menuPauseBool = false;
                FindObjectOfType<AudioManager>().PauseBool(true);
                FindObjectOfType<AudioManager>().PauseBool(false);
                thisLevelStems = music13;
                stemStairBool = false;
                break;
            case 6:
                menuPauseBool = true;
                FindObjectOfType<AudioManager>().PauseBool(true);
                stemStairBool = false;
                break;
            case 7:
                menuPauseBool = true;
                FindObjectOfType<AudioManager>().PauseBool(true);
                stemStairBool = false;
                break;
            case 8:
                menuPauseBool = false;
                FindObjectOfType<AudioManager>().PauseBool(true);
                FindObjectOfType<AudioManager>().PauseBool(false);
                thisLevelStems = music21;
                stemCount = 1;
                UpdateStemStairs();
                stemStairBool = true;
                break;
            case 9:
                menuPauseBool = false;
                FindObjectOfType<AudioManager>().PauseBool(true);
                FindObjectOfType<AudioManager>().PauseBool(false);
                thisLevelStems = music22;
                stemCount = 1;
                UpdateStemStairs();
                stemStairBool = true;
                break;
            case 10:
                menuPauseBool = true;
                FindObjectOfType<AudioManager>().PauseBool(true);
                stemStairBool = false;
                break;
            case 11:
                menuPauseBool = false;
                FindObjectOfType<AudioManager>().PauseBool(true);
                FindObjectOfType<AudioManager>().PauseBool(false);
                thisLevelStems = music31;
                stemStairBool = false;
                break;
            case 12:
                menuPauseBool = true;
                FindObjectOfType<AudioManager>().PauseBool(true);
                stemStairBool = false;
                break;
            case 13:
                menuPauseBool = true;
                OnMenuPause();
                stemStairBool = false;
                break;
            case 14:
                menuPauseBool = true;
                stemStairBool = false;
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
        
        if (!menuPauseBool)
        {
            RestartStems();
        }

        ToOtter();
    }



    public void RestartStems()
    {
        for (int i = 0; i < AudioSources.Count; i++)
        {
            OnMenuUnPause();
            AudioSources[i].Play();
            print("RESTART");
        }
    }

    public void OnMenuPause()
    {
        for (int i = 0; i < AudioSources.Count; i++)
        {
            AudioSources[i].Pause();
        }
    }

    public void OnMenuUnPause()
    {
        for (int i = 0; i < AudioSources.Count; i++)
        {
            AudioSources[i].UnPause();
            print("UNPAUSE");
        }
    }

    public void ToSeal()
    {
        if (!stemStairBool)
        {
            sealStemAudioSource.volume = sealStemVolume;
            otterStemAudioSource.volume = 0;
            frogStemAudioSource.volume = 0;
            stemFocus = StemFocus.Seal;
        }

    }
    public void ToOtter()
    {
        if (!stemStairBool)
        {
            sealStemAudioSource.volume = 0;
            otterStemAudioSource.volume = otterStemVolume;
            frogStemAudioSource.volume = 0;
            stemFocus = StemFocus.Otter;
        }
        
    }
    public void ToFrog()
    {
        if (!stemStairBool)
        {
            sealStemAudioSource.volume = 0;
            otterStemAudioSource.volume = 0;
            frogStemAudioSource.volume = frogStemVolume;
            stemFocus = StemFocus.Frog;
        }
        
    }

    public void UpdateStemStairs()
    {
        stemFocus = StemFocus.Stairs;
        switch (stemCount)
        {
            case 1:
                mainStemAudioSource.volume = mainStemVolume;
                sealStemAudioSource.volume = 0;
                otterStemAudioSource.volume = 0;
                frogStemAudioSource.volume = 0;
                break;
            case 2:
                mainStemAudioSource.volume = mainStemVolume;
                sealStemAudioSource.volume = sealStemVolume;
                otterStemAudioSource.volume = 0;
                frogStemAudioSource.volume = 0;
                break;
            case 3:
                mainStemAudioSource.volume = mainStemVolume;
                sealStemAudioSource.volume = sealStemVolume;
                otterStemAudioSource.volume = otterStemVolume;
                frogStemAudioSource.volume = 0;
                break;
            case 4:
                mainStemAudioSource.volume = mainStemVolume;
                sealStemAudioSource.volume = sealStemVolume;
                otterStemAudioSource.volume = otterStemVolume;
                frogStemAudioSource.volume = frogStemVolume;
                break;
        }
    }

    public void stemCountInt(int value)
    {
        stemCount += value;
        if (stemCount <= 4 && stemCount > 0)
        {

            UpdateStemStairs();
        }
        else if (stemCount > 4)
        {
            stemCount = 4;
        }
        else if (stemCount <= 0)
        {
            stemCount = 0;
        }
    }
}

