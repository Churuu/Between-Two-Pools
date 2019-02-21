//Joakim

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour {

    public Camera Cam;
    Vector2 lastCamPos;
    Vector2 BackgroundStartPos;
    Vector2 Diff = new Vector2(0, 0);
    public float speed;
    Vector2 Move;


    void Start()
    {

        lastCamPos = Cam.transform.position;
        BackgroundStartPos = transform.position;

    }


    void Update()
    {
        Diff.x = (Cam.transform.position.x + BackgroundStartPos.x) / speed;
        Debug.Log(transform.position);
		Diff.y = (Cam.transform.position.y + BackgroundStartPos.y) / speed;
        Move = new Vector2(Diff.x, Diff.y);
        transform.position = new Vector3(Move.x, Move.y, 0);
        lastCamPos = Cam.transform.position;
    }
}
