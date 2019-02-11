using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{

    public string abilityButton;
    public Vector2 direction;
    public float maxDistance;
    public GameObject tounge;


    void Start()
    {

    }

    void Update()
    {
        ExtendTounge();
    }

    void ExtendTounge()
    {
        if (Input.GetButtonDown(abilityButton))
        {
            Vector2 playerPos = transform.position;
            RaycastHit2D hit = Physics2D.Raycast(playerPos, direction, maxDistance);

            if (hit.collider == null)
                return;


            Vector2 hitPoint = hit.point;
            Vector2 middlePoint = (playerPos + hitPoint ) * 0.5f;

            GameObject toungeTemp = Instantiate(tounge, middlePoint, Quaternion.identity) as GameObject;
            toungeTemp.transform.localScale = new Vector3(Vector2.Distance(hitPoint, middlePoint) * 1.8f, toungeTemp.transform.localScale.y);

        }
    }


    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }
}
