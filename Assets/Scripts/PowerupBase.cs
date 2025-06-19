using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBase : MonoBehaviour, IPickupable
{
    
    public virtual void Pickup()
    { 
        // activate
    }
    
    void FixedUpdate()
    {
        // make view go bigger then smaller on repeat
        // change colour randomly too
    }
}
