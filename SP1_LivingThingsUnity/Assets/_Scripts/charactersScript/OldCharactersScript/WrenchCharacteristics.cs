//Joakim
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchCharacteristics : MonoBehaviour {

    public float DegresPerRotation;
    public Vector3 OffsetRotatePoint;
    public string TagOnMutter;
    public string Abillity;
    public string RotateRight;
    public string RotateLeft;
    private Rigidbody2D rb2D;
    private SpriteRenderer Sprite;
    [HideInInspector] public bool AbiltyActive;
    private bool movementScriptActive;
    private Vector3 MutterPos;
    // Use this for initialization
    void Start()
    {
        AbiltyActive = false;
        movementScriptActive = true;
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(Abillity))
        {
            AbiltyActive = !AbiltyActive;
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
        if (AbiltyActive == true && movementScriptActive == false)
        {
            if (Input.GetButtonDown(RotateRight))
                transform.RotateAround(MutterPos, new Vector3(0, 0, 1), DegresPerRotation);
            else if (Input.GetButtonDown(RotateLeft))
                transform.RotateAround(MutterPos, new Vector3(0, 0, 1), -DegresPerRotation);   
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagOnMutter))
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

