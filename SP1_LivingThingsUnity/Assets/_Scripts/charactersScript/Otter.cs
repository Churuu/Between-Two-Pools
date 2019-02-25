using UnityEngine;

public class Otter : MonoBehaviour
{
    // Jonas Thunberg på börjad 2019-02-05
    public enum MomentDerection { Right, Left, Up, Down }
    private enum Ability { Pull, Thrust }
    [SerializeField] private Ability abilityOne = Ability.Thrust;
    public MomentDerection magnetEnumMomentDerection = MomentDerection.Right;
    private Rigidbody2D rb2D;
    private PlayerController playerController;
    [SerializeField] Rigidbody2D[] magneticRigidBodys;
    [SerializeField] Rigidbody2D[] magneticRigidBodysMagnetToObject;
    public bool magnetPowerActiv = false;
    [SerializeField] private float MaxDistancePulled = 10f; // Maximum range at which the marble will start being pulled to the cup
    [SerializeField] float MaxStrengthPulled = 20f; // Strength with which the marble will be pulled when it is right next to the cup (reduces with distance)
    [SerializeField] bool pulled = false;
    [SerializeField] private float MaxDistanceThrust = 10f; // Maximum range at which the marble will start being thrust from the cup
    [SerializeField] float MaxStrengthThrust = 20f; // Strength with which the marble will be thrust when it is right next to the cup (reduces with distance)
    [SerializeField] bool thrust = false;
    [SerializeField] private string buttonNameAbility1AvPå = "Ability1";
    [SerializeField] private string buttonNameAbility1Togel = "S";
    [SerializeField] private string buttonNameAbility2 = "Ability2";
    [SerializeField] private bool ability2Activ = false;
    [SerializeField] private float minDistanceStopMoving = 0.9f;
    [SerializeField] private float maxDistanceToGetPulled = 10f;
    [SerializeField] float maxStrengthToGetPulled = 50f;
    [SerializeField] private float maxDistanceToGetThrust = 10f;
    [SerializeField] float maxStrengthToGetThrust = 20f;
    int numer = 0;
    public bool okToShangeMagnet = false;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = transform.GetComponent<Rigidbody2D>();
        if (transform.GetComponent<PlayerController>() != null)
        {
            playerController = transform.GetComponent<PlayerController>();
        }

    }


    void Update()
    {
        if (!playerController.Grounded())
        {
            anim.SetBool("Floating", true);
        }
        else { anim.SetBool("Floating", false); }
        if (abilityOne == Ability.Pull)
        {
            Debug.Log("pull(O_O)");
            anim.SetBool("PullActive", true);
            anim.SetBool("PushActive", false);
        }
        else if (abilityOne == Ability.Thrust)
        {
            Debug.Log("Thrust(O_O)");
            anim.SetBool("PushActive", true);
            anim.SetBool("PullActive", false);
        }

        if (magnetPowerActiv || ability2Activ)
        {
            anim.SetBool("PushedQ", true);
        }
        else
        {
            anim.SetBool("PushedQ", false);
        }


        if (!magnetPowerActiv)
        {
            if (rb2D.velocity.x < 0)
            {
                magnetEnumMomentDerection = MomentDerection.Left;
            }
            else if (rb2D.velocity.x > 0)
            {
                magnetEnumMomentDerection = MomentDerection.Right;
            }
        }
        if (okToShangeMagnet)
        {
            if (Input.GetButtonDown(buttonNameAbility2))//TODO
            {
                ability2Activ = !ability2Activ;
                Debug.Log("Ability2 active");

            }

            if (Input.GetButtonDown(buttonNameAbility1AvPå))//TODO
            {
                magnetPowerActiv = !magnetPowerActiv;



            }
            if (Input.GetButtonDown(buttonNameAbility1Togel))
            {
                if (abilityOne == Ability.Pull)
                {
                    abilityOne = Ability.Thrust;
                }
                else if (abilityOne == Ability.Thrust)
                {

                    abilityOne = Ability.Pull;
                }

            }
        }
        if (ability2Activ)
        {
            if (abilityOne == Ability.Pull)
            {
                MagnetGetPulled();
            }
            else if (abilityOne == Ability.Thrust)
            {
                MagnetGetThrust();
            }
        }
        if (magnetPowerActiv)
        {
            if (abilityOne == Ability.Pull)
            {
                Debug.Log("pull");
                //anim.SetBool("PullActive", true);
                //anim.SetBool("PushActive", false);
                Update_MagnetismPulled();
            }
            else if (abilityOne == Ability.Thrust)
            {
                // anim.SetBool("PullActive", false);
                // anim.SetBool("PushActive", true);
                Update_MagnetismThrust();
            }

        }



        //  Update_CupDirection();
    }

    void Update_MagnetismPulled()
    {

        for (int i = 0; i < magneticRigidBodys.Length; i++)
        {
            if (magneticRigidBodys[i] != null && magneticRigidBodys[i].gameObject.active)
            {
                if (magnetEnumMomentDerection == MomentDerection.Left)
                {
                    if (magneticRigidBodys[i].transform.position.x < this.transform.position.x)
                    {
                        MagnetismPulled(i);
                    }
                }
                else if (magnetEnumMomentDerection == MomentDerection.Right)
                {
                    if (magneticRigidBodys[i].transform.position.x > this.transform.position.x)
                    {
                        MagnetismPulled(i);
                    }
                }
            }
        }
    }


    void MagnetGetPulled()
    {
        for (int i = 0; i < magneticRigidBodysMagnetToObject.Length; i++)
        {
            if (magneticRigidBodysMagnetToObject[i] != null && magneticRigidBodysMagnetToObject[i].gameObject.active)
            {
                float Distance = Vector3.Distance(magneticRigidBodysMagnetToObject[i].transform.position, this.transform.position);

                if (Distance < maxDistanceToGetPulled) // Marble is in range of the magnet
                {
                    float TDistance = Mathf.InverseLerp(maxDistanceToGetPulled, 0f, Distance); // Give a decimal representing how far between 0 distance and max distance.
                    float strength = Mathf.Lerp(0f, maxStrengthToGetPulled, TDistance); // Use that decimal to work out how much strength the magnet should apple
                    Vector3 DirectionToCup = (this.transform.position - magneticRigidBodysMagnetToObject[i].transform.position).normalized; // Get the direction from the marble to the cup
                    if (Distance < minDistanceStopMoving)
                    {
                        rb2D.velocity = new Vector2(0, 0);
                        // magneticRigidBodys[i].velocity = new Vector2(0, magneticRigidBodys[i].velocity.y);
                    }
                    else
                    {
                        rb2D.AddForce(-DirectionToCup * strength, ForceMode2D.Force);// apply force to the marble
                    }


                }
            }
        }
    }

    void MagnetGetThrust()
    {
        for (int i = 0; i < magneticRigidBodysMagnetToObject.Length; i++)
        {
            if (magneticRigidBodysMagnetToObject[i] != null && magneticRigidBodysMagnetToObject[i].gameObject.active)
            {
                float Distance = Vector3.Distance(magneticRigidBodysMagnetToObject[i].transform.position, this.transform.position);

                if (Distance < maxDistanceToGetThrust) // Marble is in range of the magnet
                {
                    float TDistance = Mathf.InverseLerp(maxDistanceToGetThrust, 0f, Distance); // Give a decimal representing how far between 0 distance and max distance.
                    float strength = Mathf.Lerp(0f, maxStrengthToGetThrust, TDistance); // Use that decimal to work out how much strength the magnet should apple
                    Vector3 DirectionToCup = (this.transform.position - magneticRigidBodysMagnetToObject[i].transform.position).normalized; // Get the direction from the marble to the cup
                                                                                                                                            //if (Distance < minDistanceStopMoving)
                                                                                                                                            //{
                                                                                                                                            //    rb2D.velocity = new Vector2(0, 0);
                                                                                                                                            //    // magneticRigidBodys[i].velocity = new Vector2(0, magneticRigidBodys[i].velocity.y);
                                                                                                                                            //}
                                                                                                                                            //else
                                                                                                                                            //{
                    rb2D.AddForce(DirectionToCup * strength, ForceMode2D.Force);// apply force to the marble
                    //}


                }
            }
        }
    }



    void MagnetismPulled(int i)
    {
        float Distance = Vector3.Distance(magneticRigidBodys[i].transform.position, this.transform.position);

        if (Distance < MaxDistancePulled) // Marble is in range of the magnet
        {
            float TDistance = Mathf.InverseLerp(MaxDistancePulled, 0f, Distance); // Give a decimal representing how far between 0 distance and max distance.
            float strength = Mathf.Lerp(0f, MaxStrengthPulled, TDistance); // Use that decimal to work out how much strength the magnet should apple
            Vector3 DirectionToCup = (this.transform.position - magneticRigidBodys[i].transform.position).normalized; // Get the direction from the marble to the cup
            if (Distance < minDistanceStopMoving)
            {
                magneticRigidBodys[i].velocity = new Vector2(0, 0);
                // magneticRigidBodys[i].velocity = new Vector2(0, magneticRigidBodys[i].velocity.y);
            }
            else
            {
                magneticRigidBodys[i].AddForce(DirectionToCup * strength, ForceMode2D.Force);// apply force to the marble
            }


        }
    }



    void Update_MagnetismThrust()
    {
        for (int i = 0; i < magneticRigidBodys.Length; i++)
        {
            if (magneticRigidBodys[i] != null && magneticRigidBodys[i].gameObject.active)
            {
                if (magnetEnumMomentDerection == MomentDerection.Left)
                {
                    if (magneticRigidBodys[i].transform.position.x < this.transform.position.x)
                    {
                        MagnetismThrust(i);
                    }
                }
                else if (magnetEnumMomentDerection == MomentDerection.Right)
                {
                    if (magneticRigidBodys[i].transform.position.x > this.transform.position.x)
                    {
                        MagnetismThrust(i);
                    }
                }
            }
        }
    }
    void MagnetismThrust(int i)
    {
        float Distance = Vector3.Distance(magneticRigidBodys[i].transform.position, this.transform.position);

        if (Distance < MaxDistanceThrust) // Marble is in range of the magnet
        {
            float TDistance = Mathf.InverseLerp(MaxDistanceThrust, 0f, Distance); // Give a decimal representing how far between 0 distance and max distance.
            float strength = Mathf.Lerp(0f, MaxStrengthThrust, TDistance); // Use that decimal to work out how much strength the magnet should apple
            Vector3 DirectionToCup = (this.transform.position - magneticRigidBodys[i].transform.position).normalized; // Get the direction from the marble to the cup

            magneticRigidBodys[i].AddForce(-DirectionToCup * strength, ForceMode2D.Force);// apply force to the marble

        }
    }




    //void Update_CupDirection()
    //{
    //    Vector3 DirectionToMarble = (MarbleRigidBody.transform.position - this.transform.position).normalized; // Direction from the cup to the marble
    //    this.transform.forward = DirectionToMarble; // Make the cap face that direction
    //}



}
