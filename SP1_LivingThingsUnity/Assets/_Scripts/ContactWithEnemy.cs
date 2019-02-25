//Joakim
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactWithEnemy : MonoBehaviour {
    public string enemyTag;
    public string deadAnimParam;
    private Animator anim;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(enemyTag))
        {
            if (GetComponent<Frog>() != null)
                GetComponent<Frog>().enabled = false;
           
            if(GetComponent<Otter>() != null)
                GetComponent<Otter>().enabled = false;

            GetComponent<PlayerController>().enabled = false;
            anim.SetBool(deadAnimParam, true);


        }
    }

    
}
