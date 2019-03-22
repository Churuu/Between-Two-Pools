using System.Collections;
using UnityEngine;

public class Frog : MonoBehaviour
{

    public float maxExtendedDistance;
    [Range(0, 1)]
    public int rockCount = 0;
    public string extendToungeButton, shootRockButton;
    public GameObject tounge;
    public GameObject toungeEnd;
    public GameObject rock;
    public Transform toungeStart;
    public LayerMask toungeStickLayer;

    public AnimationClip extendClip, retractClip;

    PlayerController playerController;
    bool activated = false;
    bool extended;
    //  bool canRetract = false;

    GameObject _tounge;
    [SerializeField]
    Animator animFrog;
    [SerializeField]
    Animator animRockFrog;
    Vector2 direction = Vector2.right;


    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (activated)
        {
            ButtonHandler();
            SetDirection();
        }

        if (!extended && _tounge != null)
        {
            Destroy(_tounge);
            if (GameObject.FindGameObjectsWithTag("FrogTounge") != null)
            {
                GameObject[] games = GameObject.FindGameObjectsWithTag("FrogTounge");
                for (int i = 0; i < games.Length; i++)
                {
                    Destroy(games[i]);
                }
            }
        }

        //playerController.SetPlayerState(!extended);
    }

    void ButtonHandler()
    {
        if (Input.GetButtonDown(shootRockButton))
        {
            animFrog.SetBool("ShootRock", false);
            animRockFrog.SetBool("ShootRock", false);
            if (rockCount == 1)
            {
                animFrog.SetBool("ShootRock", true);
                animRockFrog.SetBool("ShootRock", true);
                ShootRocks();
            }
        }
        else if (Input.GetButtonDown(extendToungeButton))
        {
            if (!extended)//&& !canRetract)
            {
                extended = true;
                StartCoroutine(ExtendTounge());
            }
            else if (extended)//&& canRetract)
            {
                animFrog.SetBool("ShootTounge", false);
                animRockFrog.SetBool("ShootTounge", false);
                Invoke("Unextend", retractClip.length / 2);
            }
        }
        else
        {
            animFrog.SetBool("ShootRock", false);
            animRockFrog.SetBool("ShootRock", false);
        }
    }

    IEnumerator ExtendTounge()
    {

        Vector2 playerPos = toungeStart.position;
        RaycastHit2D hit = Physics2D.Raycast(playerPos, direction, maxExtendedDistance, toungeStickLayer);

        print(playerController.Grounded());

        if (hit.collider != null && playerController.Grounded())
        {
            animFrog.SetBool("ShootTounge", true);
            animRockFrog.SetBool("ShootTounge", true);
            playerController.SetPlayerState(false);

            Vector2 hitPoint = new Vector2(hit.point.x + (0.5f * direction.x), hit.point.y);
            Vector2 middlePoint = (playerPos + hitPoint) / 2;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            yield return new WaitForSeconds(extendClip.length);

            _tounge = Instantiate(tounge, middlePoint, Quaternion.identity);



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
            //  canRetract = true;

        }
    }

    void SetDirection()
    {
        if (playerController.GetMoveDirection() != Vector2.zero)
            direction = playerController.GetMoveDirection();
    }

    void ShootRocks()
    {
        GameObject rockTemp = Instantiate(rock, toungeStart.position, transform.rotation) as GameObject;
        rockTemp.GetComponent<Rigidbody2D>().AddForce(direction * 500);
        rockCount--;
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
        //  canRetract = false;
        playerController.SetPlayerState(true);
    }

    public void DestroyTounge()
    {
        Destroy(_tounge);
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
}
