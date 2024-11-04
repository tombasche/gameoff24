using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Vector2 movement;
    Vector2 idlePosition;

    Animator animator;

    Rigidbody2D rb;
    [SerializeField]
    float moveSpeed = 1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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

    private void FixedUpdate()
    {
        Move();
    }

    public void Movement(Vector2 movementVector)
    {
        movement = movementVector;
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

    public Vector3 FacingDirection()
    {
        return idlePosition;
    }
}
