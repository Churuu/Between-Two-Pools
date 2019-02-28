using System.Collections.Generic;
using UnityEngine;
public class Otter : MonoBehaviour
{
    // Jonas Thunberg på börjad 2019-02-05
    //   public enum MomentDerection { Right, Left, Up, Down }
    private enum Ability { Pull, Thrust }
    [SerializeField] private Ability abilityOne = Ability.Thrust;
    //  public MomentDerection magnetEnumMomentDerection = MomentDerection.Right;
    private Rigidbody2D rb2D;
    private PlayerController playerController;
    List<Rigidbody2D> magneticBoxRigidBodys = new List<Rigidbody2D>();
    List<Rigidbody2D> magneticWallRigidBodys = new List<Rigidbody2D>();
    public bool magnetPowerActiv = false;
    [SerializeField] private float MaxDistancePulled = 10f; // Maximum range at which the marble will start being pulled to the cup
    [SerializeField] float MaxStrengthPulled = 20f; // Strength with which the marble will be pulled when it is right next to the cup (reduces with distance)
    [SerializeField] bool pulled = false;
    [SerializeField] private float MaxDistanceThrust = 10f; // Maximum range at which the marble will start being thrust from the cup
    [SerializeField] float MaxStrengthThrust = 20f; // Strength with which the marble will be thrust when it is right next to the cup (reduces with distance)
    [SerializeField] bool thrust = false;
    [SerializeField] private string buttonNameAbility1AvPå = "Ability1";
    [SerializeField] private string buttonNameAbility1Togel = "S";
    [SerializeField] private float maxDistanceToGetPulled = 10f;
    [SerializeField] float maxStrengthToGetPulled = 50f;
    [SerializeField] private float maxDistanceToGetThrust = 10f;
    [SerializeField] float maxStrengthToGetThrust = 20f;
    [SerializeField] private float minDistanceStopMoving = 2.35f;
    int numer = 0;
    public bool okToShangeMagnet = false;
    Animator anim;

    private void Start()
    {
        EventManager.instance.OnStartAddMagneticWall += AddWallMagnetic;
        EventManager.instance.OnStartAddMagneticBox += AddBoxMagnetic;
        anim = GetComponent<Animator>();
        rb2D = transform.GetComponent<Rigidbody2D>();
        if (transform.GetComponent<PlayerController>() != null)
        {
            playerController = transform.GetComponent<PlayerController>();
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
        //if (!playerController.Grounded())
        //{
        //    anim.SetBool("Floating", true);
        //}
        //else { anim.SetBool("Floating", false); }
        //if (abilityOne == Ability.Pull)
        //{
        //    Debug.Log("pull(O_O)");
        //    anim.SetBool("PullActive", true);
        //    anim.SetBool("PushActive", false);
        //}
        //else if (abilityOne == Ability.Thrust)
        //{
        //    //   Debug.Log("Thrust(O_O)");
        //    anim.SetBool("PushActive", true);
        //    anim.SetBool("PullActive", false);
        //}

        if (magnetPowerActiv)
        {
            anim.SetBool("PushedQ", true);
        }
        else
        {
            anim.SetBool("PushedQ", false);
        }



        if (okToShangeMagnet)
        {


            if (Input.GetButtonDown(buttonNameAbility1AvPå))//Av/PÅ
            {
                magnetPowerActiv = !magnetPowerActiv;



            }
            if (Input.GetButtonDown(buttonNameAbility1Togel)) //Bytter egenskap
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

        if (magnetPowerActiv)
        {
            if (abilityOne == Ability.Pull)
            {
                Debug.Log("pull");
                //anim.SetBool("PullActive", true);
                //anim.SetBool("PushActive", false);
                Update_MagnetismPulled();
                MagnetGetPulled();
            }
            else if (abilityOne == Ability.Thrust)
            {
                // anim.SetBool("PullActive", false);
                // anim.SetBool("PushActive", true);
                Update_MagnetismThrust();
                MagnetGetThrust();
            }

        }




    }




    void MagnetGetPulled() // Magneten dras till Wall
    {
        for (int i = 0; i < magneticWallRigidBodys.Count; i++)
        {
            if (magneticWallRigidBodys[i] != null && magneticWallRigidBodys[i].gameObject.active)
            {
                float Distance = Vector3.Distance(magneticWallRigidBodys[i].transform.position, this.transform.position);

                if (Distance < maxDistanceToGetPulled) // Marble is in range of the magnet
                {
                    float TDistance = Mathf.InverseLerp(maxDistanceToGetPulled, 0f, Distance); // Give a decimal representing how far between 0 distance and max distance.
                    float strength = Mathf.Lerp(0f, maxStrengthToGetPulled, TDistance); // Use that decimal to work out how much strength the magnet should apple
                    Vector3 DirectionToCup = (this.transform.position - magneticWallRigidBodys[i].transform.position).normalized; // Get the direction from the marble to the cup
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

    void MagnetGetThrust() // Magneten trycks ifrån Wall
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

                    rb2D.AddForce(DirectionToCup * strength, ForceMode2D.Force);// apply force to the marble
                }
            }
        }
    }
    void Update_MagnetismPulled() // Drar till sog Box
    {

        for (int i = 0; i < magneticBoxRigidBodys.Count; i++)
        {
            if (magneticBoxRigidBodys[i] != null && magneticBoxRigidBodys[i].gameObject.active)
            {
                MagnetismPulled(i);
            }
        }
    }


    void MagnetismPulled(int i)
    {
        float Distance = Vector3.Distance(magneticBoxRigidBodys[i].transform.position, this.transform.position);

        if (Distance < MaxDistancePulled) // Marble is in range of the magnet
        {
            float TDistance = Mathf.InverseLerp(MaxDistancePulled, 0f, Distance); // Give a decimal representing how far between 0 distance and max distance.
            float strength = Mathf.Lerp(0f, MaxStrengthPulled, TDistance); // Use that decimal to work out how much strength the magnet should apple
            Vector3 DirectionToCup = (this.transform.position - magneticBoxRigidBodys[i].transform.position).normalized; // Get the direction from the marble to the cup
            if (Distance < minDistanceStopMoving)
            {
                Debug.Log("Nu ska den vara stilla");
                magneticBoxRigidBodys[i].velocity = new Vector2(0, 0);
            }
            else
            {
                magneticBoxRigidBodys[i].AddForce(DirectionToCup * strength, ForceMode2D.Force);// apply force to the marble
            }


        }
    }



    void Update_MagnetismThrust()
    {
        for (int i = 0; i < magneticBoxRigidBodys.Count; i++)
        {
            if (magneticBoxRigidBodys[i] != null && magneticBoxRigidBodys[i].gameObject.active)
            {
                MagnetismThrust(i);
            }
        }
    }
    void MagnetismThrust(int i)
    {
        float Distance = Vector3.Distance(magneticBoxRigidBodys[i].transform.position, this.transform.position);

        if (Distance < MaxDistanceThrust) // Marble is in range of the magnet
        {
            float TDistance = Mathf.InverseLerp(MaxDistanceThrust, 0f, Distance); // Give a decimal representing how far between 0 distance and max distance.
            float strength = Mathf.Lerp(0f, MaxStrengthThrust, TDistance); // Use that decimal to work out how much strength the magnet should apple
            Vector3 DirectionToCup = (this.transform.position - magneticBoxRigidBodys[i].transform.position).normalized; // Get the direction from the marble to the cup

            magneticBoxRigidBodys[i].AddForce(-DirectionToCup * strength, ForceMode2D.Force);// apply force to the marble

        }
    }






}
