using UnityEngine;

public class PlayerController : MonoBehaviour
{//Påbörjad av Jonas Thunberg 2019-01-31
 //redigering 2019-02-11



    [SerializeField] bool canJump = true;
    [SerializeField] bool sealJump = false;
    [Space]
    [SerializeField] string horizontalMoment = "Horizontal";
    [SerializeField] string jumpAxis = "wJump";
    [SerializeField] string jumpAnimParam = "wJump";
    [SerializeField] string walkAnimParam = "wJump";
    [Space]
    [SerializeField] float speedVelocityHorizontal = 400f;
    //  [SerializeField] float speedVelocityHorizontalJump = 150f;
    [SerializeField] float jumpAddForce = 500f;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;
    [SerializeField] float gizmoRange = 1f;
    [SerializeField] LayerMask ground;

    float horizontalInput;
    Vector3 side;
    Rigidbody2D rb2D;
    Collider2D coll2D;
    Animator anim;



    void Start()
    {
        coll2D = GetComponent<Collider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        side = new Vector3(coll2D.bounds.size.x * 0.5f, 0f, 0f);
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        horizontalInput = Input.GetAxis(horizontalMoment); //Höger Vänster styrning 
        VerticalMovmenent();
        JumpMovment();
        AnimatePlayer();
    }


    private void FixedUpdate()
    {
        HorizontalMovmenent();
    }

    public bool Grounded()
    {

        RaycastHit2D hitMid = Physics2D.Raycast(transform.position, Vector2.down, gizmoRange, ground);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - side, Vector2.down, gizmoRange, ground);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + side, Vector2.down, gizmoRange, ground);

        if ((hitMid.collider != null || hitLeft.collider != null || hitRight.collider != null) && canJump)
            return true;

        return false;

    }


    private void JumpMovment()
    {
        if (rb2D.velocity.y < 0)
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb2D.velocity.y > 0 && !Input.GetButton(jumpAxis) && !sealJump)
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (Grounded())
        {
            sealJump = false;
            print("grounded");
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
            }
        }
    }

    private void HorizontalMovmenent()
    {
        Vector2 movement = new Vector2(horizontalInput * speedVelocityHorizontal * Time.deltaTime, rb2D.velocity.y);
        if(movement.x > 0) anim.SetBool("FaceingRight", true);
        else if (movement.x < 0) anim.SetBool("FaceingRight", false);
        rb2D.velocity = movement;
    }

    //Checks what direction the player is moving
    public Vector2 GetMoveDirection()
    {
        if (rb2D.velocity.x > 0)
            return Vector2.right;
            
        else if (rb2D.velocity.x < 0)
            return Vector2.left;

        return Vector2.zero;
    }

    void AnimatePlayer()
    {
        anim.SetFloat(walkAnimParam, rb2D.velocity.x);
        if (canJump)
            anim.SetBool(jumpAnimParam, !Grounded());
    }

    public void SealJump()
    {
        sealJump = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, Vector3.down * gizmoRange);
    }


}
