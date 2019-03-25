using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterExit : MonoBehaviour
{

    private GameObject go;
    private bool goBool = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TriggerStay();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Reciever>() != null)
        {
            if (other.gameObject.GetComponent<Reciever>().GetDoorType(Reciever.DoorType.moveTimer) 
                || other.gameObject.GetComponent<Reciever>().GetDoorType(Reciever.DoorType.move))
            {
                transform.SetParent(other.transform);
            }
        }
        
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        //goBool = false;
        if (other.gameObject.GetComponent<Reciever>() != null)
        {
            if (other.gameObject.GetComponent<Reciever>().GetDoorType(Reciever.DoorType.moveTimer) 
                || other.gameObject.GetComponent<Reciever>().GetDoorType(Reciever.DoorType.move))
            {
                transform.SetParent(null);
            }
           
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Sender>() != null)
        {
            go = other.gameObject;
            if (go.GetComponent<Sender>().GetButtonType() == Sender.ButtonType.pressureSwitch)
            {
                go.GetComponent<Sender>().ActivatePlate();
            }

            else if (go.GetComponent<Sender>().GetButtonType() == Sender.ButtonType.sealSwitch)
            {
                if (gameObject.tag == "Seal")
                {
                    go.GetComponent<Sender>().ActivatePlate();
                }
            }
            else
                goBool = true;
        }
        if (other.gameObject.tag == "StemIncrease")
        {
            print("tag");
            FindObjectOfType<StemsManager>().stemCountInt(1);
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Sender>() != null)
        {
            if (go.GetComponent<Sender>().GetButtonType() == Sender.ButtonType.pressureSwitch)
            {
                go.GetComponent<Sender>().ActivatePlate();
            }

            else if (go.GetComponent<Sender>().GetButtonType() == Sender.ButtonType.sealSwitch)
            {
                if (gameObject.tag == "Seal")
                {
                    go.GetComponent<Sender>().ActivatePlate();
                }
            }
            else
                goBool = false;

            go = null;
        }
    }

    private void TriggerStay()
    {
        if (goBool && GetComponent<PlayerController>().GetPlayerActive())
        {
            if (Input.GetKeyDown(KeyCode.E) && go.GetComponent<Sender>().GetButtonType() == Sender.ButtonType.buttonSwitch)
            {
                go.GetComponent<Sender>().BoolToggle();
            }
        }

    }
}
