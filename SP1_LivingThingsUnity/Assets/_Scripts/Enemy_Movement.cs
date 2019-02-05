using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 Random kommentarer
*/
public class Enemy_Movement : MonoBehaviour {
    public int StartRiktning; // värden -1 startar åt vänster, 0 stårstilla, 1 startar åt höger
    public float Enemy_Move_speed;
    public float Distance_From_Start_Pos;
    private Rigidbody2D Enemy_Rigid;
    private SpriteRenderer Flip_Sprite;
    private float MaxDist;
    private float NegMaxDist;
    private bool going_right = false;
    private bool going_left = false;


    void Start () {
        Enemy_Rigid = GetComponent<Rigidbody2D>();
        Flip_Sprite = GetComponent<SpriteRenderer>();
        Vector2 movement = new Vector2(StartRiktning, 0.0f);
        Enemy_Rigid.velocity = new Vector2(movement.x * Enemy_Move_speed, movement.y * Enemy_Move_speed);
        Physics2D.IgnoreLayerCollision(8, 8);
        // Om Distance_From_Start_Pos är 0 så patrullerar fienden fram tills kollition med vägg
        if(Distance_From_Start_Pos == 0) {
            MaxDist = 100000;
            NegMaxDist = -100000;
        }
        else {
            MaxDist = transform.position.x + Distance_From_Start_Pos;
            NegMaxDist = transform.position.x - Distance_From_Start_Pos;
        }

        if (StartRiktning > 0) { going_right = true; }
        else { going_left = true; }

    }

	void Update () {
        Vector2 movement = new Vector2(StartRiktning, 0.0f);
        Enemy_Rigid.velocity = new Vector2(movement.x * Enemy_Move_speed, movement.y * Enemy_Move_speed);
        ChangeDirection();
    }
    void ChangeDirection()
    {
        // Byt riktning efter satt distans från startpositionen om inte kollision med vägg sker innan
        if (transform.position.x > MaxDist && going_right == true)
        {
            StartRiktning = StartRiktning * -1;
            Flip_Sprite.flipX = !Flip_Sprite.flipX;
            going_right = false;
            going_left = true;
        }
        else if (transform.position.x < NegMaxDist && going_left == true)
        {
            StartRiktning = StartRiktning * -1;
            Flip_Sprite.flipX = !Flip_Sprite.flipX;
            going_right = true;
            going_left = false;
        }
        else { }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Byt riktning vid kollition med vägg
        if (col.gameObject.CompareTag("Vägg")){
            StartRiktning = StartRiktning * -1;
            Flip_Sprite.flipX = !Flip_Sprite.flipX;
            going_right = !going_right;
            going_left = !going_left;
        } 
    }

    void OnDrawGizmosSelected()
    {
        Vector3 Target_Pos1 = new Vector3(transform.position.x + Distance_From_Start_Pos, transform.position.y, transform.position.z);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, Target_Pos1);
        Gizmos.DrawCube(Target_Pos1, new Vector3(1, 1, 1));
        Vector3 Target_Pos2 = new Vector3(transform.position.x - Distance_From_Start_Pos, transform.position.y, transform.position.z);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, Target_Pos2);
        Gizmos.DrawCube(Target_Pos2, new Vector3(1, 1, 1));
    }
}
