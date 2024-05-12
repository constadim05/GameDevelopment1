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
    public float jumpCooldown = 1f;
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
        jumped = context.action.triggered;
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        shooting = context.action.triggered;
    }

    void Update()
    {
        // Jumping logic
        if (grounded && canJump && jumped)
        {
            Jump(fixedJumpHeight);
            lastJumpTime = Time.time;
            canJump = false;
        }

        // Update grounded state in animator
        myAnim.SetBool("grounded", grounded);

        // Shooting logic
        if (shooting)
        {
            // Add your shooting logic here
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

        // Jump cooldown check
        if (Time.time - lastJumpTime > jumpCooldown)
        {
            canJump = true;
        }

        // Update canJump when grounded
        if (grounded)
        {
            canJump = true;
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
