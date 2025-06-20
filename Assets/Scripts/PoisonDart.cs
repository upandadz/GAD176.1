using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDart : PiercingProjectile
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.GetComponent<Player>())
        {
            // collision with player, poison PFX, lower speed of player for X time, limit to one jump
        }
    }
    
}
