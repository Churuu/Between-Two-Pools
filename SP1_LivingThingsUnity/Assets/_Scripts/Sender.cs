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
        buttonSwitch = 0, sealSwitch, pressureSwitch
    }
    [SerializeField]
    private ButtonType buttonType;


    [SerializeField]
    private List<GameObject> gameObjects = new List<GameObject>();

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
            GetComponent<ObjectAudioClip>().PlaySingle(0);
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
        }

        else if (buttonType == ButtonType.sealSwitch)
        {

        }
    }

    public void ActivatePlate()
    {
        GetComponent<ObjectAudioClip>().PlaySingle(0);
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

    public ButtonType GetButtonType()
    {
        return buttonType;
    }
}
