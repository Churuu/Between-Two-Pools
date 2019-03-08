using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private string nextSong = "NextSong";

    [SerializeField]
    private string previousSong = "PreviousSong";

    private int musicIndex = 0;

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
    }

    void Update()
    {
        if (musicIndex < 0)
        {
            musicIndex = musicClips.Count - 1;
        }
        else if (musicIndex >= musicClips.Count)
        {
            musicIndex = 0;
        }
        SwitchMusic();
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