//Skapad  av Robin Nechovski 07-02-2019

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [HideInInspector] public Vector2 direction;

    [SerializeField] private float destructionDistance;
    [SerializeField] private LayerMask destructableWall;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        DestroyWall();
        //Debug.DrawRay(transform.position);
    }

    void DestroyWall()
    {
        if (rb.velocity.x != 0)
            SetDestructionDirection(new Vector2(rb.velocity.x, 0));

        if (Input.GetButtonDown("Ability"))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, destructionDistance, destructableWall);

            if (hit.collider == null)
                return;

            Destroy(hit.collider.gameObject);
        }
    }

    public void SetDestructionDirection(Vector2 direction)
    {
        this.direction = direction;
    }
}
