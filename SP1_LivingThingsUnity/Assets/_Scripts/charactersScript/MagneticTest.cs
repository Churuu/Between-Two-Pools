using UnityEngine;

public class MagneticTest : MonoBehaviour
{
    // Jonas Thunberg på börjad 2019-02-05
    public enum MomentDerection { Right, Left, Up, Down }
    public MomentDerection magnetEnumMomentDerection = MomentDerection.Right;
    private Rigidbody2D rb2D;
    [SerializeField] Rigidbody2D[] magneticRigidBodys;
    [SerializeField] Rigidbody2D[] magneticRigidBodysMagnetToObject;
    public bool magnetPowerActiv = false;
    [SerializeField] private float MaxDistancePulled = 10f; // Maximum range at which the marble will start being pulled to the cup
    [SerializeField] float MaxStrengthPulled = 20f; // Strength with which the marble will be pulled when it is right next to the cup (reduces with distance)
    [SerializeField] bool pulled = false;
    [SerializeField] private float MaxDistanceThrust = 10f; // Maximum range at which the marble will start being thrust from the cup
    [SerializeField] float MaxStrengthThrust = 20f; // Strength with which the marble will be thrust when it is right next to the cup (reduces with distance)
    [SerializeField] bool thrust = false;
    [SerializeField] private string buttonNameAbility1 = "Ability1";
    [SerializeField] private string buttonNameAbility2 = "Ability2";
    [SerializeField] private bool ability2Activ = false;
    [SerializeField] private float minDistanceStopMoving = 0.9f;
    int numer = 0;
    public bool okToShangeMagnet = false;
    private void Start()
    {
        rb2D = transform.GetComponent<Rigidbody2D>();
    }


    void Update()
    {
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

            if (Input.GetButtonDown(buttonNameAbility1))//TODO
            {
                numer++;
                if (numer > 2)
                {
                    numer = 0;
                }
                if (numer == 1)
                {
                    magnetPowerActiv = true;
                    thrust = false;
                    pulled = true;

                }
                else if (numer == 2)
                {
                    magnetPowerActiv = true;
                    pulled = false;
                    thrust = true;
                }
                else if (numer == 0)
                {
                    magnetPowerActiv = false;
                    pulled = false;
                    thrust = false;
                }

            }
        }
        if (ability2Activ)
        {
            MagnetGetPulled();
        }
        if (pulled)
        {
            Update_MagnetismPulled();
        }
        else if (thrust)
        {
            Update_MagnetismThrust();
        }
        else
        {

        }

        //  Update_CupDirection();
    }

    void Update_MagnetismPulled()
    {

        for (int i = 0; i < magneticRigidBodys.Length; i++)
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


    void MagnetGetPulled()
    {
        for (int i = 0; i < magneticRigidBodysMagnetToObject.Length; i++)
        {
            float Distance = Vector3.Distance(magneticRigidBodysMagnetToObject[i].transform.position, this.transform.position);

            if (Distance < MaxDistancePulled) // Marble is in range of the magnet
            {
                float TDistance = Mathf.InverseLerp(MaxDistancePulled, 0f, Distance); // Give a decimal representing how far between 0 distance and max distance.
                float strength = Mathf.Lerp(0f, MaxStrengthPulled, TDistance); // Use that decimal to work out how much strength the magnet should apple
                Vector3 DirectionToCup = (this.transform.position - magneticRigidBodysMagnetToObject[i].transform.position).normalized; // Get the direction from the marble to the cup
                if (Distance < minDistanceStopMoving)
                {
                    rb2D.velocity = new Vector2(0, 0);
                    // magneticRigidBodys[i].velocity = new Vector2(0, magneticRigidBodys[i].velocity.y);
                }
                else
                {
                   
                }
 rb2D.AddForce(-DirectionToCup * strength, ForceMode2D.Force);// apply force to the marble

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
