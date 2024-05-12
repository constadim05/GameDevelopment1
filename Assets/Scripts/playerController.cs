using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float walkSpeed;

    private CharacterController characterController;
    private Rigidbody rb;
    private Animator animator;
    private bool facingRight = true;

    private bool grounded;
    public Transform groundCheck;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;

    private Vector2 movementInput;
    private bool jumpInput;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.useGravity = false; // Disable Rigidbody's gravity
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            jumpInput = true;
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
        UpdateAnimations();
    }

    private void HandleMovement()
    {
        float move = movementInput.x;

        float speed = Mathf.Abs(move) > 0 ? (Input.GetButton("Fire3") || Input.GetButton("Fire1") ? walkSpeed : runSpeed) : 0f;
        animator.SetFloat("speed", Mathf.Abs(move));

        Vector3 movement = new Vector3(move * speed, 0f, 0f);
        characterController.Move(movement * Time.deltaTime);

        if ((move > 0 && !facingRight) || (move < 0 && facingRight))
        {
            Flip();
        }
    }

    private void HandleJump()
    {
        grounded = Physics.Raycast(groundCheck.position, Vector3.down, groundCheckDistance, groundLayer);

        if (grounded && jumpInput)
        {
            float jumpVelocity = Mathf.Sqrt(2f * Physics.gravity.magnitude * characterController.height);
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
            jumpInput = false; // Reset jump input
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void UpdateAnimations()
    {
        // Implement your animation transitions based on movement and jump states here
    }

    public bool GetRunning()
    {
        // Return true if the character is running (based on your game logic)
        return false;
    }

    public float GetFacing()
    {
        return facingRight ? 1 : -1;
    }
}
