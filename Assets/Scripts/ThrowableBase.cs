using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableBase : MonoBehaviour
{
    protected bool canBePickedUp = true;
    protected bool thrown = false;
    
    

    protected void Throw()
    {
        // wait a tiny bit till collider is turned back on
        // turn rb.kinematic = false
        // remove parent
        // 
        // launch the damn thing
    }

    protected void Pickup()
    {
        // turn off collider
        // set rb to is kinematic = true
        // set parent to player that walks over it
        // set position to players throw spot, maybe change the angle
        // add to players held item to x script knows it has something to throw
    }
}
