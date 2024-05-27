using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Movement variables
    public float runSpeed;

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

    public int _playerID = 0;

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
        if (_playerID == 1)
        {
            if (Input.GetKey(KeyCode.A))
            {
                Move(-1f);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Move(1f);
            }
            else
            {
                StopMoving();
            }

            if (canJump && Input.GetKeyDown(KeyCode.W))
            {
                Jump(fixedJumpHeight);
                lastJumpTime = Time.time;
                canJump = false;
            }
        }
        // Movement logic for PlayerController2
        else if (_playerID == 2)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Move(-1f);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Move(1f);
            }
            else
            {
                StopMoving();
            }

            if (canJump && Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump(fixedJumpHeight);
                lastJumpTime = Time.time;
                canJump = false;
            }
        }

        // Update grounded state in animator
        myAnim.SetBool("grounded", grounded);
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
        // Movement logic for both players
        if (grounded)
        {
            // Set the velocity based on the movement input
            myRB.velocity = new Vector3(move * runSpeed, myRB.velocity.y, 0);

            // Check if there's movement input
            bool moving = Mathf.Abs(move) > 0;

            // Set the running flag based on whether there's movement input
            running = moving;
        }
        else
        {
            // If there's no movement input or player stops moving, reset the velocity to zero and set running to false
            myRB.velocity = new Vector3(0, myRB.velocity.y, 0);
            running = false;
        }

        // Update the running parameter in the Animator
        myAnim.SetBool("running", running);
    }

    void StopMoving()
    {
        if (grounded)
        {
            // Set the velocity to zero when there's no movement input
            myRB.velocity = new Vector3(0, myRB.velocity.y, 0);
            running = false;
            myAnim.SetBool("running", false);
        }
    }

    void Jump(float jumpHeight)
    {
        if (grounded)
        {
            isJumping = true;
            grounded = false;
            myRB.velocity = new Vector3(myRB.velocity.x, jumpHeight, 0);
        }
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
