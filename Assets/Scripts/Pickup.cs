using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform pickupPoint;
    public GameObject itemHeld;

    [SerializeField] private Movement movement;
    private void OnTriggerEnter2D(Collider2D other) // Pickup
    {
        PickupItem(other);
    }

    private void PickupItem(Collider2D itemPickedUp)
    {
        // store reference to item
        GameObject item = itemPickedUp.gameObject;

        // if throwablebase add to item held and pickup point, remove trigger
        if (item.GetComponent<ThrowableBase>() != null && itemHeld == null)
        {
            itemHeld = item; // add to held
            itemHeld.transform.parent = transform; // set parent
            itemHeld.transform.position = pickupPoint.position; // set position
            if (movement.GetIsFacingRight()) // TODO hack to change the angle of the spear depending on what direction the player is facing
            {
                itemHeld.transform.rotation = Quaternion.Euler(0, 0, -65f);
            }
            else
            {
                itemHeld.transform.rotation = Quaternion.Euler(0, 0, 65f);
            }
            // let the item know it's been picked up - item should have Ipickable pickup function as well
            itemHeld.GetComponent<ThrowableBase>().Pickup();
        } 
        
        // else if powerup, activate right away

    
    }

}
