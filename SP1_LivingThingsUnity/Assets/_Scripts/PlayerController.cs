using UnityEngine;

public class PlayerController : MonoBehaviour
{//Påbörjad av Jonas Thunberg 2019-01-31
 //redigering 2019-02-11
    private Rigidbody2D rb2D;
    private Collider2D coll2D;

    [SerializeField] private float materialFrictiom = 0;
    private int materialFrictiomZero = 0;

    [SerializeField] private string horizontalMoment = "Horizontal";
    [SerializeField] private string jumpAxis = "wJump";

    [SerializeField] private float speedVelocityHorizontal = 7f;
    [SerializeField] private float speedVelocityHorizontalJump = 5f;
    public bool canJump = true;//TODO
    [SerializeField] private float jumpAddForce = 500f;
    [SerializeField] float maxTimeToNextJump = 0.05f;
    [SerializeField] float deltaTimeNextJump = 0f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float toleranceNextJump = 0.0005f;

    [SerializeField] private LayerMask mask;// ta bort?
    [SerializeField] private float raycastSize;
    private float raycastSizeOriginal;
    private float horizontalInput;
    private Vector3 side;



    //  private float verticalInput;


    private void Awake()
    {
        coll2D = GetComponent<Collider2D>();
        //raycastSize = (coll2D.bounds.size.y * 0.5f) + toleranceNextJump;
        rb2D = GetComponent<Rigidbody2D>();
        side = new Vector3(coll2D.bounds.size.x * 0.5f, 0f, 0f);
        raycastSizeOriginal = raycastSize;

    }

    void Start()
    {
        deltaTimeNextJump = 0;
        SetMaterialFrictiom();

    }
    private void Update()
    {
        horizontalInput = Input.GetAxis(horizontalMoment); //Höger Vänster styrning 
        VerticalMovmenent();
        JumpMovment();
        ZeroVelocityCollidIfJump();
    }


    private void FixedUpdate()
    {
        HorizontalMovmenent();
    }

    private bool Grounded()
    {

        RaycastHit2D hitMid = Physics2D.Raycast(transform.position, Vector2.down, raycastSize);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - side, Vector2.down, raycastSize);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + side, Vector2.down, raycastSize);

        if (hitMid.collider != null || hitLeft.collider != null || hitRight.collider != null)
            return true;

        return false;

    }

    private void ZeroVelocityCollidIfJump()
    {
        if (rb2D.velocity.magnitude > 0 && !Grounded())
        {
            RaycastHit2D hitLeftBot = Physics2D.Raycast(transform.position, Vector2.left, raycastSize);
            RaycastHit2D hitRightTop = Physics2D.Raycast(transform.position, Vector2.right, raycastSize);

            if ( hitLeftBot.collider != null || hitRightTop.collider != null)
                rb2D.velocity = new Vector2(0, rb2D.velocity.y);

        }
    }
    private void JumpMovment()
    {
        if (rb2D.velocity.y < 0)
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb2D.velocity.y > 0 && !Input.GetButton(jumpAxis))
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    //AddForce För att hoppa
    private void VerticalMovmenent()
    {
        if (Input.GetButtonDown(jumpAxis))
        {
            if (Grounded())
            {
                rb2D.AddForce(Vector2.up * jumpAddForce);
                SetMaterialFrictiom();
            }
        }
    }

    private void HorizontalMovmenent()
    {


        Vector2 movement = new Vector2(horizontalInput * speedVelocityHorizontal * Time.deltaTime, rb2D.velocity.y);

        rb2D.velocity = movement;




        /*if (horizontalInput < 0)//Left
        {
            if (Grounded())
            {
                rb2D.velocity = new Vector2(-speedVelocityHorizontalJump, rb2D.velocity.y);
            }
            else
            {
                rb2D.velocity = new Vector2(-speedVelocityHorizontal, rb2D.velocity.y);//* Time.deltaTime
            }
            horizontalInput = 0;
        }
        else if (horizontalInput > 0)//Right
        {
            if (!Grounded())
            {
                rb2D.velocity = new Vector2(+speedVelocityHorizontalJump, rb2D.velocity.y);//* Time.deltaTime
            }
            else
            {
                rb2D.velocity = new Vector2(+speedVelocityHorizontal, rb2D.velocity.y);//* Time.deltaTime
            }

            horizontalInput = 0;
        }
        else if (horizontalInput == 0)
        {
            if (rb2D.velocity.x < 0)//Left
            {

            }
            if (rb2D.velocity.x > 0)//Right
            {

            }
        }*/
    }

    private void SetMaterialFrictiom()
    {
        if (Grounded())
        {
            //coll2D.sharedMaterial.friction = materialFrictiom;
        }
        else
        {
            //coll2D.sharedMaterial.friction = materialFrictiomZero;
        }
    }



    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, Vector3.down * 1.0001f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector3.left * 1.0001f);
        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, Vector3.right * 1.0001f);

    }
}
