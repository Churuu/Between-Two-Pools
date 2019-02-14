using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Jesper Li 07/02 - 19
public class Reciever : MonoBehaviour
{
    [SerializeField]
    private GameObject audioObject;

    private AudioSource audioSource;

    public enum DoorType
    {
        flip = 0, move, moveTimer
    }
    [SerializeField]
    DoorType doorType;

    //Motion Variables
    [Space]
    [Header("Motion Variables")]
    [SerializeField]
    private Vector3 start;
    [SerializeField]
    private Vector3 target;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float acceleration = 1.1f;
    [SerializeField]
    private float tolerance = 0.15f;

    //Variables for different door states
    [Space]
    [SerializeField]
    private bool gameObjectToggle = true;
    [SerializeField]
    private bool toggle = true;
    [SerializeField]
    private bool testbool = false;

    [Space]
    [Header("Timed Platform Variables")]
    [SerializeField]
    private bool timerToggle;
    [SerializeField]
    private float timer;

    float timerFloat = 0;

    // Use this for initialization
    void Start ()
    {
        
        start = transform.position;
        gameObject.SetActive(gameObjectToggle);
        if (audioObject != null)
        {
            audioSource = audioObject.GetComponent<AudioSource>();
        }
        
    }
    
	// Update is called once per frame
	void Update ()
    {
        if (testbool)
        ToggleElevator(start, target, speed, acceleration, Time.deltaTime, tolerance, gameObjectToggle);
	}

    public void ToggleObject()
    {
        if (toggle)
        {
            //gameObjectToggle = !gameObjectToggle;
            //audioSource.Play();

            if (doorType == DoorType.flip)
            {
                gameObjectToggle = !gameObjectToggle;
                gameObject.SetActive(gameObjectToggle);
            }

            else if (doorType == DoorType.move)
            {
                gameObjectToggle = !gameObjectToggle;
            }

            else if (doorType == DoorType.moveTimer)
            {
                timerToggle = !timerToggle;
            }
        } 
    }

    //public void ToggleObject(bool senderBool)
    //{
    //    if (toggle)
    //    {
    //        audioSource.Play();
    //        if (gameObjectToggle == false)
    //        {
    //            gameObjectToggle = senderBool;
    //        }
    //        else
    //            gameObjectToggle = false;
    //        if (doorType == DoorType.flip)
    //        {
    //            gameObject.SetActive(gameObjectToggle);
    //        }
    //        else if (doorType == DoorType.moveTimer)
    //        {
    //            timerToggle = true;
    //        }
    //    }
    //}

    public void BoolToogle()
    {
        testbool = true;
        toggle = false;
    }

    private void ToggleElevator(Vector3 start, Vector3 target, float speed, float acceleration,
        float time, float tolerance, bool flip)
    {

        Motion motion = new Motion(start, target, speed, acceleration, time, tolerance);
        motion.Source = transform.position;

        if (flip)
        {
            motion.Target = target;
        }
        else
        {
            motion.Target = start;

        }
        if (doorType != DoorType.flip)
        {
            transform.Translate(motion.SourceToTarget * motion.Velocity * Time.deltaTime);
        }
        

        if (timerToggle && motion.InTargetRegion)
        {
            
            if (timerFloat < timer)
            {
                timerFloat += Time.deltaTime;
            }
            else if (timerFloat >= timer)
            {
                gameObjectToggle = !gameObjectToggle;
                //timerToggle = false;
                timerFloat = 0;
            }
        }
    }

    public bool GetDoorType(DoorType dT)
    {
        if (dT == doorType)
        {
            return true;
        }
        else
            return false;
    }
}

