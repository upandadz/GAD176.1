using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupAbility : MonoBehaviour // rename class to something to do with the ability to pick up
{
    public Transform pickupPoint;
    public GameObject itemHeld;

    [SerializeField] private Movement movement; // this is to get 

    private float zAngle = 65;
    private void OnTriggerEnter2D(Collider2D other) // need to know what direction we are facing
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
                itemHeld.transform.rotation = Quaternion.Euler(0, 0, -zAngle);
            }
            else
            {
                itemHeld.transform.rotation = Quaternion.Euler(0, 0, zAngle);
            }
            // let the item know it's been picked up - item should have Ipickable pickup function as well
            itemHeld.GetComponent<ThrowableBase>().Pickup();
        }
        // else if powerup, activate right away
        else if (item.GetComponent<PowerupBase>())
        {
            item.GetComponent<PowerupBase>().Pickup();
        }

    
    }

}
