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

    }

    void SongRequest(int musicIndex)
    {
        musicSource.clip = musicClips[musicIndex];

        musicSource.Play();
    }

}