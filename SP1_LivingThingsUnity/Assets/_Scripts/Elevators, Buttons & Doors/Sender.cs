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

    private bool pressedAnimBool = false;
    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        nonActivatableGameObjects = gameObjects;
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(gameObject.name + "Count: " + gameObjects.Count);
        //print(gameObject.name + "test: " + test);

        //if (timerBool == true)
        //{

        //    if (timer > 0.6f)
        //    {

        //        anim.SetBool("Pressed", false);
        //        timerBool = false;
        //    }
        //    else
        //        timer += 0.1f;
        //}
        //else
        //    timer = 0;

        anim.SetBool("Pressed", pressedAnimBool);

        if (nonActivatableGameObjects.Count == 0)
        {
            print(gameObject.name + "Should stop anim");
            AnimBoolTrue();
        }
        else if (nonActivatableGameObjects.Count != 0)
        {
            for (int i = 0; i < nonActivatableGameObjects.Count; i++)
            {
                if (nonActivatableGameObjects[i].GetComponent<Reciever>().GetDoorActivatable() == false)
                {
                    nonActivatableGameObjects.RemoveAt(i);
                    break;
                }

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
                        gameObjects[i].GetComponent<Reciever>().ObjectCamera();
                        GetComponent<ObjectAudioClip>().PlayRandom();
                        pressedAnimBool = true;
                        //anim.SetBool("Pressed", true);
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
                        gameObjects[i].GetComponent<Reciever>().ObjectCamera();
                        GetComponent<ObjectAudioClip>().PlayRandom();
                        //anim.SetBool("Pressed", true);
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
                        gameObjects[i].GetComponent<Reciever>().ObjectCamera();
                        GetComponent<ObjectAudioClip>().PlayRandom();
                        pressedAnimBool = true;
                        //anim.SetBool("Pressed", true);
                        print("SwitchTypeAll: true");
                        gameObjects[i].GetComponent<Reciever>().ToggleObject();
                        gameObjects[i].GetComponent<Reciever>().BoolToogle();
                    }

                }
            }

            //timerBool = true;

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
                    gameObjects[i].GetComponent<Reciever>().ObjectCamera();
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
                    gameObjects[i].GetComponent<Reciever>().ObjectCamera();
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
                    gameObjects[i].GetComponent<Reciever>().ObjectCamera();
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

    public void AnimBoolTrue()
    {
        pressedAnimBool = true;
    }

    public void AnimBoolFalse()
    {
        pressedAnimBool = false;
    }
}
