using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SignOfTruth : MonoBehaviour
{

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var col = collider.gameObject;

        if (col.CompareTag("Seal"))
        {
            anim.SetBool("Rats", false);
        }
        else if (col.CompareTag("Frog"))
        {
            anim.SetBool("Rats", true);
        }

    }
}
