using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{

    public string abilityButton;
    public float maxExtendedDistance;
    public GameObject tounge;
    public GameObject toungeEnd;
    public Transform toungeStart;
    public bool activated = false;
    public GameObject rock;

    private bool extended;
    private float extendToungeTimer = 5f;
    private float HeldInButtonTimer = 0.3f;
    private PlayerController playerController;
    private GameObject toungeTemp;
    private Vector2 direction;
    private float rockCount = 1f;


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

        Vector2 playerPos = toungeStart.position;
        RaycastHit2D hit = Physics2D.Raycast(playerPos, direction, maxExtendedDistance);

        print(playerController.Grounded());

        if (hit.collider != null && playerController.Grounded())
        {

            Vector2 hitPoint = new Vector2(hit.point.x + (0.5f * direction.x), hit.point.y);


            Vector2 middlePoint = (playerPos + hitPoint) / 2;

            toungeTemp = Instantiate(tounge, middlePoint, Quaternion.identity) as GameObject;

            var toungeTempCol = toungeTemp.GetComponent<BoxCollider2D>();
            var toungeTempSprite = toungeTemp.GetComponent<SpriteRenderer>();

            Vector3 size = new Vector3(Vector2.Distance(hitPoint, middlePoint) * 1.5f, toungeTempCol.size.y);

            toungeTempCol.size = size;
            toungeTempSprite.size = size;

            Vector2 spawnPosition = new Vector2(toungeTemp.transform.position.x + 0.075f + ((toungeTempCol.size.x / 2) * direction.x), toungeTemp.transform.position.y);
            GameObject toungeTempEnd = Instantiate(toungeEnd, spawnPosition, Quaternion.Inverse(transform.rotation), toungeTemp.transform) as GameObject;
            toungeTempEnd.transform.localScale = new Vector3(toungeTempEnd.transform.localScale.x * direction.x, toungeTempEnd.transform.localScale.y, toungeTempEnd.transform.localScale.z);

            if (direction == Vector2.left)
                toungeTempEnd.transform.position = new Vector2(toungeTempEnd.transform.position.x - .15f, toungeTempEnd.transform.position.y);

            extended = true;
        }
    }

    void DisableMovementWhenExtended()
    {
        if (extended)
        {
            playerController.enabled = false;
        }
        else if(!extended && playerController.GetPlayerState())
        {
            playerController.enabled = true;
        }
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
            HeldInButtonTimer -= Time.deltaTime;

            if (HeldInButtonTimer < 0 && rockCount == 1)
                ShootRocks();
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

    void ShootRocks()
    {
        //spawn rocks
        GameObject rockTemp = Instantiate(rock, toungeStart.position, transform.rotation) as GameObject;
        rockTemp.GetComponent<Rigidbody2D>().AddForce(direction * 500);
        rockCount--;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject col = other.gameObject;
        if (col.CompareTag("Rock") && rockCount < 1)
        {
            rockCount++;
            Destroy(col);
        }

    }

    public void SwitchActivation(bool state)
    {
        activated = state;
        if (state == false)
            Destroy(toungeTemp);
    }
}
