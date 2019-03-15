using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
    public static AudioManager instance = null;     //Allows other scripts to call functions from SoundManager.             

    [SerializeField]
    private List<AudioClip> musicClips = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> ambienceClips = new List<AudioClip>();

    [SerializeField]
    private AudioClip mainMenuMusic;
    [SerializeField]
    private AudioClip pauseMenuMusic;

    [SerializeField]
    private AudioClip winMusic;
    [SerializeField]
    private AudioClip loseMusic;

    [SerializeField]
    private string nextSong = "NextSong";

    [SerializeField]
    private string previousSong = "PreviousSong";

    private int musicIndex = 0;

    private GameObject stemsManager;

    private bool pause = false;

    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);

        stemsManager = FindObjectOfType<StemsManager>().gameObject;
    }

    void Update()
    {
        if (!pause)
        {
            //if (musicIndex < 0)
            //{
            //    musicIndex = musicClips.Count - 1;
            //}
            //else if (musicIndex >= musicClips.Count)
            //{
            //    musicIndex = 0;
            //}
            

            //SwitchMusic();
        }
        else if (pause)
        {
            
            if (musicSource.clip.name != pauseMenuMusic.name)
            {
                musicSource.clip = pauseMenuMusic;
                musicSource.Play();
            }
        }
        if (SceneManager.GetActiveScene().name == "MainMenu" ||
                SceneManager.GetActiveScene().name == "StartScene" ||
                SceneManager.GetActiveScene().name == "Options" ||
                SceneManager.GetActiveScene().name == "Credits")
        {
            if (musicSource.clip.name != mainMenuMusic.name)
            {
                musicSource.clip = mainMenuMusic;
                musicSource.Play();            }
        }
        print("isPlaying: " + musicSource.isPlaying);
    }

    public void pauseBool(bool pBool)
    {
        if (pBool)
        {
            stemsManager.GetComponent<StemsManager>().OnMenuPause();
            pause = true;
            musicSource.UnPause();
        }
        if (!pBool)
        {
            stemsManager.GetComponent<StemsManager>().OnMenuUnPause();
            pause = false;
            musicSource.Pause();
        }
    }


    public void SongRequest()
    {
        musicSource.clip = musicClips[musicIndex];

        musicSource.Play();
    }

    public void SongRequest(int musicI)
    {
        musicIndex = musicI;
        musicSource.clip = musicClips[musicIndex];

        musicSource.Play();
    }

    public void SwitchMusic()
    {
        if (Input.GetButtonDown(nextSong))
        {
            musicIndex++;
            SongRequest();
        }
        else if (Input.GetButtonDown(previousSong))
        {
            musicIndex--;
            SongRequest();
        }
    }

}