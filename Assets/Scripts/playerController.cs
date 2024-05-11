using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float walkSpeed;

    private CharacterController characterController;
    private Animator animator;
    private bool facingRight = true;

    private bool grounded;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Vector2 movementInput;
    private bool jumpInput;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumpInput = context.action.triggered;
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        float move = movementInput.x;

        float speed = Mathf.Abs(move) > 0 ? (Input.GetButton("Fire3") || Input.GetButton("Fire1") ? walkSpeed : runSpeed) : 0f;
        animator.SetFloat("speed", Mathf.Abs(move));

        Vector3 movement = new Vector3(move * speed, characterController.velocity.y, 0f);
        characterController.Move(movement * Time.fixedDeltaTime);

        if ((move > 0 && !facingRight) || (move < 0 && facingRight))
        {
            Flip();
        }
    }

    private void HandleJump()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (grounded && jumpInput)
        {
            Jump();
        }
    }

    private void Jump()
    {
        float jumpHeight = 5f; // Set your desired jump height here
        Vector3 jumpVelocity = new Vector3(characterController.velocity.x, jumpHeight, 0f);
        characterController.Move(jumpVelocity * Time.fixedDeltaTime);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
