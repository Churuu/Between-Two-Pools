//Joakim

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour {

    public Transform Cam;
    Vector2 lastCamPos;
    Vector2 Diff = new Vector2(0, 0);
    public float speed;
    Vector2 Move;


    void Start()
    {

        lastCamPos = Cam.position;
    }


    void Update()
    {
        Diff.x = Cam.position.x;
        Debug.Log(Diff.x);
		Diff.y = Cam.position.y;
        Move = new Vector2(Diff.x, Diff.y);
        transform.position = new Vector3(Move.x, Move.y, 0);
        lastCamPos = Cam.position;
    }
}
