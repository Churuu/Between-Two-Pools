using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StemsManager : MonoBehaviour
{
    [SerializeField] private AudioSource sealStemAudioSource;

    [SerializeField] private AudioSource otterStemAudioSource;

    [SerializeField] private AudioSource frogStemAudioSource;

    //[SerializeField] private AudioSource mainStemAudioSource;
    private AudioSource mainStemSource;

    private float mainStemVolume;
    private float sealStemVolume;
    private float otterStemVolume;
    private float frogStemVolume;

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
        mainStemSource = GetComponent<AudioSource>();

        ToOtter();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    public void OnMenuPause()
    {
        mainStemSource.Pause();
        sealStemAudioSource.Pause();
        otterStemAudioSource.Pause();
        frogStemAudioSource.Pause();
    }

    public void OnMenuUnPause()
    {
        mainStemSource.UnPause();
        sealStemAudioSource.UnPause();
        otterStemAudioSource.UnPause();
        frogStemAudioSource.UnPause();
        switch (stemFocus)
        {
            case StemFocus.Otter:
                ToOtter();
                break;
            case StemFocus.Seal:
                ToSeal();
                break;
            case StemFocus.Frog:
                ToFrog();
                break;
        }
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
