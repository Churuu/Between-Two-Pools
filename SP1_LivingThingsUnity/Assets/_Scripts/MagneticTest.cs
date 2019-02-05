using UnityEngine;

public class MagneticTest : MonoBehaviour
{
    // Jonas Thunberg 2019-02-05

    [SerializeField] Rigidbody2D[] magneticRigidBodys;

    [SerializeField] private float MaxDistancePulled = 10f; // Maximum range at which the marble will start being pulled to the cup
    [SerializeField] float MaxStrengthPulled = 100f; // Strength with which the marble will be pulled when it is right next to the cup (reduces with distance)
    [SerializeField] bool pulled = false;
    [SerializeField] private float MaxDistanceThrust = 10f; // Maximum range at which the marble will start being thrust from the cup
    [SerializeField] float MaxStrengthThrust = 100f; // Strength with which the marble will be thrust when it is right next to the cup (reduces with distance)
    [SerializeField] bool thrust = false;
    [SerializeField] private string buttonName = "Ability";
    int numer = 0;
    public bool okToShangeMagnet = false;
    void Update()
    {
        if (okToShangeMagnet)
        {
            if (Input.GetButtonDown(buttonName))//TODO
            {
                numer++;
                if (numer > 2)
                {
                    numer = 0;
                }
                if (numer == 0)
                {

                    thrust = false;
                    pulled = true;

                }
                else if (numer == 1)
                {

                    pulled = false;
                    thrust = true;
                }
                else if (numer == 2)
                {

                    pulled = false;
                    thrust = false;
                }

            }
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
            float Distance = Vector3.Distance(magneticRigidBodys[i].transform.position, this.transform.position);

            if (Distance < MaxDistancePulled) // Marble is in range of the magnet
            {
                float TDistance = Mathf.InverseLerp(MaxDistancePulled, 0f, Distance); // Give a decimal representing how far between 0 distance and max distance.
                float strength = Mathf.Lerp(0f, MaxStrengthPulled, TDistance); // Use that decimal to work out how much strength the magnet should apple
                Vector3 DirectionToCup = (this.transform.position - magneticRigidBodys[i].transform.position).normalized; // Get the direction from the marble to the cup

                magneticRigidBodys[i].AddForce(DirectionToCup * strength, ForceMode2D.Force);// apply force to the marble

            }
        }
    }
    void Update_MagnetismThrust()
    {
        for (int i = 0; i < magneticRigidBodys.Length; i++)
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
    }

    //void Update_CupDirection()
    //{
    //    Vector3 DirectionToMarble = (MarbleRigidBody.transform.position - this.transform.position).normalized; // Direction from the cup to the marble
    //    this.transform.forward = DirectionToMarble; // Make the cap face that direction
    //}



}
