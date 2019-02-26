//Joakim
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactWithEnemy : MonoBehaviour {
    public string enemyTag = "Enemy";
    public string deadAnimParam = "Dead";
    private Animator anim;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(enemyTag))
        {
            if (EventManager.instance.onKilld != null)
            {
                EventManager.instance.onKilld(this.gameObject);
                anim.SetBool(deadAnimParam, true);
            }
           
        }
    }

    
}
