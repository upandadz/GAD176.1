using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ThrowableBase : MonoBehaviour, IPickupable
{
    public bool thrown = false;
    public bool wasThrownRight;
    public Rigidbody2D rb;
    
    protected Vector2 throwDirection;
    
    [SerializeField] protected BoxCollider2D boxCollider;
    public virtual void Throw()
    {
        
    }

    /*protected virtual void Pickup(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HeldPickup>().itemHeld == null) // check if already holding an item
        {
            boxCollider.enabled = false; // turn off collider
            rb.simulated = false; // set rb to is simulated = false
            transform.parent = collision.transform; // set parent to player that walks over it
            heldPickup = collision.gameObject.GetComponent<HeldPickup>();
            transform.position = heldPickup.pickupPoint.position; // set position to players throw spot
            heldPickup.itemHeld = this.gameObject; // add to players held item 
        }
    }*/
    public virtual void Pickup()
    {
        rb.simulated = false; // set rb to is simulated = false
        boxCollider.isTrigger = false; // turn off trigger
    }
    
}
