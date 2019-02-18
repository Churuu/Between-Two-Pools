using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{

    public string abilityButton;
    public float maxExtendedDistance;
    public GameObject tounge;

    private bool extended;
    private bool activated = true;
    private float extendToungeTimer = 5f;
    private float HeldInButtonTimer = 0.3f;
    private PlayerController playerController;
    private GameObject toungeTemp;
    private Vector2 direction;


    void Start()
    {
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
        RaycastHit2D hit = Physics2D.Raycast(playerPos, direction, maxExtendedDistance);

        if (hit.collider != null)
        {
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
            //Destroy walls with rocks
        }
        else if (Input.GetButtonUp(abilityButton) && HeldInButtonTimer > 0)
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
            HeldInButtonTimer = 0.2f;
        }

    }

    public void SwitchActivation(bool state)
    {
        activated = state;
    }
}
