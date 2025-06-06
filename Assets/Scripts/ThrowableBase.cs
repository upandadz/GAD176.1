using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ThrowableBase : MonoBehaviour
{
    protected bool canBePickedUp = true;
    protected bool thrown = false;
    protected HeldPickup heldPickup;
    
    protected Vector2 throwDirection;
    protected bool wasThrownRight;
    
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected BoxCollider2D boxCollider;
    public virtual void Throw(float throwForce)
    {
        if (heldPickup.GetComponent<Movement>().GetIsFacingRight()) // change the direction thrown based on direction player is facing
        {
            throwDirection = new Vector2(1, 1);
            wasThrownRight = true;
        }
        else
        {
            throwDirection = new Vector2(-1, 1);
            wasThrownRight = false;
        }
        rb.simulated = true;  // turn rb.simulated = true
        transform.parent = null; // remove parent
        thrown = true;
        rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);; // launch the damn thing
        StartCoroutine(WaitForXSeconds(0.3f)); // wait a tiny bit till collider is turned back on --------------- might need to wait a little bit longer
        boxCollider.enabled = true;
        heldPickup.itemHeld = null;
        heldPickup = null;
    }

    public bool GetThrown()
    {
        return thrown;
    }

    protected virtual void Pickup(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HeldPickup>().itemHeld == null) // check if already holding an item
        {
            canBePickedUp = false;
            boxCollider.enabled = false; // turn off collider
            rb.simulated = false; // set rb to is simulated = false
            transform.parent = collision.transform; // set parent to player that walks over it
            heldPickup = collision.gameObject.GetComponent<HeldPickup>();
            transform.position = heldPickup.pickupPoint.position; // set position to players throw spot
            heldPickup.itemHeld = this.gameObject; // add to players held item 
        }
    }

    private IEnumerator WaitForXSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
