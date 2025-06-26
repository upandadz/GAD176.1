using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    [SerializeField] private Controls controls;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D collider;
    [SerializeField] private Transform transform;
    [SerializeField] private Animator animator;

    [SerializeField] private Vector2 boxSizeGround;
    [SerializeField] private Vector2 boxSizeWall;
    [SerializeField] private float castDistanceGround;
    [SerializeField] private float castDistanceWall;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float dashForce;
    
    private KeyCode moveLeft;
    private KeyCode moveRight;
    private KeyCode jump;
    private KeyCode dash;

    private bool canDash = true;
    private bool canDoubleJump = true;
    private bool isDashing = false;
    private bool isFacingRight = true;
    private float dashCD = 1f;
    private float dashingTime = 0.1f;
    
    void Start()
    {
        moveLeft = controls.moveLeftKey;
        moveRight = controls.moveRightKey;
        jump = controls.jumpKey;
        dash = controls.DashKey;
    }
    
    void Update()
    {
        if (isDashing == true)
        {
            return; // stops movement from messing with the dash
        }
        
        // allows animator to know when running
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        
        // move left
        if (Input.GetKey(moveLeft) && IsWalledLeft() == false)
        {
            rb.velocity = new Vector2(-1 * moveSpeed, rb.velocity.y);
        }

        // move right
        if (Input.GetKey(moveRight) && IsWalledRight() == false)
        {
            rb.velocity = new Vector2(1 * moveSpeed, rb.velocity.y);
        }
        
        // jump
        if (Input.GetKeyDown(jump) && IsGrounded())
        {
            Jump();
        }
        else if (Input.GetKeyDown(jump) && !IsGrounded() && canDoubleJump == true)
        {
            Jump();
            canDoubleJump = false;
        }
        
        // dash
        if (Input.GetKey(dash) && canDash)
        {
            StartCoroutine(Dash());
        }
        
        FlipPlayer();
    }
    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            rb.velocity *= 0.9f; // makes movement less slidey
        }
    }

    public bool GetIsFacingRight()
    {
        return isFacingRight;
    }
    
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    private void FlipPlayer()
    {
        if (rb.velocity.x > 0) // facing right
        {
            transform.localScale = new Vector3(1, 1, 1);
            isFacingRight = true;
        }
        else if (rb.velocity.x < 0) // facing left
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isFacingRight = false;
        }
    }

    // something to detect when the player hits the ground - raycast
    private bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSizeGround, 0, -transform.up, castDistanceGround, groundLayer)) // can't do transform.down so we do -transform.up
        {
            animator.SetBool("isGrounded", true);
            canDoubleJump = true;
            return true;
        }
        else
        {
            animator.SetBool("isGrounded", false);
            return false;
        }
    }

    private bool IsWalledLeft()
    {
        // two big ifs, for multiple layers, I don't like this but here we are
        if (Physics2D.BoxCast(transform.position, boxSizeWall, 0, -transform.right, castDistanceWall, wallLayer) || Physics2D.BoxCast(transform.position, boxSizeWall,0,transform.right, castDistanceWall, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsWalledRight()
    {
        if (Physics2D.BoxCast(transform.position, boxSizeWall,0,transform.right, castDistanceWall, wallLayer) || Physics2D.BoxCast(transform.position, boxSizeWall,0,transform.right, castDistanceWall, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // to visualise IsGrounded raycast
    private void OnDrawGizmos()
    { 
        Gizmos.DrawWireCube(transform.position-transform.up * castDistanceGround, boxSizeGround);
        Gizmos.DrawWireCube(transform.position - transform.right * castDistanceWall, boxSizeWall);
    }

    
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale; // storing original gravity
        Vector2 originalVelocity = rb.velocity;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashForce, 0f); // add force to local x direction
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity; // setting gravity back
        rb.velocity = originalVelocity;
        isDashing = false;
        yield return new WaitForSeconds(dashCD);
        canDash = true;
    }
}
