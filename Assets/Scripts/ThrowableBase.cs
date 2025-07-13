using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// base for throwable items
/// </summary>
public class ThrowableBase : MonoBehaviour, IPickupable
{
    public bool thrown = false;
    public bool wasThrownRight;
    public Rigidbody2D rb;
    
    [SerializeField] protected BoxCollider2D boxCollider;
    public virtual void Throw()
    {
        
    }
    public virtual void Pickup()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.simulated = false; // set rb to is simulated = false
        boxCollider.isTrigger = false; // turn off trigger
    }

    private void OnEnable()
    {
        MakeStatic();
    }
    
    protected void MakeStatic()
    {
        thrown = false;
        // stick in/on the ground
        rb.bodyType = RigidbodyType2D.Static; 
        // turn on trigger
        boxCollider.enabled = true;
        boxCollider.isTrigger = true;
    }
    
}
