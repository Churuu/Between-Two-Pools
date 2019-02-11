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
    private float speed;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float time;
    [SerializeField]
    private float tolerance;

    //Variables for different door states
    [Space]
    [SerializeField]
    private bool gameObjectToggle;
    [SerializeField]
    private bool toggle;
    [SerializeField]
    private bool moveBool;

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
        gameObject.SetActive(gameObjectToggle);
        if (audioObject != null)
        {
            audioSource = audioObject.GetComponent<AudioSource>();
        }
        
    }
    
	// Update is called once per frame
	void Update ()
    {
        ToggleElevator(start, target, speed, acceleration, time, tolerance, gameObjectToggle);

        
	}

    public void ToggleObject()
    {
        if (toggle)
        {
            gameObjectToggle = !gameObjectToggle;
            audioSource.Play();

            if (doorType == DoorType.flip)
            {
                gameObject.SetActive(gameObjectToggle);

            }
            else if (doorType == DoorType.moveTimer)
            {
                timerToggle = true;
            }
        } 
    }

    public void ToggleObject(bool senderBool)
    {
        if (toggle)
        {
            audioSource.Play();
            if (gameObjectToggle == false)
            {
                gameObjectToggle = senderBool;
            }
            else
                gameObjectToggle = false;
            if (doorType == DoorType.flip)
            {
                gameObject.SetActive(gameObjectToggle);
            }
            else if (doorType == DoorType.moveTimer)
            {
                timerToggle = true;
            }
        }
    }

    public void BoolToogle()
    {
        toggle = !toggle;
    }

    private void ToggleElevator(Vector3 start, Vector3 target, float speed, float acceleration, float time, float tolerance, bool flip)
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
        transform.Translate(motion.SourceToTarget * motion.Velocity * Time.deltaTime);

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

    //private void ToggleElevator(Vector3 source, Vector3 target, float speed, float acceleration, float time, float tolerance)
    //{
    //    Motion motion = new Motion(source, target, 1, 0, 3, tolerance);
    //    transform.Translate(motion.TargetToSource * speed * Time.deltaTime);
    //}
}
