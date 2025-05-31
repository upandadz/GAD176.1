using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private Transform pickupSpawn;
    [SerializeField] private Transform pickupParent;
    public GameObject heldPickup;
    private float rotationAngle;

    void Start()
    {
        // Debug.Log(pickupParent.transform.rotation.y);
        if (pickupParent.transform.rotation.y == 1)
        {
            rotationAngle = 45;
        }
        else if (pickupParent.transform.rotation.y == 0)
        {
            rotationAngle = -45;
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Throwable" && heldPickup == null) // need to add if thrown == false && if not already holding something
        {
            heldPickup = collision.gameObject;
            heldPickup.transform.position = pickupSpawn.position;
            heldPickup.transform.parent = transform;
            heldPickup.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
            if (heldPickup.GetComponent<Rigidbody2D>())
            {
                heldPickup.GetComponent<Rigidbody2D>().simulated = false;
            }
        }
    }
}
