using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : ThrowableBase
{
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (canBePickedUp && collision.gameObject.tag == "Player") // pickup
        {
            Pickup(collision);
        }
        else if (thrown && collision.gameObject.tag == "Player") // thrown at player
        {
            // stay on player as visual only
            // particle effect
            // sound
            // add to points
        }
        else if (thrown && collision.gameObject.tag == "Ground") // lands on ground
        {
            // stick in the ground
        }
    }

    private void FixedUpdate()
    {
        if (thrown)
        {
            // change rotation based on y velicoty
        }
    }

    protected override void Pickup(Collision2D collision)
    {
        base.Pickup(collision);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, -65)); // need to change angle depending on what way the player is facing ------------ maybe a fixed angle? or i can instantiate a new one?
    }
}
