using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Jesper Li 07/02 - 19
public class Sender : MonoBehaviour
{
    
    public enum SwitchType
    {
        door, lock1, all
    }
    [SerializeField]
    SwitchType switchType;

    public enum ButtonType
    {
        hammerSwitch, buttonSwitch = 0, pressureSwitch
    }
    [SerializeField]
    ButtonType buttonType;


    [SerializeField]
    List<GameObject> gameObjects = new List<GameObject>();

    [SerializeField]
    bool test1;
    [SerializeField]
    bool test2;
    [SerializeField]
    bool test3;
    [SerializeField]
    bool test4;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void BoolToggle()
    {
        if (GetButtonType() == ButtonType.buttonSwitch)
        {
            if (switchType == SwitchType.door)
            {
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    gameObjects[i].GetComponent<Reciever>().ToggleObject();
                }
            }

            else if (switchType == SwitchType.lock1)
            {
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    gameObjects[i].GetComponent<Reciever>().BoolToogle();
                }
            }

            else if (switchType == SwitchType.all)
            {
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    
                    gameObjects[i].GetComponent<Reciever>().ToggleObject();
                    gameObjects[i].GetComponent<Reciever>().BoolToogle();
                }
            }
        }

        else if (buttonType == ButtonType.hammerSwitch)
        {

        }

        else if (buttonType == ButtonType.pressureSwitch)
        {
            
        }
    }

    public void ActivatePlate()
    {
        test3 = !test3;
        if (switchType == SwitchType.door)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].GetComponent<Reciever>().ToggleObject();
            }
        }

        else if (switchType == SwitchType.lock1)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].GetComponent<Reciever>().BoolToogle();
            }
        }

        else if (switchType == SwitchType.all)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].GetComponent<Reciever>().BoolToogle();
                gameObjects[i].GetComponent<Reciever>().ToggleObject();
            }
        }

        //}

    }

    public ButtonType GetButtonType()
    {
        return buttonType;
    }
}
