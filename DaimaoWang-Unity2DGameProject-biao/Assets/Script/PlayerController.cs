using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    private Rigidbody2D myRigidbody;
    private Animator myAnim;
    private BoxCollider2D myFeet;
    private bool isGround;
    private bool canDoubleJump;
    public float jumpSpeed;
    public float doubleJumpSpeed;

    private bool isOneWayPlatform;
    public float restoreTime;

    private bool isLadder;

    private bool isClimbing;

    private bool isJumping;

    private bool isFalling;

    private bool isDoubleJumping;

    private bool isDoubleFallng;

    public float climbSpeed;

    private float playerGravity;

    private bool jumpButton = false;


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();

        playerGravity = myRigidbody.gravityScale;


    }

    // Update is called once per frame
    void Update()
    {

        if (GameController.isGameAlive)
        {
            Flip();
            Run();
            Jump();
            CheckGrounded();
            SwitchAnimation();
            OneWayPlatformCheck();
            CheckLadder();
            Climb();
            CheckAirStatus();
            //  Attack();
        }

    }

    void CheckAirStatus()
    {
        isJumping = myAnim.GetBool("Jump");
        isFalling = myAnim.GetBool("Fall");
        isDoubleJumping = myAnim.GetBool("DoubleJump");
        isDoubleFallng = myAnim.GetBool("DoubleFall");
        isClimbing = myAnim.GetBool("Climbing");

    }
    void Climb()
    {

        if (isLadder)
        {


            //float moveY = Input.GetAxis("Vertical");
            float moveY = VirtualJoystick.v.normalized.y;


            if (moveY > 0.5f || moveY < -0.5f)
            {
                //SoundManager.PlayClimbClip();
                myAnim.SetBool("Climbing", true);
                myRigidbody.gravityScale = 0.0f;
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, moveY * climbSpeed);

            }
            else
            {
                if (isJumping || isFalling || isDoubleFallng || isDoubleJumping)
                {
                    myAnim.SetBool("Climbing", false);

                }
                else
                {
                    myAnim.SetBool("Climbing", false);

                    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0.0f);
                }
            }
        }
        else
        {
            myAnim.SetBool("Climbing", false);
            myRigidbody.gravityScale = playerGravity;
        }
    }


    void CheckLadder()
    {
        isLadder = myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }
    void RestorePlayerLayer()
    {
        if (!isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
    void OneWayPlatformCheck()
    {

        if (isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
        float moveY = Input.GetAxis("Vertical");
        if (isOneWayPlatform && moveY < -0.1f)
        {
            gameObject.layer = LayerMask.NameToLayer("OneWayPlatform");
            Invoke("RestorePlayerLayer", restoreTime);
        }
    }
    void CheckGrounded()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
        myFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform")) ||
        myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));

        isOneWayPlatform = myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
        // Debug.Log(isGround);
    }


    void Flip()
    {
        bool plyerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (plyerHasXAxisSpeed)
        {
            if (myRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);

            }

            if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);

            }
        }
    }

    void Run()
    {
        //float moveDir = Input.GetAxis("Horizontal");
        //Debug.Log(Input.GetMouseButton(0));
        float moveDir = VirtualJoystick.v.normalized.x;
        //moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
        bool plyerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("Run", plyerHasXAxisSpeed);
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump") || jumpButton)
        {

            if (isGround)
            {
                SoundManager.PlayJumpClip();
                myAnim.SetBool("Jump", true);
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                myRigidbody.velocity = Vector2.up * jumpVel;
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    SoundManager.PlayJumpClip();
                    myAnim.SetBool("DoubleJump", true);
                    Vector2 doubleJumpVel = new Vector2(0.0f, doubleJumpSpeed);
                    myRigidbody.velocity = Vector2.up * doubleJumpVel;
                    canDoubleJump = false;
                }
            }
        }
        jumpButton = false;
    }







    void SwitchAnimation()
    {
        myAnim.SetBool("Idle", false);
        if (myAnim.GetBool("Jump"))
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("Fall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("Fall", false);
            myAnim.SetBool("Idle", true);
        }

        if (myAnim.GetBool("DoubleJump"))
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("DoubleJump", false);
                myAnim.SetBool("DoubleFall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("DoubleFall", false);
            myAnim.SetBool("Idle", true);
        }
    }

    public void JumpButton()
    {
        jumpButton = true;
    }
}
