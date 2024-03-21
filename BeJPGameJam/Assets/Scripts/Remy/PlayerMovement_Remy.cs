using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement_Remy : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float climbSpeed;//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!ladder.cs!!!!!!!!!!!!!!!!!!!!!
    [SerializeField] private float jumpForce;
    [SerializeField] private Rigidbody2D rbPlayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool doubleJumpPower;

    private Vector3 velocity = Vector3.zero;
    private bool isJumping;
    private bool isGrounded;

    [HideInInspector] public bool isClimbing; //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Ladder.cs!!!!!!!!!!!!!!!!!!!!!!!!!

    private float horizontalMovement;

    private float verticalMovement;//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Ladder.cs!!!!!!!!!!!!!!

    private int jumpNumber;


    private void Update()
    {
        if (Input.GetButtonDown("Jump") && (isGrounded || (doubleJumpPower && jumpNumber < 1)))
        {
            isJumping = true;
            jumpNumber++;
        }

        if (isGrounded) jumpNumber = 0;

        Flip(rbPlayer.velocity.x);

        float characterVelocity = Mathf.Abs(rbPlayer.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
        animator.SetBool("isClimbing", isClimbing);//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!animation grimper ladder.cs!!!!!!!!!!!!!!!!!
    }


    void FixedUpdate()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        verticalMovement = Input.GetAxis("Vertical") * climbSpeed * Time.deltaTime;//!!!!!!!!!! mouvement vertical pour ladder.cs
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);
        MovePlayer(horizontalMovement,verticalMovement); //!!!!!!!!!!!!!!!!Il faut ajouter le paramètre vertical movement
    }


    void MovePlayer(float _horizontalMovement, float _verticalMovement)//!!!!!!!!!!!!!!!!!!!!!!!!!Modifiée pour ladder.cs!!!!!!!!!!!!
    {
        if (!isClimbing)//!!!!!!!!!!!!!!!!!ladder: condition pour effectuer la fonction si le joueur ne grimpe pas 
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rbPlayer.velocity.y);
            rbPlayer.velocity = Vector3.SmoothDamp(rbPlayer.velocity, targetVelocity, ref velocity, .05f);

            if (isJumping)
            {
                rbPlayer.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }
        }
        else //SI le joueur grimpe !!!!!!!!!!!!!!!!!!! ladder.cs donc faut copier tout le else
        {
            Vector3 targetVelocity = new Vector2(0, _verticalMovement);
            rbPlayer.velocity = Vector3.SmoothDamp(rbPlayer.velocity, targetVelocity, ref velocity, .05f);
        }


    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (_velocity < -0.1f)
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
    -+*/
}
