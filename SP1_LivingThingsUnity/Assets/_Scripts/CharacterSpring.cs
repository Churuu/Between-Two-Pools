using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpring : MonoBehaviour
{

    [Range(50, 100)]
    public float springBoostMultiplier;
    [Range(0, 49)]
    public float springBoostFallout;
    public float BoostResetTimerDelta;
    public float springJumpDistance;

    [SerializeField] private string abilityKey;
    private float boostResetTimer;
    private bool jumped = false;
    private bool shrunken = false;
    private GameObject previousJumpingObject;

    void Start()
    {
        boostResetTimer = BoostResetTimerDelta;
    }

    void Update()
    {
        BoostLandingObject();
        ResetBoostJump();
        Shrink();
    }

    void BoostLandingObject()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.up, springJumpDistance);

        if (hit2D.collider != null && !shrunken)
        {
            if (hit2D.collider.gameObject != previousJumpingObject || previousJumpingObject == null)
            {
                jumped = false;
                previousJumpingObject = hit2D.collider.gameObject;
            }


            if (hit2D.collider.GetComponent<Rigidbody2D>() != null)
            {
                var hitRB = hit2D.collider.GetComponent<Rigidbody2D>();

                if (hitRB.velocity.y < 0)
                {
                    float velocityY = hitRB.velocity.y;
                    hitRB.velocity = new Vector2(hitRB.velocity.x, 0);
                    if (jumped)
                    {
                        print("fallout 76");
                        springBoostMultiplier = springBoostFallout;
                    }

                    print(Mathf.Abs(hitRB.velocity.y) * springBoostMultiplier);

                    hitRB.AddForce(Vector2.up * Mathf.Abs(velocityY) * springBoostMultiplier);
                    boostResetTimer = Time.time + BoostResetTimerDelta;
                    jumped = true;
                    print(jumped);
                }
            }
        }
    }

    void ResetBoostJump()
    {
        if (Time.time > boostResetTimer)
        {
            jumped = false;
            previousJumpingObject = null;
            boostResetTimer = Time.time + BoostResetTimerDelta;
        }
    }

    void Shrink()
    {
        if (Input.GetButtonDown(abilityKey))
            shrunken = !shrunken;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, Vector3.up * springJumpDistance);
    }
}
