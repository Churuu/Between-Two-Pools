using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour {



    void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<SceneTransitioner>().LoadScene("RobinTesting");
    }
}
