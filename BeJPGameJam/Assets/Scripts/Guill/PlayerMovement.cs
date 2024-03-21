using System;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] public Rigidbody2D rbPlayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] public bool doubleJumpPower;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool _isClimbing;
    
    private Vector3 _velocity = Vector3.zero;
    private bool _isJumping;
    private float _horizontalMovement;
    private float _verticalMovement;
    private int _jumpNumber;

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && (isGrounded || (doubleJumpPower && _jumpNumber < 1)))
        {
            _isJumping = true;
            _jumpNumber++;
        }

        if (isGrounded) _jumpNumber = 0;

        Flip(rbPlayer.velocity.x);

        float characterVelocity = Mathf.Abs(rbPlayer.velocity.x);
        
        if (isGrounded)
            animator.SetFloat("Speed", characterVelocity);
        else 
            animator.SetFloat("Speed", 0.1f);
        animator.SetBool("IsClimbing", _isClimbing);
    }

    void FixedUpdate()
    {
        _horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        _verticalMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);
        MovePlayer(_horizontalMovement, _verticalMovement);
    }


    void MovePlayer(float horizontalMovement, float verticalMovement)
    {
        if (!_isClimbing)
        {
            Vector3 targetVelocity = new Vector2(horizontalMovement, rbPlayer.velocity.y);
            rbPlayer.velocity = Vector3.SmoothDamp(rbPlayer.velocity, targetVelocity, ref _velocity, .05f);

            if (_isJumping)
            {
                rbPlayer.AddForce(new Vector2(0f, jumpForce));
                _isJumping = false;
            }
        }
        else
        {
            Vector3 targetVelocity = new Vector2(0, verticalMovement);
            rbPlayer.velocity = Vector3.SmoothDamp(rbPlayer.velocity, targetVelocity, ref _velocity, .05f);
        }
    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        } else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }
    
/*
     private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
   */  
}
