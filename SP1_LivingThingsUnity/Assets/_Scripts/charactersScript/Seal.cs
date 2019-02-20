//Skapad av Robin Nechovski 07-02-2019
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seal : MonoBehaviour
{

    public float springBoostMultiplier = 75f;
    public float springBoostFallout = 25f;
    public float BoostResetTimerDelta = 5f;
    public string bounceAnimParam = "Bounce";

    private float boostResetTimer;
    private bool jumped = false;
    private bool shrunken = false;
    private PlayerController playerController;
    private GameObject previousJumpingObject;
    Animator anim;

    void Start()
    {
        boostResetTimer = BoostResetTimerDelta;
        anim = transform.parent.GetComponent<Animator>();
    }

    void Update()
    {
        ResetBoostJump();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        BoostLandingObject(col.gameObject);
    }

    void BoostLandingObject(GameObject gObj)
    {
        if (gObj != previousJumpingObject || previousJumpingObject == null)
        {
            jumped = false;
            previousJumpingObject = gObj;
            // anim.ResetTrigger(bounceAnimParam);
        }

        var hitRB = gObj.GetComponent<Rigidbody2D>();

        if (hitRB.velocity.y < 0)
        {
            float velocityY = hitRB.velocity.y;
            hitRB.velocity = new Vector2(hitRB.velocity.x, 0);

            playerController = gObj.GetComponent<PlayerController>();
            playerController.SealJump();
            hitRB.AddForce(Vector2.up * (jumped ? springBoostFallout : springBoostMultiplier));

            boostResetTimer = Time.time + BoostResetTimerDelta;

            jumped = true;

            anim.SetTrigger(bounceAnimParam);

        }

    }

    void ResetBoostJump()
    {
        if (Time.time > boostResetTimer)
        {
            jumped = false;
            previousJumpingObject = null;
            boostResetTimer = Time.time + BoostResetTimerDelta;
            //anim.ResetTrigger(bounceAnimParam);
        }
    }
}
