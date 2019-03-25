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
    private List<AudioClip> ability3List = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> landingSounds = new List<AudioClip>();

    [SerializeField]
    private AudioClip deathSound;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Walk()
    {
        GetComponent<ObjectAudioClip>().PlaySingle(walkingSounds);
    }

    public void Jump()
    {
        GetComponent<ObjectAudioClip>().PlaySingle(jumpSounds);
    }

    public void Landing()
    {
        GetComponent<ObjectAudioClip>().PlaySingle(landingSounds);
    }

    public void Ability1()
    {
        GetComponent<ObjectAudioClip>().PlaySingle(ability1List);
    }

    public void Ability2()
    {
        GetComponent<ObjectAudioClip>().PlaySingle(ability2List);
    }

    public void Ability3()
    {
        GetComponent<ObjectAudioClip>().PlaySingle(ability3List);
    }
    
    public void Death()
    {
        GetComponent<ObjectAudioClip>().PlaySingle(deathSound);
    }
}
