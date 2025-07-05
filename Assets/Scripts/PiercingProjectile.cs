using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PiercingProjectile : ThrowableBase
{
    [SerializeField] private PolygonCollider2D tipCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;
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

    protected virtual void OnCollisionEnter2D(Collision2D collision) 
    {
        
        if (thrown && collision.gameObject.GetComponent<Surface>() != null) // lands on ground
        {
            MakeStatic();
            tipCollider.enabled = false;
            // transform.parent = collision.transform; // this is for when I have moving platforms so it moves with the platform, currently making it go weird with the floor
        }
        else if (thrown && collision.gameObject.GetComponent<Player>() != null) // hit player
        {
            thrown = false;
           // GameEvents.OnHitByProjectileEvent.Invoke(collision.gameObject.GetComponent<Player>()); ---------------------
            // change sprite to dug in
            spriteRenderer.sprite = dugInSprite;
            // make player the parent
            transform.parent = collision.transform;
            // stop velocity
            rb.velocity = Vector2.zero;

            Vector3 currentLocalPos = transform.localPosition;
            float centerPull = 0.3f;
            float targetX;
            if (wasThrownRight)
            {
                targetX = 0.2f;
            }
            else
            {
                targetX = -0.2f;
            }
            float newX = Mathf.Lerp(currentLocalPos.x, targetX, centerPull);
            
            float targetY;
            float newY = currentLocalPos.y; // named new Y because it will potentially change depending how far from the center it lands
            if (currentLocalPos.y < -0.3f) // if too far low
            {
                targetY = -0.2f;
                newY = Mathf.Lerp(currentLocalPos.y, targetY, centerPull);
            }
            else if (currentLocalPos.y > 0.3f) // if too high
            {
                targetY = -0.2f;
                newY = Mathf.Lerp(currentLocalPos.y, targetY, centerPull);
            }
            transform.localPosition = new Vector3(newX, newY, transform.localPosition.z);
            
            tipCollider.enabled = false;
            boxCollider.enabled = false;
            rb.simulated = false;
        }
    }
    
    private void FixedUpdate()
    {
        // rotate if thrown
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
