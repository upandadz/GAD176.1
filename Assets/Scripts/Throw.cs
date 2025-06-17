using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Throw : MonoBehaviour
{
    [SerializeField] private Pickup pickup;
    [SerializeField] private Controls controls;
    [SerializeField] private Animator animator;
    [SerializeField] private Movement movement;

    private float maxThrowForce = 9f;
    private float holdDownStartTime;
    private Vector2 throwDirection;
    
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
        if (Input.GetKeyUp(throwKey) && pickup.itemHeld != null) // throw
        {
            float holdDownTime = Time.time - holdDownStartTime;
            animator.SetTrigger("Threw");
            ThrowItem(holdDownTime);
        }
        
    }

    private void ThrowItem(float holdTime)
    {
        ThrowableBase throwable = pickup.itemHeld.GetComponent<ThrowableBase>();
        if (movement.GetIsFacingRight())
        {
            throwDirection = new Vector2(1, 1);
            throwable.wasThrownRight = true;
        }
        else
        {
            throwDirection = new Vector2(-1, 1);
            throwable.wasThrownRight = false;
        }
        pickup.itemHeld = null;
        throwable.rb.simulated = true;
        throwable.transform.parent = null;
        throwable.thrown = true;
        throwable.rb.AddForce(throwDirection * CalculateThrowForce(holdTime), ForceMode2D.Impulse);
        throwable.Throw();
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
