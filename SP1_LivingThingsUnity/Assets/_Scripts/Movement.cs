using UnityEngine;

public class Movement : MonoBehaviour
{//Påbörjad av Jonas Thunberg 2019-01-31
 //redigering 2019-02-11
    private Rigidbody2D rb2D;
    private Collider2D coll2D;

    [SerializeField] private float materialFrictiom = 50f;
    private int materialFrictiomZero = 0;

    [SerializeField] private string horizontalMoment = "Horizontal";
    [SerializeField] private string jumpAxi = "wJump";

    [SerializeField] private float speedVelocityHorizontal = 7f;
    [SerializeField] private float speedVelocityHorizontalJump = 5f;
    public bool canJump = true;//TODO
    [SerializeField] private float jumpAddForce = 500f;
    [SerializeField] private bool okToJump = true;
    [SerializeField] float maxTimeToNextJump = 0.05f;
    [SerializeField] float deltaTimeNextJump = 0f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float toleranceNextJump = 0.0005f;

    [SerializeField] private LayerMask mask;// ta bort?
    private float raycastSize;
    private float raycastSizeOriginal;
    private float horizontalInput;
    private Vector3 side;



    //  private float verticalInput;


    private void Awake()
    {
        coll2D = GetComponent<Collider2D>();
        raycastSize = (coll2D.bounds.size.y * 0.5f) + toleranceNextJump;
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
        horizontalInput = Input.GetAxis(horizontalMoment);//Höger Vänster styrning 
        // 
        VerticalMovmenent();
        if (canJump)
        {
            OKtoJump();
            JumpMovment();
        }
        else
        {

        }



    }


    private void FixedUpdate()
    {
        HorizontalMovmenent();
    }
    //Tareda på om spelaren är på marken
    private void OKtoJump() // TODO Inte helt fel fri kan ibland missa att den nuda marken
    {//V velocety = 0 time TODO
        if (!okToJump && rb2D.velocity.y < toleranceNextJump && rb2D.velocity.y > -toleranceNextJump)
        {
            deltaTimeNextJump += Time.deltaTime;
        }
        else
        {
            deltaTimeNextJump = 0;
        }
        if (!okToJump || !(deltaTimeNextJump > maxTimeToNextJump))
        {
            RaycastHit2D hitMid = Physics2D.Raycast(transform.position, Vector2.down, raycastSize);//, mask);
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - side, Vector2.down, raycastSize);//, mask);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position + side, Vector2.down, raycastSize);//, mask);

            if (hitMid.collider != null || hitLeft.collider != null || hitRight.collider != null  || deltaTimeNextJump > maxTimeToNextJump)
            {
                okToJump = true;
                deltaTimeNextJump = 0;
                SetMaterialFrictiom();

            }
        }

    }

    private void JumpMovment()
    {
        if (rb2D.velocity.y < 0)
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb2D.velocity.y > 0 && !Input.GetButton(jumpAxi))
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    //AddForce För att hoppa
    private void VerticalMovmenent()
    {

        if (okToJump)
        {
            if (Input.GetButtonDown(jumpAxi))//Up
            {
                rb2D.AddForce(Vector2.up * jumpAddForce);
                okToJump = false;
                SetMaterialFrictiom();
            }
        }
    }

    private void HorizontalMovmenent()
    {
        if (horizontalInput < 0)//Left
        {
            if (!okToJump)
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
            if (!okToJump)
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
        }
    }

    private void SetMaterialFrictiom()
    {
        if (okToJump)
        {
            coll2D.sharedMaterial.friction = materialFrictiom;
        }
        else
        {
            coll2D.sharedMaterial.friction = materialFrictiomZero;
        }
    }

}
