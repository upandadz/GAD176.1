using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField] private HeldPickup heldPickup;
    [SerializeField] private Controls controls;

    private float maxThrowForce = 8f;
    private float holdDownStartTime;
    
    private KeyCode throwKey;

    void Start()
    {
        throwKey = controls.throwKey;
    }

    void Update()
    {
        if (Input.GetKeyDown(throwKey) && heldPickup != null)
        {
            holdDownStartTime = Time.time;
        }
        if (Input.GetKeyUp(throwKey) && heldPickup != null)
        {
            float holdDownTime = Time.time - holdDownStartTime;
            heldPickup.itemHeld.GetComponent<ThrowableBase>().Throw(CalculateThrowForce(holdDownTime));
        }
        
    }

    private float CalculateThrowForce(float holdTime)
    {
        float maxHoldDownTime = 1.5f;
        float holdDownTimeNormalized = Mathf.Clamp01(holdTime / maxHoldDownTime); // keeps the value between 0 and 1
        float force = holdDownTimeNormalized * maxThrowForce;
        return force;
    }
}
