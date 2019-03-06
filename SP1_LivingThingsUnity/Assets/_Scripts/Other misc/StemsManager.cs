using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StemsManager : MonoBehaviour
{
    [SerializeField] private AudioSource SealStemAudioSource;

    [SerializeField] private AudioSource OtterStemAudioSource;

    [SerializeField] private AudioSource FrogStemAudioSource;

    private float SealStemVolume;
    private float OtterStemVolume;
    private float FrogStemVolume;

    // Use this for initialization
    void Start ()
    {
        SealStemVolume = SealStemAudioSource.volume;
        OtterStemVolume = OtterStemAudioSource.volume;
        FrogStemVolume = FrogStemAudioSource.volume;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ToSeal()
    {
        SealStemAudioSource.volume = SealStemVolume;
        OtterStemAudioSource.volume = 0;
        FrogStemAudioSource.volume = 0;
    }
    public void ToOtter()
    {
        SealStemAudioSource.volume = 0;
        OtterStemAudioSource.volume = OtterStemVolume;
        FrogStemAudioSource.volume = 0;
    }
    public void ToFrog()
    {
        SealStemAudioSource.volume = 0;
        OtterStemAudioSource.volume = 0;
        FrogStemAudioSource.volume = FrogStemVolume;
    }
}
