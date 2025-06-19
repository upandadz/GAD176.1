using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDart : ThrowableBase
{
    public override void Pickup()
    {
        base.Pickup();
        rb.bodyType = RigidbodyType2D.Dynamic;
        boxCollider.enabled = false;
    }
    
    // maybe inherit from spear? very similar
}
