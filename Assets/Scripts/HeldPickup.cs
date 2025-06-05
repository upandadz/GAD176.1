using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldPickup : MonoBehaviour
{
    public Transform pickupPoint;
    public GameObject itemHeld;
    private Controls controls;

    private void Start()
    {
        controls = GetComponent<Controls>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(controls.throwKey) && itemHeld != null) // this needs to be moved elsewhere -------------
        {
            itemHeld.GetComponent<ThrowableBase>().Throw();
        }
    }
}
