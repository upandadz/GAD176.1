using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : ThrowableBase
{
    private float minY = -10f; // Expected max fall speed
    private float maxY = 10f;  // Expected max upward speed

    private float minAngle = -135f;
    private float maxAngle = -45f;
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (canBePickedUp && collision.gameObject.tag == "Player") // pickup
        {
            Pickup(collision);
        }
        else if (thrown && collision.gameObject.tag == "Ground") // lands on ground
        {
            thrown = false;
            canBePickedUp = true;
             // stick in the ground
        }
    }
    
    private void FixedUpdate()
    {
        if (thrown)
        {
            // starting y velocity = -45 degrees on the z axis
            // y velocity at 0 = -90 degrees on the z axis
            // -starting y velocity = - 135 degrees on the z axis
            
            float yVelocity = rb.velocity.y;

            // Normalize yVelocity to a 0–1 value
            float t = Mathf.InverseLerp(minY, maxY, yVelocity);

            // Interpolate between min and max angle
            float zRotation;
            
            if (!wasThrownRight) // TODO this was a hack due to local scaling issues with movement
            {
                zRotation = Mathf.Lerp(maxAngle, minAngle, t);
            }
            else
            {
                zRotation = Mathf.Lerp(minAngle, maxAngle, t);
            }

            rb.MoveRotation(zRotation);
        }
    }

    public override void Throw(float throwForce)
    {
        base.Throw(throwForce);
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

    protected override void Pickup(Collision2D collision)
    {
        base.Pickup(collision);
        if (collision.gameObject.GetComponent<Movement>().GetIsFacingRight()) // TODO hack to change the angle of the spear depending on what direction the player is facing
        {
            transform.rotation = Quaternion.Euler(0, 0, -65f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 65f);
        }
        
    }
}
