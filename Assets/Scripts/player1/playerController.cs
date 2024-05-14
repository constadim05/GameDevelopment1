using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Movement variables
    public float runSpeed;
    public float walkSpeed;
    bool running;

    Rigidbody myRB;
    Animator myAnim;

    bool facingRight;

    // For jumping
    bool grounded = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float fixedJumpHeight;

    bool isJumping = false;
    bool canJump = true;
    public float jumpCooldown = 1f;
    float lastJumpTime;

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        facingRight = true;

        lastJumpTime = -jumpCooldown;
    }

    void Update()
    {
        // Movement logic for PlayerController1
        float move = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            move = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            move = 1f;
        }
        Move(move);

        // Jumping logic for PlayerController1
        if (Input.GetKeyDown(KeyCode.W) && canJump)
        {
            Jump(fixedJumpHeight);
            lastJumpTime = Time.time;
            canJump = false;
        }

        // Update grounded state in animator
        myAnim.SetBool("grounded", grounded);

        // Update running state in animator
        myAnim.SetBool("isRunning", running);
    }

    void FixedUpdate()
    {
        // Ground check
        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        grounded = groundCollisions.Length > 0;

        // Flip character
        float move = Input.GetAxis("Horizontal");
        if (move > 0 && !facingRight || move < 0 && facingRight)
        {
            Flip();
        }

        // Jump cooldown check
        if (Time.time - lastJumpTime > jumpCooldown)
        {
            canJump = true;
        }
    }

    void Move(float move)
    {
        // Movement logic for PlayerController1
        if (grounded)
        {
            myRB.velocity = new Vector3(move * runSpeed, myRB.velocity.y, 0);
            running = Mathf.Abs(move) > 0;
        }
        else
        {
            running = false;
        }
    }

    void Jump(float jumpHeight)
    {
        isJumping = true;
        grounded = false;
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
        return facingRight ? 1 : -1;
    }

    public bool getRunning()
    {
        return running;
    }
}
