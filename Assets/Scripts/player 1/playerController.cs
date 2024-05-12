using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class playerController : MonoBehaviour
{
    [SerializeField]
    // Movement variables
    public float runSpeed;
    public float walkSpeed;
    bool running;

    private Vector2 movementInput; // Added movementInput variable
    private bool jumped; // Added jumped variable
    private bool shooting; // Added shooting variable

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
    public float jumpCooldown = 0.5f; // Adjust jump cooldown as needed
    float lastJumpTime;

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        facingRight = true;
        lastJumpTime = -jumpCooldown;
    }

    public void Move(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumped = true;
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        shooting = context.action.triggered;
        if (shooting)
        {
            Shoot();
        }
    }

    void Update()
    {
        // Jumping logic
        if (grounded && canJump && jumped && !isJumping)
        {
            Jump(fixedJumpHeight);
            lastJumpTime = Time.time;
            canJump = false;
            isJumping = true; // Set isJumping flag
        }

        // Update grounded state in animator
        myAnim.SetBool("grounded", grounded);

        // Reset jump flag if not pressed
        if (!jumped)
        {
            canJump = true;
        }
        else
        {
            // Set jumped to false to avoid jumping multiple times
            jumped = false;
        }

        // Check for cooldown to reset isJumping flag
        if (isJumping && Time.time - lastJumpTime > jumpCooldown)
        {
            isJumping = false;
        }
    }




    void FixedUpdate()
    {
        // Ground check
        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        grounded = groundCollisions.Length > 0;

        // Movement
        float move = movementInput.x; // Changed to use movementInput
        myAnim.SetFloat("speed", Mathf.Abs(move));

        float sneaking = Input.GetAxis("Fire3");
        myAnim.SetFloat("sneaking", sneaking);

        float firing = Input.GetAxis("Fire1");
        myAnim.SetFloat("shooting", firing);

        if ((sneaking > 0 || firing > 0) && grounded)
        {
            myRB.velocity = new Vector3(move * walkSpeed, myRB.velocity.y, 0);
        }
        else
        {
            myRB.velocity = new Vector3(move * runSpeed, myRB.velocity.y, 0);
            if (Mathf.Abs(move) > 0) running = true;
        }

        // Flip character
        if (move > 0 && !facingRight || move < 0 && facingRight)
        {
            Flip();
        }
    }

    void Jump(float jumpHeight)
    {
        isJumping = true;
        grounded = false;
        myRB.velocity = new Vector3(myRB.velocity.x, jumpHeight, 0);
    }

    void Shoot()
    {
        // Implement your shooting logic here
        Debug.Log("Shooting!"); // Example: Print "Shooting!" to the console
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
