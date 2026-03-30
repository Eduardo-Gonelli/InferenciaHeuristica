using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float maxMoveSpeed = 7f;
    [SerializeField] private float acceleration = 60f;
    [SerializeField] private float deceleration = 75f;
    [SerializeField] private float airControlMultiplier = 0.85f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float gravity = 35f;
    [SerializeField] private float fallGravityMultiplier = 1.8f;
    [SerializeField] private float jumpCutGravityMultiplier = 2.4f;
    [SerializeField] private float maxFallSpeed = 22f;
    [SerializeField] private float coyoteTime = 0.12f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.08f;
    [SerializeField] private Vector2 groundCheckPadding = new Vector2(0.08f, 0.02f);
    [SerializeField] private float collisionSkin = 0.02f;

    private CapsuleCollider2D capsuleCollider;
    private Vector2 velocity;
    private float horizontalInput;
    private float coyoteTimeCounter;
    private bool jumpPressed;
    private bool isGrounded;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        ReadInput();
        UpdateGroundState();
        HandleHorizontalMovement(Time.deltaTime);
        HandleJump();
        ApplyGravity(Time.deltaTime);
        Move(Time.deltaTime);
        jumpPressed = false;
    }

    private void ReadInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }
    }

    private void UpdateGroundState()
    {
        Collider2D hit = CheckGroundOverlap();
        bool wasGrounded = isGrounded;

        isGrounded = hit != null && velocity.y <= 0f;

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;

            if (!wasGrounded && velocity.y < 0f)
            {
                velocity.y = 0f;
            }
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    private void HandleHorizontalMovement(float deltaTime)
    {
        float targetSpeed = horizontalInput * maxMoveSpeed;
        bool isAccelerating = Mathf.Abs(targetSpeed) > 0.01f;
        float baseRate = isAccelerating ? acceleration : deceleration;
        float controlMultiplier = isGrounded ? 1f : airControlMultiplier;
        float movementRate = baseRate * controlMultiplier;

        velocity.x = Mathf.MoveTowards(velocity.x, targetSpeed, movementRate * deltaTime);
    }

    private void HandleJump()
    {
        if (jumpPressed && coyoteTimeCounter > 0f)
        {
            velocity.y = jumpForce;
            isGrounded = false;
            coyoteTimeCounter = 0f;
        }
    }

    private void ApplyGravity(float deltaTime)
    {
        if (isGrounded && velocity.y <= 0f)
        {
            velocity.y = 0f;
            return;
        }

        float gravityMultiplier = 1f;

        if (velocity.y < 0f)
        {
            gravityMultiplier = fallGravityMultiplier;
        }
        else if (velocity.y > 0f && !Input.GetButton("Jump"))
        {
            gravityMultiplier = jumpCutGravityMultiplier;
        }

        velocity.y -= gravity * gravityMultiplier * deltaTime;
        velocity.y = Mathf.Max(velocity.y, -maxFallSpeed);
    }

    private void Move(float deltaTime)
    {
        Vector3 position = transform.position;

        position.x += velocity.x * deltaTime;

        float verticalDistance = velocity.y * deltaTime;

        if (verticalDistance > 0f)
        {
            RaycastHit2D hit = CastCollider(Vector2.up, verticalDistance + collisionSkin);

            if (hit.collider != null)
            {
                float moveDistance = Mathf.Max(hit.distance - collisionSkin, 0f);
                position.y += moveDistance;
                velocity.y = 0f;
            }
            else
            {
                position.y += verticalDistance;
            }
        }
        else if (verticalDistance < 0f)
        {
            float fallDistance = Mathf.Abs(verticalDistance);
            RaycastHit2D hit = CastCollider(Vector2.down, fallDistance + collisionSkin);

            if (hit.collider != null)
            {
                float moveDistance = Mathf.Max(hit.distance - collisionSkin, 0f);
                position.y -= moveDistance;
                velocity.y = 0f;
                isGrounded = true;
            }
            else
            {
                position.y += verticalDistance;
                isGrounded = false;
            }
        }

        transform.position = position;
    }

    private Collider2D CheckGroundOverlap()
    {
        Bounds bounds = capsuleCollider.bounds;
        Vector2 size = GetCastSize();
        Vector2 center = new Vector2(bounds.center.x, bounds.min.y - (groundCheckDistance * 0.5f));

        return Physics2D.OverlapBox(center, new Vector2(size.x, groundCheckDistance), 0f, groundLayer);
    }

    private RaycastHit2D CastCollider(Vector2 direction, float distance)
    {
        Bounds bounds = capsuleCollider.bounds;
        Vector2 size = GetCastSize();

        return Physics2D.BoxCast(bounds.center, size, 0f, direction, distance, groundLayer);
    }

    private Vector2 GetCastSize()
    {
        Bounds bounds = capsuleCollider.bounds;
        Vector2 size = new Vector2(bounds.size.x, bounds.size.y) - groundCheckPadding;

        size.x = Mathf.Max(size.x, 0.05f);
        size.y = Mathf.Max(size.y, 0.05f);

        return size;
    }

    private void OnDrawGizmosSelected()
    {
        CapsuleCollider2D currentCollider = GetComponent<CapsuleCollider2D>();

        if (currentCollider == null)
        {
            return;
        }

        Bounds bounds = currentCollider.bounds;
        Vector2 size = new Vector2(bounds.size.x, bounds.size.y) - groundCheckPadding;
        Vector3 center = new Vector3(bounds.center.x, bounds.min.y - (groundCheckDistance * 0.5f), bounds.center.z);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, new Vector3(size.x, groundCheckDistance, 0f));
    }
}
