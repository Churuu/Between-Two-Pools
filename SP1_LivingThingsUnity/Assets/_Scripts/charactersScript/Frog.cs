using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{

    public string abilityButton;
    public Vector2 direction;
    public float maxDistance;
    public GameObject tounge;
    public LayerMask anchor;

    private DistanceJoint2D joint2D;
    private bool extended;
    private bool activated = true;
    private float extendToungeTimer = 5f;
    private float jointTimer = 0.3f;
    private PlayerController playerController;
    private GameObject toungeTemp;


    void Start()
    {
        joint2D = GetComponent<DistanceJoint2D>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (activated)
        {
            ButtonHandler();
            DisableMovementWhenExtended();
            SetDirection();
        }
    }

    void ExtendTounge()
    {

        Vector2 playerPos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(playerPos, direction, maxDistance);

        if (hit.collider == null)
            return;

        Vector2 hitPoint;

        if (direction == Vector2.left)
            hitPoint = new Vector2(hit.point.x - 0.5f, hit.point.y);
        else
            hitPoint = new Vector2(hit.point.x + 0.5f, hit.point.y);

        Vector2 middlePoint = (playerPos + hitPoint) / 2;

        toungeTemp = Instantiate(tounge, middlePoint, Quaternion.identity) as GameObject;

        var toungeTempCol = toungeTemp.GetComponent<BoxCollider2D>();
        var toungeTempSprite = toungeTemp.GetComponent<SpriteRenderer>();

        Vector3 size = new Vector3(Vector2.Distance(hitPoint, middlePoint) * 1.5f, toungeTempCol.size.y);

        toungeTempCol.size = size;
        toungeTempSprite.size = size;

        extended = true;

    }

    void DisableMovementWhenExtended()
    {
        if (extended)
            playerController.enabled = false;
        else
            playerController.enabled = true;
    }


    void SetDirection()
    {
        if (playerController.GetMoveDirection() != Vector2.zero)
            direction = playerController.GetMoveDirection();
    }

    void ButtonHandler()
    {

        RaycastHit2D anchorCast = Physics2D.CircleCast(transform.position, 5, direction, 5, anchor);

        if (Input.GetButton(abilityButton))
        {
            jointTimer -= Time.deltaTime;
            print(anchorCast.collider.name);

            if (jointTimer <= 0 && anchorCast.collider != null)
            {
                joint2D.connectedAnchor = anchorCast.collider.transform.position;
                joint2D.enabled = true;
            }
        }
        else if (Input.GetButtonUp(abilityButton) && jointTimer > 0)
        {
            if (!extended)
            {
                ExtendTounge();
            }
            else
            {
                Destroy(toungeTemp);
                extended = false;
            }
        }
        else
        {
            jointTimer = 0.3f;
            joint2D.enabled = false;
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5);
    }

    public void SwitchActivation(bool state)
    {
        activated = state;
    }
}
