using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Expat Studios. (2023, feb 10). Simple Moving Platforms - Unity [Video]. YouTube. https://www.youtube.com/watch?v=FtC4e2fVstE 
    
    [SerializeField] private bool isHorizontal;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveDistance; // how far to move the platform each direction
    
    private Vector2 startPosition;
    private Vector2 movePosition; // where the platform will move to

    void Start()
    {
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (isHorizontal)
        {
            movePosition.x = startPosition.x + Mathf.Sin(Time.time * moveSpeed) * moveDistance;
            transform.position = new Vector2(movePosition.x, transform.position.y);
        }
        else // verticle
        {
            movePosition.y = startPosition.y + Mathf.Sin(Time.time * moveSpeed) * moveDistance;
            transform.position = new Vector2(transform.position.x, movePosition.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.parent = transform;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.parent = null;
    }
}
