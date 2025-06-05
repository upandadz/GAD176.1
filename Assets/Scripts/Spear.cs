using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : ThrowableBase
{
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (canBePickedUp && collision.gameObject.tag == "Player") // pickup
        {
            Pickup();
        }
        else if (thrown && collision.gameObject.tag == "Player") // thrown at player
        {
            
        }
        else if (thrown && collision.gameObject.tag == "Ground") // lands on ground
        {
            
        }
    }
}
