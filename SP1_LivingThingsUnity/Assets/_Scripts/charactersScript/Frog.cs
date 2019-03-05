using System.Collections;
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

    PlayerController playerController;
    bool activated = false;
    bool extended;
    float rockCount = 1f;
    GameObject _tounge;
    Animator anim;
    Vector2 direction =  Vector2.right;


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

    IEnumerator ExtendTounge()
    {

        Vector2 playerPos = toungeStart.position;
        RaycastHit2D hit = Physics2D.Raycast(playerPos, direction, maxExtendedDistance, toungeStickLayer);

        print(playerController.Grounded());

        if (hit.collider != null && playerController.Grounded())
        {
            anim.SetBool("ShootTounge", true);
			playerController.SetPlayerState(false);
            Vector2 hitPoint = new Vector2(hit.point.x + (0.5f * direction.x), hit.point.y);
            extended = true;


            Vector2 middlePoint = (playerPos + hitPoint) / 2;

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length - 0.25f);

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

        }
    }

    void DisableMovementWhenExtended()
    {
        if (extended)
        {
        }
        else if (!extended)
        {
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
                StartCoroutine(ExtendTounge());
            }
            else
            {
                anim.SetBool("ShootTounge", false);
                Invoke("Unextend", anim.GetCurrentAnimatorStateInfo(0).length - .25f);
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

    public bool GetToungeState()
    {
        return extended;
    }

    void Unextend()
    {
        Destroy(_tounge);
        extended = false;
		playerController.SetPlayerState(true);
    }

    public void DestroyTounge()
    {
        Destroy(_tounge);
    }
}
