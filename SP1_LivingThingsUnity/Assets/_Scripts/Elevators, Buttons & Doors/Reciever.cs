using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Jesper Li 07/02 - 19
public class Reciever : MonoBehaviour
{
    public Animator anim;
    public enum DoorType
    {
        flip = 0, move, moveTimer, 
    }
    [SerializeField]
    DoorType doorType;

    public enum ElevatorState
    {
        target, moving, downTime,
    }
    ElevatorState elevatorState;

    [SerializeField]
    private GameObject camera;

    CameraFollow cameraFollow;

    private bool cameraPan = true;
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
    [SerializeField][Tooltip("changes state of object, if false at start object will be disabled")]
    private bool gameObjectToggle = true;
    [SerializeField][Tooltip("Cant use button if false")]
    private bool lockBool = true;
    [SerializeField][Tooltip("Wont move if false")]
    private bool moveBool;

    private bool componentToggle;

    [Space]
    [Header("Timed Platform Variables")]
    [SerializeField]
    private bool timerToggle;
    [SerializeField]
    private float timer;

    float timerFloat = 0;

    int testInt = 0;

    private float timeD;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        start = transform.position;
        ToggleObjectComponents();
        anim.SetBool("Pressed", !gameObjectToggle);
        camera.SetActive(false);
        cameraFollow = FindObjectOfType<CameraFollow>();
    }

// Update is called once per frame
void Update ()
    {
        if (moveBool && doorType != DoorType.flip)
        {
            //timer += Time.deltaTime;
            ToggleElevator(start, target, speed, acceleration, timeD += Time.deltaTime, tolerance, gameObjectToggle);
        }
        //else
        //    timer = 0;

	}

    public void ToggleObject()
    {
        if (lockBool)
        {
            //gameObjectToggle = !gameObjectToggle;
            timeD = 0;

            if (doorType == DoorType.flip)
            {
                gameObjectToggle = !gameObjectToggle;
                anim.SetBool("Pressed", !gameObjectToggle);
                ToggleObjectComponents();
                GetComponent<ObjectAudioClip>().PlayRandom();
            }

            else if (doorType == DoorType.move)
            {
                
                gameObjectToggle = !gameObjectToggle;
                GetComponent<ObjectAudioClip>().PlayRandom();
            }

            else if (doorType == DoorType.moveTimer)
            {
                //print("test2");
                timerToggle = !timerToggle;
            }
        }
        else
            GetComponent<ObjectAudioClip>().PlaySingle(1);
    }

    public void ToggleObjectComponents()
    {
        if (gameObject.activeInHierarchy)
        {
            GetComponent<BoxCollider2D>().enabled = gameObjectToggle;
            //GetComponent<SpriteRenderer>().enabled = gameObjectToggle;
        }
    }

    public void BoolToogle()
    {
        moveBool = true;
        lockBool = false;
    }


    private void ToggleElevator(Vector3 start, Vector3 target, float speed, float acceleration,
        float time, float tolerance, bool flip)
    {
        Motion motion = new Motion(start, target, speed, acceleration, time, tolerance);

        motion.Source = transform.position;
        //print("current: " + motion.Current);
        //print("valid?: " + motion.Valid);
        //print("test0");
        if (flip)
        {
            motion.Target = target;
            //motion.Source = start;
            
        }
        else
        {
            motion.Target = start;
            //motion.Source = target;

        }
        if (doorType != DoorType.flip)
        {
            //print(motion.InTargetRegion);
            transform.Translate(motion.SourceToTarget * motion.Velocity * Time.deltaTime);
            //if (motion.SourceToTarget.sqrMagnitude >= 0)
            //{
            //    transform.Translate((motion.Source - transform.position) * (speed + acceleration * Time.deltaTime));
            //}
            //else if (motion.SourceToTarget.sqrMagnitude < 0)
            //{
            //    transform.Translate(-(motion.Source - transform.position) * (speed + acceleration * Time.deltaTime));
            //}

            //print("test1");
        }

        if (timerToggle && motion.InTargetRegion)
        {

            if (timerFloat < timer && moveBool)
            {
                if (timerFloat == 0)
                {
                    GetComponent<ObjectAudioClip>().PlaySingle(0);
                }
                timerFloat += Time.deltaTime;
            }
            else if (timerFloat >= timer)
            {
                timeD = 0;
                gameObjectToggle = !gameObjectToggle;
                
                //timerToggle = false;
                timerFloat = 0;
            }
        }

        else if (timerToggle && !motion.InTargetRegion)
        {
            if (!GetComponent<ObjectAudioClip>().audioSource.isPlaying)
            {
                GetComponent<ObjectAudioClip>().PlaySingle(1);
            }
            
        }
        else if (gameObject.tag == "Elevator")
        {
            if (motion.InTargetRegion && elevatorState == ElevatorState.moving)
            {
                elevatorState = ElevatorState.target;
            }
            if (!motion.InTargetRegion)
            {
                elevatorState = ElevatorState.moving;
                if (!GetComponent<ObjectAudioClip>().audioSource.isPlaying)
                {
                    GetComponent<ObjectAudioClip>().PlaySingle(1);
                }
            }
            else if (motion.InTargetRegion && elevatorState == ElevatorState.target)
            {
                GetComponent<ObjectAudioClip>().PlaySingle(0);
                elevatorState = ElevatorState.downTime;
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

    public bool GetDoorActivatable()
    {
        return lockBool;
    }

    public void ObjectCamera()
    {
        if (camera != null && cameraPan)
        {
            StartCoroutine(cameraFollow.SwitchActiveCamera(camera));
            cameraPan = false;
        }
    }
}
