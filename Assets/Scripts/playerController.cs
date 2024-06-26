using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update

    //movement variables
    public float runSpeed;
    public float walkSpeed;
    bool running;

    Rigidbody myRB;
    Animator myAnim;

    bool facingRight;

    //for jumping
    bool grounded = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float fixedJumpHeight;

    bool isJumping = false;
    bool canDoubleJump = true;
    public float doubleJumpCooldown = 1f;
    float doubleJumpTimer;
    float lastJumpTime;

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        facingRight = true;

        lastJumpTime = -doubleJumpCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (grounded && Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            grounded = false;
            myAnim.SetBool("grounded", grounded);
            myRB.velocity = new Vector3(myRB.velocity.x, fixedJumpHeight, 0);
        }
        else if (canDoubleJump && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            lastJumpTime  = Time.time;
            grounded = false;
            myAnim.SetBool("grounded", grounded);
            myRB.velocity = new Vector3(myRB.velocity.x, fixedJumpHeight, 0);
           
        }
    }
    void FixedUpdate()
    {
        running = false;


        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0)
        {
            grounded = true;
            isJumping = false; //reset the jumping flag when grounded
        }
        else
        {
            grounded = false;
        }

        myAnim.SetBool("grounded", grounded);

        float move = Input.GetAxis("Horizontal");
        myAnim.SetFloat("speed", Mathf.Abs(move));

        float sneaking = Input.GetAxis("Fire3");
        myAnim.SetFloat("sneaking", sneaking);

        myRB.velocity = new Vector3(move * runSpeed, myRB.velocity.y, 0);

        

        float firing = Input.GetAxis("Fire1");
        myAnim.SetFloat("shooting", firing);

        if((sneaking>0 || firing>0) && grounded)
        {
           
            myRB.velocity = new Vector3(move * walkSpeed, myRB.velocity.y, 0);


        }
        else
        {
            myRB.velocity = new Vector3(move * runSpeed, myRB.velocity.y, 0);
            if(Mathf.Abs(move)>0) running = true;
        }

        if (move > 0 && !facingRight) Flip();
        else if (move < 0 && facingRight) Flip();
    
        if (Time.time - lastJumpTime > doubleJumpCooldown)
        {
            canDoubleJump = true;
        }
        else
        {
            canDoubleJump = false;
        }
    }
    void Jump(float jumpHeight)
    {
        isJumping = true;
        grounded = false;
        myAnim.SetBool("grounded", grounded);
        myRB.velocity = new Vector3(myRB.velocity.x, jumpHeight, 0);
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
    }

    public float GetFacing()
    {
        if (facingRight) return 1;
        else return -1;

    }

    public bool getRunning()
    {
        return (running);
    }
}
