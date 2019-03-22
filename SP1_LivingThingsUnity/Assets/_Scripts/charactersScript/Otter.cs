using System.Collections.Generic;
using UnityEngine;
public class Otter : MonoBehaviour
{
    // Jonas Thunberg på börjad 2019-02-05
    //   public enum MomentDerection { Right, Left, Up, Down }
    private enum Ability { Pull, Thrust }
    [SerializeField] private Ability abilityOne = Ability.Pull;
    //  public MomentDerection magnetEnumMomentDerection = MomentDerection.Right;
    private Rigidbody2D rb2D;
    private PlayerController playerController;
    List<Rigidbody2D> magneticBoxRigidBodys = new List<Rigidbody2D>();
    List<Rigidbody2D> magneticWallRigidBodys = new List<Rigidbody2D>();
    public bool magnetPowerActiv = false;
    public bool okToShangeMagnet = false;

    [SerializeField] private float minDistanceBoxStopMoving = 2.35f;
    [SerializeField] private float minDistanceWallStopMoving = 1f;

    [Header("InputKey")]
    [SerializeField] private string buttonNameAbility1AvPå = "Ability1";
    [SerializeField] private string buttonNameAbility1Togel = "S";

    [Header("BoxGetPulle")]
    [SerializeField] private float maxDistanceBoxPulled = 5f; // Maximum range at which the marble will start being pulled to the cup
    [SerializeField] float maxStrengthBoxPulled = 2250f; // Strength with which the marble will be pulled when it is right next to the cup (reduces with distance)
    [SerializeField] bool pulled = false;

    [Header("BoxGetThrust")]
    [SerializeField] private float maxDistanceBoxThrust = 5f; // Maximum range at which the marble will start being thrust from the cup
    [SerializeField] float maxStrengthBoxThrust = 2250f; // Strength with which the marble will be thrust when it is right next to the cup (reduces with distance)
    [SerializeField] bool thrust = false;

    [Header("OtterGetPulledToWall")]
    [SerializeField] private float maxDistanceToGetPulled = 10f;
    [SerializeField] float maxStrengthToGetPulled = 50f;

    [Header("OtterGetThrustFromWall")]
    [SerializeField] private float maxDistanceToGetThrust = 10f;
    [SerializeField] float maxStrengthToGetThrust = 20f;

    [Header("ChildeAnimator 4")]
    public Animator[] animChild; // 1. Normal/Red 2. RedActive 3. Blue 4. BlueActive

    [HideInInspector] public bool pullActiv = true;
    int numer = 0;
    MenuSystem menuSystem;

    [Header("OtterPower")]
    [SerializeField]
    private AudioClip powerOnAudio;
    [SerializeField]
    private AudioClip powerOffAudio;


    //   Animator anim;

    private void Start()
    {
        
           menuSystem = FindObjectOfType<MenuSystem>();
        EventManager.instance.OnStartAddMagneticWall += AddWallMagnetic;
        EventManager.instance.OnStartAddMagneticBox += AddBoxMagnetic;
        //anim = GetComponent<Animator>();
        rb2D = transform.GetComponent<Rigidbody2D>();
        if (transform.GetComponent<PlayerController>() != null)
        {
            playerController = transform.GetComponent<PlayerController>();
        }
        for (int i = 0; i < animChild.Length; i++)
        {
            if (animChild[i] != null)
                animChild[i].SetBool("Pull", true);
        }
    }

    private void AddBoxMagnetic(Rigidbody2D rb2D)
    {
        magneticBoxRigidBodys.Add(rb2D);
    }

    private void AddWallMagnetic(Rigidbody2D rb2D)
    {
        magneticWallRigidBodys.Add(rb2D);
    }

    void Update()
    {

        
        if (!menuSystem.P_Pressed)
        {

            if (magnetPowerActiv)
            {
                for (int i = 0; i < animChild.Length; i++)
                {
                    if (animChild[i] != null)
                        animChild[i].SetBool("PushedQ", true);
                }
            }
            else
            {
                for (int i = 0; i < animChild.Length; i++)
                {
                    if (animChild[i] != null)
                        animChild[i].SetBool("PushedQ", false);
                }
            }



            if (okToShangeMagnet)
            {


                if (Input.GetButtonDown(buttonNameAbility1AvPå))//Av/PÅ
                {
                    
                    magnetPowerActiv = !magnetPowerActiv;
                    if (magnetPowerActiv)
                    {
                        GetComponent<AudioSource>().clip = powerOnAudio;
                        GetComponent<AudioSource>().Play();

                    }
                    else if (!magnetPowerActiv)
                    {
                        GetComponent<AudioSource>().clip = powerOffAudio;
                        GetComponent<AudioSource>().Play();

                    }
                    for (int i = 0; i < animChild.Length; i++)
                    {
                        if (animChild[i] != null)
                            animChild[i].SetBool("AbillityOn", magnetPowerActiv);
                    }



                }
                if (Input.GetButtonDown(buttonNameAbility1Togel)) //Bytter egenskap
                {
                    if (abilityOne == Ability.Pull)
                    {
                        for (int i = 0; i < animChild.Length; i++)
                        {
                            if (animChild[i] != null)
                            {
                                animChild[i].SetBool("Thrust", true);
                                animChild[i].SetBool("Pull", false);
                            }
                        }

                        abilityOne = Ability.Thrust;
                        pullActiv = false;
                    }
                    else if (abilityOne == Ability.Thrust)
                    {
                        abilityOne = Ability.Pull;
                        pullActiv = true;
                        for (int i = 0; i < animChild.Length; i++)
                        {
                            if (animChild[i] != null)
                            {
                                animChild[i].SetBool("Thrust", false);
                                animChild[i].SetBool("Pull", true);
                            }
                        }
                    }

                }
            }


            if (magnetPowerActiv)
            {
                if (abilityOne == Ability.Pull)
                {
                    Update_OtterPulledBox();
                    OtterGetPulledToWall();
                }
                else if (abilityOne == Ability.Thrust)
                {
                    Update_OtterThrustBox();
                    OtterGetThrustFromWall();
                }

            }
        }
    }

    void OtterGetPulledToWall() // Magneten dras till Wall
    {
        for (int i = 0; i < magneticWallRigidBodys.Count; i++)
        {
            if (magneticWallRigidBodys[i] != null && magneticWallRigidBodys[i].gameObject.active)
            {
                float Distance = Vector3.Distance(magneticWallRigidBodys[i].transform.position, this.transform.position);//avståndet mellan Otter och wall

                if (Distance < maxDistanceToGetPulled) // Marble is in range of the magnet
                {
                    float TDistance = Mathf.InverseLerp(maxDistanceToGetPulled, 0f, Distance); // Give a decimal representing how far between 0 distance and max distance.
                    float strength = Mathf.Lerp(0f, maxStrengthToGetPulled, TDistance); // Use that decimal to work out how much strength the magnet should apple
                    Vector3 DirectionToCup = (this.transform.position - magneticWallRigidBodys[i].transform.position).normalized; // Get the direction from the marble to the cup
                    if (Distance < minDistanceWallStopMoving)
                    {
                        rb2D.velocity = new Vector2(0, 0);
                        // magneticRigidBodys[i].velocity = new Vector2(0, magneticRigidBodys[i].velocity.y);
                    }
                    else
                    {
                        rb2D.AddForce(-DirectionToCup * strength * Time.deltaTime, ForceMode2D.Force);// apply force to the marble
                    }


                }
            }
        }
    }

    void OtterGetThrustFromWall() // Magneten trycks ifrån Wall
    {
        for (int i = 0; i < magneticWallRigidBodys.Count; i++)
        {
            if (magneticWallRigidBodys[i] != null && magneticWallRigidBodys[i].gameObject.active)
            {
                float Distance = Vector3.Distance(magneticWallRigidBodys[i].transform.position, this.transform.position);

                if (Distance < maxDistanceToGetThrust) // Marble is in range of the magnet
                {
                    float TDistance = Mathf.InverseLerp(maxDistanceToGetThrust, 0f, Distance); // Give a decimal representing how far between 0 distance and max distance.
                    float strength = Mathf.Lerp(0f, maxStrengthToGetThrust, TDistance); // Use that decimal to work out how much strength the magnet should apple
                    Vector3 DirectionToCup = (this.transform.position - magneticWallRigidBodys[i].transform.position).normalized; // Get the direction from the marble to the cup

                    rb2D.AddForce(DirectionToCup * strength * Time.deltaTime, ForceMode2D.Force);// apply force to the marble
                }
            }
        }
    }

    void Update_OtterPulledBox() // Drar till sig Box
    {

        for (int i = 0; i < magneticBoxRigidBodys.Count; i++)
        {
            if (magneticBoxRigidBodys[i] != null && magneticBoxRigidBodys[i].gameObject.active)
            {
                OtterPulledBox(i);
            }
        }
    }

    void OtterPulledBox(int i)
    {
        float Distance = Vector3.Distance(magneticBoxRigidBodys[i].transform.position, this.transform.position);

        if (Distance < maxDistanceBoxPulled) // Marble is in range of the magnet
        {
            float TDistance = Mathf.InverseLerp(maxDistanceBoxPulled, 0f, Distance); // Give a decimal representing how far between 0 distance and max distance.
            float strength = Mathf.Lerp(0f, maxStrengthBoxPulled, TDistance); // Use that decimal to work out how much strength the magnet should apple
            Vector3 DirectionToCup = (this.transform.position - magneticBoxRigidBodys[i].transform.position).normalized; // Get the direction from the marble to the cup
            if (Distance < minDistanceBoxStopMoving)
            {
           //     Debug.Log("Nu ska den vara stilla");
                magneticBoxRigidBodys[i].velocity = new Vector2(0, 0);
            }
            else
            {
                magneticBoxRigidBodys[i].AddForce(DirectionToCup * strength * Time.deltaTime, ForceMode2D.Force);// apply force to the marble
            }


        }
    }

    void OtterThrustBox(int i)
    {
        float Distance = Vector3.Distance(magneticBoxRigidBodys[i].transform.position, this.transform.position);

        if (Distance < maxDistanceBoxThrust) // Marble is in range of the magnet
        {
            float TDistance = Mathf.InverseLerp(maxDistanceBoxThrust, 0f, Distance); // Give a decimal representing how far between 0 distance and max distance.
            float strength = Mathf.Lerp(0f, maxStrengthBoxThrust, TDistance); // Use that decimal to work out how much strength the magnet should apple
            Vector3 DirectionToCup = (this.transform.position - magneticBoxRigidBodys[i].transform.position).normalized; // Get the direction from the marble to the cup

            magneticBoxRigidBodys[i].AddForce(-DirectionToCup * strength * Time.deltaTime, ForceMode2D.Force);// apply force to the marble

        }
    }

    void Update_OtterThrustBox()
    {
        for (int i = 0; i < magneticBoxRigidBodys.Count; i++)
        {
            if (magneticBoxRigidBodys[i] != null && magneticBoxRigidBodys[i].gameObject.active)
            {
                OtterThrustBox(i);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PlanktonRun"))
        {
            FindObjectOfType<NPCFollower>().RunAway();
        }
    }







}
