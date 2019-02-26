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
    private AudioClip ability1;

    [SerializeField]
    private AudioClip ability2;

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

    private void Ability1()
    {
        GetComponent<ObjectAudioClip>().PlaySingle(ability1);
    }

    private void Ability2()
    {
        GetComponent<ObjectAudioClip>().PlaySingle(ability2);
    }
}
