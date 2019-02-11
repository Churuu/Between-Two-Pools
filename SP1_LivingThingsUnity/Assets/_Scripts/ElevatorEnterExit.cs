using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorEnterExit : MonoBehaviour
{
    private GameObject go;
    private bool goBool = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //TriggerStay();
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("test");
        if (other.gameObject.GetComponent<Movement>() != null)
        {
            //go = other.gameObject;
            //goBool = true;
            
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        //goBool = false;
        if (other.gameObject.GetComponent<Movement>() != null)
            other.transform.SetParent(null);
        //go = null;
    }

    private void TriggerStay()
    {
        if (goBool)
        {
            go.transform.SetParent(transform);
        }
        else
            go.transform.SetParent(null);
    }
}
