using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField] private Pickup pickup;
    [SerializeField] private Controls controls;

    public float throwForce = 500f;
    private KeyCode throwKey;
    private GameObject thrownObject;

    void Start()
    {
        throwKey = controls.throwKey;
    }

    void Update()
    {
        if (Input.GetKeyDown(throwKey))
        {
            ThrowItem();
        }
    }
    // player holds down throw button, throw force goes up, slider comes up showing throw force
    // throw force needs to somehow alter the trajectory of the thrown object
    
    // on release object gets thrown

    private void ThrowItem()
    {
        if (pickup.heldPickup != null)
        {
            thrownObject = pickup.heldPickup.gameObject;
            thrownObject.transform.parent = null;
            pickup.heldPickup = null;
            if(thrownObject.GetComponent<Rigidbody2D>())
            {
                thrownObject.GetComponent<Rigidbody2D>().isKinematic = true;
                // thrownObject.GetComponent<Rigidbody2D>().AddForce();
            }
            
        }
    }
    
}
