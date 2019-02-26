using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SignOfTruth : MonoBehaviour
{

    public string rats = "Seal", friends = "Frog";

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var col = collider.gameObject;

        if (col.CompareTag(rats))
        {
            anim.SetBool("Rats", false);
        }
        else if (col.CompareTag(friends))
        {
            anim.SetBool("Rats", true);
        }

    }
}
