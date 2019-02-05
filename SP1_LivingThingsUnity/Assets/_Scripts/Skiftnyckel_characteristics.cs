using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skiftnyckel_characteristics : MonoBehaviour {
    public float Degresperclick;
    public Vector3 OffsetRotatePoint;
    private Rigidbody2D rb2D;
    private SpriteRenderer Sprite;
    private bool AbiltyActive;
    private bool movementScriptActive;
    private float rotate;
    private Vector3 MutterPos;
	// Use this for initialization
	void Start () {
        AbiltyActive = false;
        movementScriptActive = true;
        rb2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
           
            AbiltyActive = !AbiltyActive;
            //endast visuelt hjälpmedel eftersom rätta sprites inte finns än
            Sprite = GetComponent<SpriteRenderer>();
            if (AbiltyActive == true)
            {
                Sprite.color = Color.red;
            }
            else
            {
                Sprite.color = Color.white;
            }
            // Ability avaktiverad 
            if (AbiltyActive == false)
            {
                movementScriptActive = true;
                GetComponent<Movement>().enabled = true;
                rb2D.gravityScale = 1;
                transform.rotation = Quaternion.identity; // reset Rotation to original orientation
            }
        }

        // rotera runt muttern
        if(AbiltyActive == true && movementScriptActive == false)
        {
            rotate = Degresperclick;
            if (Input.GetKeyDown(KeyCode.RightArrow))
                transform.RotateAround(MutterPos, new Vector3(0 ,0 ,1), rotate);
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                transform.RotateAround(MutterPos, new Vector3(0, 0, 1), -rotate);
        } 
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Mutter"))
        {
            if (AbiltyActive == true)
            {
                MutterPos = col.transform.position;
                transform.position = col.transform.position - OffsetRotatePoint;
                rb2D.velocity = new Vector2(0, 0);
                rb2D.gravityScale = 0;
                movementScriptActive = false;
                GetComponent<Movement>().enabled = false;
            }
        }
    }
    
}
