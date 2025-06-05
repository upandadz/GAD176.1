using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ThrowableBase : MonoBehaviour
{
    protected bool canBePickedUp = true;
    protected bool thrown = false;
    
    protected float throwForce = 8f;
    
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected BoxCollider2D boxCollider;
    public void Throw()
    {
        Vector2 throwDirection = new Vector2(1, 1); // should change depending on what way the player is facing --------
        rb.simulated = true;  // turn rb.simulated = true
        transform.parent = null; // remove parent
        thrown = true;
        rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);; // launch the damn thing
        StartCoroutine(WaitForXSeconds(0.1f)); // wait a tiny bit till collider is turned back on
        boxCollider.enabled = true;
        // need to get rid of itself from held item-----------
        
    }

    protected virtual void Pickup(Collision2D collision)
    {
        canBePickedUp = false;
        boxCollider.enabled = false; // turn off collider
        rb.simulated = false; // set rb to is simulated = false
        transform.parent = collision.transform; // set parent to player that walks over it
        transform.position = collision.gameObject.GetComponent<HeldPickup>().pickupPoint.position;  // set position to players throw spot, maybe change the angle
        collision.gameObject.GetComponent<HeldPickup>().itemHeld = this.gameObject; // add to players held item 
        // needs to check if already holding an item ---------------
    }

    private IEnumerator WaitForXSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
