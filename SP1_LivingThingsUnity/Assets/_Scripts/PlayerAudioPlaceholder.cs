﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioPlaceholder : MonoBehaviour
{

    [SerializeField]
    private List<AudioClip> walkingSounds = new List<AudioClip>();
    [SerializeField]
    private bool test1;
    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        PlayerMovementCheck();
	}

    public void PlayerMovementCheck()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            GetComponent<ObjectAudioClip>().PlaySingle(0);
        }
    }

    private void Step()
    {
        GetComponent<ObjectAudioClip>().PlaySingle(walkingSounds);
    }
}