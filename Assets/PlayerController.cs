using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 movement;
    PlayerControls controls;

    Animator animator;
    Vector2 idlePosition;

    PlayerHiding playerHiding;
    Rigidbody2D rb;
    [SerializeField]
    float moveSpeed = 1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerHiding = GetComponent<PlayerHiding>();
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Enable();
        PlayerInteraction.OnPCClicked += DisablePlayerControls;
    }

    private void OnDisable()
    {
        controls.Disable();
        PlayerInteraction.OnPCClicked -= DisablePlayerControls;
    }

    void DisablePlayerControls()
    {
        controls.Disable();
    }

    void PlayerInput()
    {
        if (playerHiding.IsHidden())
        {
            movement = Vector2.zero;
        }
        else
        {
            movement = controls.Player.Movement.ReadValue<Vector2>();
        }


        if (movement == Vector2.zero)
        {
            AnimateIdle();
        }
        else
        {
            AnimateMovement();
            // Idle position is the movement last frame if the player moved
            idlePosition = movement;
        }
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AnimateMovement()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetInteger("Speed", (int)movement.sqrMagnitude);
    }

    private void AnimateIdle()
    {
        animator.SetFloat("Horizontal", 0f);
        animator.SetFloat("Vertical", 0f);
        animator.SetInteger("Speed", 0);
        animator.SetFloat("HorizontalIdle", idlePosition.x);
        animator.SetFloat("VerticalIdle", idlePosition.y);
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public bool IsMoving() => movement != Vector2.zero;
}
