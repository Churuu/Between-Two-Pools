﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{

    public float maxExtendedDistance;
    public float HeldInButtonTimer = 0.3f;
    public string abilityButton;
    public GameObject tounge;
    public GameObject toungeEnd;
    public GameObject rock;
    public Transform toungeStart;
    public LayerMask toungeStickLayer;
    public bool activated = false;

    bool extended;
    float extendToungeTimer = 5f;
    PlayerController playerController;
    GameObject _tounge;
    Vector2 direction;
    float rockCount = 1f;
    Animator anim;


    void Start()
    {
        playerController = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
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
        RaycastHit2D hit = Physics2D.Raycast(playerPos, direction, maxExtendedDistance, toungeStickLayer);

        print(playerController.Grounded());

        if (hit.collider != null && playerController.Grounded())
        {
            anim.SetBool("ShootTounge", true);
            Vector2 hitPoint = new Vector2(hit.point.x + (0.5f * direction.x), hit.point.y);


            Vector2 middlePoint = (playerPos + hitPoint) / 2;

            _tounge = Instantiate(tounge, middlePoint, Quaternion.identity) as GameObject;

            var _toungeCol = _tounge.GetComponent<BoxCollider2D>();
            var _toungeSprite = _tounge.GetComponent<SpriteRenderer>();

            Vector3 size = new Vector3(Vector2.Distance(hitPoint, middlePoint) * 1.5f, _toungeCol.size.y);

            _toungeCol.size = size;
            _toungeSprite.size = size;

            Vector2 spawnPosition = new Vector2(_tounge.transform.position.x + 0.075f + ((_toungeCol.size.x / 2) * direction.x), _tounge.transform.position.y);
            GameObject _toungeEnd = Instantiate(toungeEnd, spawnPosition, Quaternion.Inverse(transform.rotation), _tounge.transform) as GameObject;
            _toungeEnd.transform.localScale = new Vector3(_toungeEnd.transform.localScale.x * direction.x, _toungeEnd.transform.localScale.y, _toungeEnd.transform.localScale.z);

            if (direction == Vector2.left)
                _toungeEnd.transform.position = new Vector2(_toungeEnd.transform.position.x - .15f, _toungeEnd.transform.position.y);

            extended = true;
        }
    }

    void DisableMovementWhenExtended()
    {
        if (extended)
        {
            playerController.enabled = false;
        }
        else if (!extended && playerController.GetPlayerState())
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
            {
                anim.SetBool("ShootRock", true);
                ShootRocks();
            }
        }
        else if (Input.GetButtonUp(abilityButton) && HeldInButtonTimer > 0)
        {
            if (!extended)
            {
                ExtendTounge();
            }
            else
            {
                Destroy(_tounge);
                extended = false;
                anim.SetBool("ShootTounge", false);
            }
        }
        else
        {
            HeldInButtonTimer = 0.2f;
            anim.SetBool("ShootRock", false);
        }
    }

    void ShootRocks()
    {
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
    }

    public void DestroyTounge()
    {
        Destroy(_tounge);
    }
}
