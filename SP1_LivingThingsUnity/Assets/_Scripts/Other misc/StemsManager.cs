using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StemsManager : MonoBehaviour
{
    [SerializeField] private AudioSource sealStemAudioSource;

    [SerializeField] private AudioSource otterStemAudioSource;

    [SerializeField] private AudioSource frogStemAudioSource;

    //[SerializeField] private AudioSource mainStemAudioSource;

    private float sealStemVolume;
    private float otterStemVolume;
    private float frogStemVolume;

    // Use this for initialization
    void Start ()
    {
        sealStemVolume = sealStemAudioSource.volume;
        otterStemVolume = otterStemAudioSource.volume;
        frogStemVolume = frogStemAudioSource.volume;

        ToOtter();
	}
	
	// Update is called once per frame
	void Update ()
    {
        print(sealStemAudioSource.volume + ": seal");
        print(otterStemAudioSource.volume + ": otter");
        print(frogStemAudioSource.volume + ": frog");
    }

    public void ToSeal()
    {
        sealStemAudioSource.volume = sealStemVolume;
        otterStemAudioSource.volume = 0;
        frogStemAudioSource.volume = 0;
    }
    public void ToOtter()
    {
        sealStemAudioSource.volume = 0;
        otterStemAudioSource.volume = otterStemVolume;
        frogStemAudioSource.volume = 0;
    }
    public void ToFrog()
    {
        sealStemAudioSource.volume = 0;
        otterStemAudioSource.volume = 0;
        frogStemAudioSource.volume = frogStemVolume;
    }
}
