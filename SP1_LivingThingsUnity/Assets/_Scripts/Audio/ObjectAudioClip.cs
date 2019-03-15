using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAudioClip : MonoBehaviour
{
    [SerializeField]
    public AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> audioClips = new List<AudioClip>();

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    private int RandomizeClip(int min, int max)
    {
        int randomIndex;
        return randomIndex = Random.Range(min, max);      
    }

    public void PlaySingle(int min, int max)
    {

        audioSource.clip = audioClips[RandomizeClip(min, max)];
        
        audioSource.Play();
    }

    public void PlaySingle(int clipIndex)
    {
        audioSource.clip = audioClips[clipIndex];

        audioSource.Play();
    }

    public void PlaySingle(AudioClip clip)
    {
        audioSource.clip = clip;

        audioSource.Play();
    }
    
    public void PlaySingle(List<AudioClip> soundList)
    {
        audioSource.clip = soundList[RandomizeClip(0, soundList.Count)];

        audioSource.Play();
    }

    public void PlayRandom()
    {
        audioSource.clip = audioClips[RandomizeClip(0, audioClips.Count)];

        audioSource.Play();
    }
}
