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
    private float timer = 5f;
    private RaycastHit2D hit;
    private float jointTimer = 0.3f;

    void Start()
    {
        joint2D = GetComponent<DistanceJoint2D>();
    }

    void Update()
    {
        ButtonHandler();
    }

    void ExtendTounge()
    {
        if (!extended)
        {
            Vector2 playerPos = transform.position;

            if (hit.collider == null)
                return;


            Vector2 hitPoint = hit.point;
            Vector2 middlePoint = (playerPos + hitPoint) * 0.5f;

            GameObject toungeTemp = Instantiate(tounge, middlePoint, Quaternion.identity) as GameObject;
            toungeTemp.transform.localScale = new Vector3(Vector2.Distance(hitPoint, middlePoint) * 1.5f, toungeTemp.transform.localScale.y);
            extended = true;
            Destroy(toungeTemp, 5);
            timer = 5f;

        }
    }


    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    void ButtonHandler()
    {

        if (Input.GetButtonDown(abilityButton))
        {
            joint2D.connectedAnchor = hit.point;
            Vector2 playerPos = transform.position;
            hit = Physics2D.Raycast(playerPos, direction, maxDistance);
        }

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
            ExtendTounge();
        }
        else
        {
            jointTimer = 0.3f;
            joint2D.enabled = false;
        }

        timer -= Time.deltaTime;

        if (timer <= 0)
            extended = false;
    }
}
