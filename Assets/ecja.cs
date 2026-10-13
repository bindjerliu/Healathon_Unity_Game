using UnityEngine;

public class NPCPointToPoint : MonoBehaviour
{
    [Header("Movement Targets")]
    public Transform pointA;
    public Transform pointB;
    
    [Header("Settings")]
    public float moveSpeed = 2.5f;
    public float waitTime = 1.0f;

    private Transform _currentTarget;
    private bool _isWaiting = false;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        // Start by moving toward Point B
        _currentTarget = pointB;
    }

    void FixedUpdate()
    {
        if (_isWaiting || pointA == null || pointB == null) return;

        Move();
        CheckDistance();
    }

    private void Move()
    {
        // Determine horizontal direction (1 for Right, -1 for Left)
        float direction = (_currentTarget.position.x > transform.position.x) ? 1f : -1f;
        
        // Apply movement to Rigidbody
        _rb.linearVelocity = new Vector2(direction * moveSpeed, _rb.linearVelocity.y);

        // Flip Sprite based on direction
        if (_spriteRenderer != null)
        {
            _spriteRenderer.flipX = direction < 0; 
        }

        // Set Animation
        if (_animator != null) _animator.SetBool("IsWalking", true);
    }

    private void CheckDistance()
    {
        // Check if we are close enough to the target on the X axis
        if (Mathf.Abs(transform.position.x - _currentTarget.position.x) < 0.1f)
        {
            StartCoroutine(WaitAndSwitch());
        }
    }

    private System.Collections.IEnumerator WaitAndSwitch()
    {
        _isWaiting = true;
        _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y); // Stop moving
        
        if (_animator != null) _animator.SetBool("IsWalking", false);

        yield return new WaitForSeconds(waitTime);

        // Switch targets
        _currentTarget = (_currentTarget == pointA) ? pointB : pointA;
        _isWaiting = false;
    }
}