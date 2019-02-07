using UnityEngine;

public class Movement : MonoBehaviour
{//Påbörjad av Jonas Thunberg 2019-01-31
    
    private Rigidbody2D rb2D;
    [SerializeField] private float materialFrictiom = 50f;
    private int materialFrictiomZero = 0;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jump = 500f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
  //  [SerializeField] private int numberJump = 100;
 //   [SerializeField] private float jumpTimerCounterDown = 1f;
    [SerializeField] private string horizontal = "Horizontal";
    [SerializeField] private string jumpAxi = "wJump";
    [SerializeField] private float toleranceNextJump = 0.0005f;
    private float raycastSize;
    private float raycastSizeOriginal;
    LayerMask mask;
    [SerializeField] private string layerMask = "Ground";
    [SerializeField] private bool okToJump = true;
    [SerializeField] float maxTimeToNextJump = 0.01f;
    [SerializeField] float deltaTimeNextJump = 0f;
    
    private float horizontalInput;
    private float verticalInput;
    private Vector3 side;
    Collider2D coll2D;
    private void Awake()
    {
        coll2D = GetComponent<Collider2D>();
        raycastSize = (coll2D.bounds.size.y * 0.5f) + toleranceNextJump;
        rb2D = GetComponent<Rigidbody2D>();
        mask = LayerMask.GetMask(layerMask);
        side = new Vector3(coll2D.bounds.size.x * 0.5f, 0f, 0f);
        raycastSizeOriginal = raycastSize;

    }
    // Use this for initialization
    void Start()
    {
        deltaTimeNextJump = 0;
        SetMaterialFrictiom();

    }
    private void Update()
    {
        horizontalInput = Input.GetAxis(horizontal);//Höger Vänster styrning 
        OKtoJump(); // 
        VerticalMovmenent();
        JumpMovment();
      

    }


    private void FixedUpdate()
    {
        // OKtoJump();
        //Debug.Log(GetComponent<Collider2D>().bounds.size.y);
        //Debug.Log(raycastSize);

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
        if (!okToJump)
        {
            RaycastHit2D hitMid = Physics2D.Raycast(transform.position, Vector2.down, raycastSize, mask);
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - side, Vector2.down, raycastSize, mask);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position + side, Vector2.down, raycastSize, mask);

            if (hitMid.collider != null || hitLeft.collider != null || hitRight.collider != null || deltaTimeNextJump > maxTimeToNextJump)
            {
                okToJump = true;
                deltaTimeNextJump = 0;
                SetMaterialFrictiom();
          //      Debug.Log("Raycast nudar marken");
            }
        }
        //else if(rb2D.velocity.y < toleranceNextJump && rb2D.velocity.y > -toleranceNextJump)
        //{
        //    okToJump = true;
        //    SetMaterialFrictiom();
        //}

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


                rb2D.AddForce(Vector2.up * jump);
                okToJump = false;
                verticalInput = 0;
                SetMaterialFrictiom();


            }
        }
    }

    private void HorizontalMovmenent()
    {

        if (horizontalInput < 0)//Left
        {
            rb2D.velocity = new Vector2(-speed, rb2D.velocity.y);//* Time.deltaTime
        //    Debug.Log("//Left");

            horizontalInput = 0;
        }
        else if (horizontalInput > 0)//Right
        {
            rb2D.velocity = new Vector2(+speed, rb2D.velocity.y);//* Time.deltaTime


            horizontalInput = 0;
        }
        else if (horizontalInput == 0)
        {
            if (rb2D.velocity.x < 0)//Left
            {

            }
            if (rb2D.velocity.x > 0)//Right
            {
           //     Debug.Log("Right speed");
            }
        }
    }

    private void SetMaterialFrictiom()
    {
        //   Debug.Log("SetMaterialFrictiom " +okToJump);
        if (okToJump)
        {
            coll2D.sharedMaterial.friction = materialFrictiom;
            //rb2D.sharedMaterial.friction = materialFrictiom;
        }
        else
        {
          coll2D.sharedMaterial.friction = materialFrictiomZero;
            // rb2D.sharedMaterial.friction = materialFrictiomZero;
        }
        // Debug.Log(rb2D.sharedMaterial.friction);
    }

}
