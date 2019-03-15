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
    private bool winLoseStingerPlaying = false;

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

        if (winLoseStingerPlaying)
        {
            if (musicSource.clip.name == winMusic.name || musicSource.clip.name == loseMusic.name)
            {
                if (musicSource.isPlaying)
                {
                    winLoseStingerPlaying = false;
                }
            }
        }
        else if (!winLoseStingerPlaying)
        {
            musicChange();
        }
        
    }

    private void musicChange()
    {
        if (!pause)
        {

        }
        else if (pause)
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
                musicSource.Play();
            }
        }
        print("isPlaying: " + musicSource.isPlaying);
    }

    public void PauseBool(bool pBool)
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

    public void OnWinStinger()
    {
        winLoseStingerPlaying = true;
        stemsManager.GetComponent<StemsManager>().OnMenuPause();
        musicSource.clip = winMusic;

        musicSource.Play();
    }

    public void OnLoseStinger()
    {
        winLoseStingerPlaying = true;
        musicSource.clip = loseMusic;

        musicSource.Play();
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