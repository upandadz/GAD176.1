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
    
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    public LayerMask groundLayer;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 20f;
    
    void Start()
    {
        moveLeft = controls.moveLeftKey;
        moveRight = controls.moveRightKey;
        jump = controls.jumpKey;
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
}
