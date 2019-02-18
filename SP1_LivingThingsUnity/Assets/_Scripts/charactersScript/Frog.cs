using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{

    public string abilityButton;
    public Vector2 direction;
    public float maxDistance;
    public GameObject tounge;

    private DistanceJoint2D joint2D;
    private bool extended;
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
        ButtonHandler();
        DisableMovementWhenExtended();
        SetDirection();
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
        if (Input.GetButton(abilityButton))
        {
            jointTimer -= Time.deltaTime;

            if (jointTimer <= 0)
            {
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

        if (Input.GetButtonDown(abilityButton))
        {
            RaycastHit2D anchorCast = Physics2D.CircleCast(transform.position, 5, Vector2.right, 5);
            joint2D.connectedAnchor = anchorCast.collider.CompareTag("AnchorPoint") ? anchorCast.collider.transform.position : transform.position;
        }

    }
}
