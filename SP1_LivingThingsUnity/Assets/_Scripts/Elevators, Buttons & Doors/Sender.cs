using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Jesper Li 07/02 - 19
public class Sender : MonoBehaviour
{
    public Animator anim;
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
    private List<GameObject> nonActivatableGameObjects = new List<GameObject>();
    private int test = 0;


    private float timer;
    private bool timerBool;
    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        nonActivatableGameObjects = gameObjects;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (timerBool)
        {
            if (timer > 0.6f)
            {
                anim.SetBool("Pressed", false);
                timerBool = false;
            }
            else
                timer += Time.deltaTime;
        }
        else
            timer = 0;

        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (nonActivatableGameObjects[i].GetComponent<Reciever>().GetDoorActivatable() == false)
            {
                test++;
                //nonActivatableGameObjects[i]
            }
            
        }
    }

    public void BoolToggle()
    {
        if (GetButtonType() == ButtonType.buttonSwitch)
        {
            
            if (switchType == SwitchType.door)
            {
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    if (gameObjects[i].GetComponent<Reciever>().GetDoorActivatable())
                    {
                        GetComponent<ObjectAudioClip>().PlayRandom();
                        anim.SetBool("Pressed", true);
                        gameObjects[i].GetComponent<Reciever>().ToggleObject();
                    }
                       
                }
            }

            else if (switchType == SwitchType.lock1)
            {
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    if (gameObjects[i].GetComponent<Reciever>().GetDoorActivatable() == false)
                    {
                        GetComponent<ObjectAudioClip>().PlayRandom();
                        anim.SetBool("Pressed", true);
                        gameObjects[i].GetComponent<Reciever>().BoolToogle();

                    }
                        
                }
            }

            else if (switchType == SwitchType.all)
            {
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    if (gameObjects[i].GetComponent<Reciever>().GetDoorActivatable())
                    {
                        GetComponent<ObjectAudioClip>().PlayRandom();
                        anim.SetBool("Pressed", true);
                        gameObjects[i].GetComponent<Reciever>().ToggleObject();
                        gameObjects[i].GetComponent<Reciever>().BoolToogle();
                    }

                }
            }

            timerBool = true;
        }
        
    }

    public void ActivatePlate()
    {
        
        if (switchType == SwitchType.door)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i].GetComponent<Reciever>().GetDoorActivatable())
                {
                    GetComponent<ObjectAudioClip>().PlaySingle(0);
                    anim.SetBool("Pressed", true);
                    gameObjects[i].GetComponent<Reciever>().ToggleObject();
                }
                
            }
        }

        else if (switchType == SwitchType.lock1)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i].GetComponent<Reciever>().GetDoorActivatable() == false)
                {
                    GetComponent<ObjectAudioClip>().PlaySingle(0);
                    anim.SetBool("Pressed", true);
                    gameObjects[i].GetComponent<Reciever>().BoolToogle();
                }
                
                
            }
        }

        else if (switchType == SwitchType.all)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i].GetComponent<Reciever>().GetDoorActivatable())
                {
                    GetComponent<ObjectAudioClip>().PlaySingle(0);
                    anim.SetBool("Pressed", true);
                    gameObjects[i].GetComponent<Reciever>().ToggleObject();
                    gameObjects[i].GetComponent<Reciever>().BoolToogle();
                }
            }
        }
        timerBool = true;
        
    }

    public ButtonType GetButtonType()
    {
        return buttonType;
    }
}
