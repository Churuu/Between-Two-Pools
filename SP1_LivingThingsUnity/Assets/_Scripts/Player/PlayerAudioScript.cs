using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioScript : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> walkingSounds = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> jumpSounds = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> ability1List = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> ability2List = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> landingSounds = new List<AudioClip>();

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void Walk()
    {
        GetComponent<ObjectAudioClip>().PlaySingle(walkingSounds);
    }

    private void Jump()
    {
        GetComponent<ObjectAudioClip>().PlaySingle(jumpSounds);
    }

    private void Landing()
    {
        GetComponent<ObjectAudioClip>().PlaySingle(landingSounds);
    }

    private void Ability1()
    {
        GetComponent<ObjectAudioClip>().PlaySingle(ability1List);
    }

    private void Ability2()
    {
        GetComponent<ObjectAudioClip>().PlaySingle(ability2List);
    }
}
