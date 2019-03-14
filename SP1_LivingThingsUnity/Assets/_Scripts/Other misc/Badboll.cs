//Joakim
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Badboll : MonoBehaviour {
    [SerializeField] Sprite Deflated;
    private SpriteRenderer changeSprite;
	// Use this for initialization
	void Start () {
        changeSprite = GetComponent<SpriteRenderer>();
	}
	
    private void OnCollisionEnter2D(Collision2D collision)
    {
      //  Debug.Log("hit");
        if(collision.gameObject.CompareTag("Rock"))
        {
            changeSprite.sprite = Deflated;
            GetComponent<BoxCollider2D>().enabled = false;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.338f, 0);
        }
    }
}
