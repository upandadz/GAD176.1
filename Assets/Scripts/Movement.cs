using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Controls controls;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform transform;
   
    private KeyCode moveLeft;
    private KeyCode moveRight;
    private KeyCode jump;
    private KeyCode dash;

    private bool isDashing = false;
    private bool canDash = true;
    private float dashCD = 1f;
    private float dashingTime = 0.2f;
    
    
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    public LayerMask groundLayer;

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float dashForce = 10f;
    
    void Start()
    {
        moveLeft = controls.moveLeftKey;
        moveRight = controls.moveRightKey;
        jump = controls.jumpKey;
        dash = controls.DashKey;
    }
    
    void Update()
    {
        // move left
        if (Input.GetKey(moveLeft))
        {
            rb.velocity = new Vector2(-1 * moveSpeed, rb.velocity.y);
        }

        // move right
        if (Input.GetKey(moveRight))
        {
            rb.velocity = new Vector2(1 * moveSpeed, rb.velocity.y);
        }
        
        // jump
        if (Input.GetKey(jump) && IsGrounded())
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
        }
        
        // dash
        if (Input.GetKey(dash) && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    
    // something to detect when the player hits the ground - raycast
    public bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer)) // can't do transform.down so we do -transform.up
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
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize);
    }

    private IEnumerator Dash() // need to change movement so the local scale changes depending on direction moved
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale; // storing original gravity
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashForce, 0f); // add force to local x direction
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        rb.gravityScale = originalGravity; // setting gravity back
        yield return new WaitForSeconds(dashCD);
        canDash = true;
        
    }
}
