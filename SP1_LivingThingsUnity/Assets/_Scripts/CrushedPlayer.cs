//Joakim
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushedPlayer : MonoBehaviour {
    public float crushDistance = 1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Crushed())
        {
            //Döda karaktären
            Debug.Log("Player crushed");
        }
        else { }
	}

    public bool Crushed()
    {

        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, crushDistance);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, crushDistance);

        if (hitDown.collider != null && hitUp.collider != null)
            return true;

        return false;

    }
}
