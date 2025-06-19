using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : ThrowableBase
{
    [SerializeField] private PolygonCollider2D tipCollider;
    [SerializeField] private Sprite dugInSprite;
    [SerializeField] private float minY = -10f; // Expected max fall speed
    [SerializeField] private float maxY = 10f;  // Expected max upward speed
    [SerializeField] private float minAngle = -135f;
    [SerializeField] private float maxAngle = -45f;

    public override void Pickup()
    {
        base.Pickup();
        boxCollider.enabled = false;
    }
    public override void Throw()
    {
        tipCollider.enabled = true;
        if (wasThrownRight)
        {
            minAngle = -135f;
            maxAngle = -45f;
        }
        else if (!wasThrownRight) 
        {
            minAngle = 45f;
            maxAngle = 135f; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        
        if (thrown && collision.gameObject.GetComponent<Surface>()) // lands on ground
        {
            MakeStatic();
            tipCollider.enabled = false;
            // transform.parent = collision.transform; // this is for when I have moving platforms so it moves with the platform, currently making it go weird with the floor
        }
        else if (thrown && collision.gameObject.GetComponent<Player>())
        {
            // thrown = false;
            // start game event
            // change sprite to dug in
            // make player the parent
        }
    }
    
    private void FixedUpdate()
    {
        if (thrown)
        {
            // starting y velocity = -45 degrees on the z axis
            // y velocity at 0 = -90 degrees on the z axis
            // -starting y velocity = -135 degrees on the z axis
            
            float yVelocity = rb.velocity.y;

            // Normalize yVelocity to a 0–1 value
            float interpolation = Mathf.InverseLerp(minY, maxY, yVelocity);

            // Interpolate between min and max angle
            float zRotation;
            
            if (!wasThrownRight) // TODO this was a hack due to local scaling issues with movement
            {
                zRotation = Mathf.Lerp(maxAngle, minAngle, interpolation);
            }
            else
            {
                zRotation = Mathf.Lerp(minAngle, maxAngle, interpolation);
            }
            rb.MoveRotation(zRotation);
        }
    }
}
