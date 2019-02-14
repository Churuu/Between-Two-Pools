using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAudioClip : MonoBehaviour
{
    [SerializeField]
    private GameObject audioManager;
    [SerializeField]
    private List<AudioClip> audioClips = new List<AudioClip>();
	// Use this for initialization
	void Start ()
    {
        audioManager = FindObjectOfType<AudioManager>().gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    private int RandomizeClip()
    {
        int randomIndex;
        return randomIndex = Random.Range(0, audioClips.Count);      
    }

    public void PlayClip()
    {
        audioManager.GetComponent<AudioManager>().PlaySingle(audioClips[RandomizeClip()]);
        
    }
}
