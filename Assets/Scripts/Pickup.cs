using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform pickupPoint;
    public GameObject itemHeld;
    
    // on collision do pickup
    private void OnCollisionEnter2D (Collision2D collision)
    {
        
    }

    private void PickupItem(Collision2D collision)
    {
        // store reference to item
        private GameObject item;

        // add to held pickup/merge scripts perhaps

        // let the item know it's been picked up - item should have Ipickable pickup function as well

        // if throwablebase add to item held and pickup point

        // else if powerup, activate right away
}
    
}
