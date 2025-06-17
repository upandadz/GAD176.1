using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
        rb.simulated = false; // set rb to is simulated = false
        boxCollider.isTrigger = false; // turn off trigger
    }
    
}
