using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Throw : MonoBehaviour
{
    [FormerlySerializedAs("heldPickup")] [SerializeField] private Pickup pickup;
    [SerializeField] private Controls controls;
    [SerializeField] private Animator animator;

    private float maxThrowForce = 9f;
    private float holdDownStartTime;
    
    private KeyCode throwKey;

    void Start()
    {
        throwKey = controls.throwKey;
    }

    void Update()
    {
        if (Input.GetKeyDown(throwKey) && pickup.itemHeld != null)
        {
            holdDownStartTime = Time.time;
        }
        if (Input.GetKeyUp(throwKey) && pickup.itemHeld != null)
        {
            float holdDownTime = Time.time - holdDownStartTime;
            animator.SetTrigger("Threw");
            pickup.itemHeld.GetComponent<ThrowableBase>().Throw(CalculateThrowForce(holdDownTime));
        }
        
    }

    private float CalculateThrowForce(float holdTime)
    {
        float maxHoldDownTime = 1f;
        float holdDownTimeNormalized = Mathf.Clamp01(holdTime / maxHoldDownTime); // keeps the value between 0 and 1
        float force = holdDownTimeNormalized * maxThrowForce;
        if (force < 4f)
        {
            force = 4f;
        }
        return force;
    }
}
