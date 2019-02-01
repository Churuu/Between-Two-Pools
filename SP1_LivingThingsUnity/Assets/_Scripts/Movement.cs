using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody2D rb2D;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jump = 50f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private int numberJump = 100;
    [SerializeField] private float jumpTimerCounterDown = 1f;
    [SerializeField] private string horizontal = "Horizontal";
    [SerializeField] private string jumpAxi = "Jump";
    [SerializeField] private float toleranceNextJump = 0.001f;


    private bool okToJump = false;
    private float horizontalInput;
    private float verticalInput;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start()
    {


    }
    private void Update()
    {
        horizontalInput = Input.GetAxis(horizontal);
        if (rb2D.velocity.y <= toleranceNextJump && rb2D.velocity.y >= -toleranceNextJump)
        {
            okToJump = true;
        }
        else
        {
            okToJump = false;
        }
        VerticalMovmenent();
        JumpMovment();

    }
    // Update is called once per frame
    private void FixedUpdate()
    {

        HorizontalMovmenent();







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
    private void VerticalMovmenent()
    {

        if (okToJump)
        {
            if (Input.GetButtonDown(jumpAxi))//Up
            {

                Debug.Log("Jump");
                rb2D.AddForce(Vector2.up * jump);
                //if (rb2D.velocity.y <= toleranceNextJump && rb2D.velocity.y >= -toleranceNextJump)
                //{

                //    okToJump = true;
                //}
                //else
                //{
                //    okToJump = false;
                //}
                //if (verticalInput > 0 && okToJump)
                //{


                //    // rb2D.velocity = new Vector2(rb2D.velocity.x, jump); //* Time.deltaTime
                //}



                verticalInput = 0;
            }
            //else if (verticalInput < 0)//Down
            //{
            //    rb2D.velocity = new Vector2(rb2D.velocity.x, -jump * Time.deltaTime);
            //    Debug.Log("//Down");

            //    verticalInput = 0;
            //}
            else if (verticalInput == 0)
            {
                if (rb2D.velocity.y < 0)//Down
                {
                    Debug.Log("Down Jump");
                }
                if (rb2D.velocity.y > 0)//Up
                {
                    Debug.Log("Up Jump");
                }
            }
        }
    }
    private void HorizontalMovmenent()
    {
        horizontalInput = Input.GetAxis(horizontal);
        if (horizontalInput < 0)//Left
        {
            rb2D.velocity = new Vector2(-speed, rb2D.velocity.y);//* Time.deltaTime
            Debug.Log("//Left");

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
                Debug.Log("Left speed");
            }
            if (rb2D.velocity.x > 0)//Right
            {
                Debug.Log("Right speed");
            }
        }
    }
}
