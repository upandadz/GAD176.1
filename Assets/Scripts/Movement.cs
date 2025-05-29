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

    private bool isGrounded = true;

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 5f;
    
    void Start()
    {
        moveLeft = controls.moveLeft;
        moveRight = controls.moveRight;
        jump = controls.jump;
    }
    
    void Update()
    {
        // move left
        if (Input.GetKey(moveLeft))
        {
            rb.velocity = Vector2.left * moveSpeed;
        }

        // move right
        if (Input.GetKey(moveRight))
        {
            rb.velocity = Vector2.right * moveSpeed;
        }
        
        // jump
        if (Input.GetKey(jump))
        {
            if (!isGrounded)
            {
                return;
            }
            isGrounded = false;
            rb.AddForce(Vector2.up * jumpForce);
        }
        
    }
    
    // something to detect when the player hits the ground - raycast
    
}
