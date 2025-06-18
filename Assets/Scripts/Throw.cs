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

    private float minThrowForce = 4f;
    private float maxThrowForce = 9f;
    private float holdDownStartTime;
    private Vector2 throwDirection;
    private bool chargingThrow = false;
    private KeyCode throwKey;

    void Start()
    {
        throwKey = controls.throwKey;
    }

    void Update()
    {
        if ((Input.GetKeyDown(throwKey) || Input.GetKeyDown(KeyCode.R)) && pickup.itemHeld != null && chargingThrow == false) // ----------- keycode R just put in for quick testing
        {
            holdDownStartTime = Time.time;
            chargingThrow = true;
        }
        if ((Input.GetKeyUp(throwKey) || Input.GetKeyUp(KeyCode.R)) && pickup.itemHeld != null && chargingThrow) // throw
        {
            float holdDownTime = Time.time - holdDownStartTime;
            animator.SetTrigger("Threw");
            // -------------------------------------------- need to change the code here to have throw and straight throw
            ThrowItem(holdDownTime);
            chargingThrow = false;
        }
        
    }

    private void ThrowItem(float holdTime, bool thrownStraight = false)
    {
        ThrowableBase throwable = pickup.itemHeld.GetComponent<ThrowableBase>();
        if (movement.GetIsFacingRight())
        {
            if (thrownStraight == true)
            {
                throwDirection = new Vector2(-1f, 0.2f);
            }
            else
            {
                throwDirection = new Vector2(1, 1);
            }
            throwable.wasThrownRight = true;
        }
        else
        {
            if (thrownStraight == true)
            {
                throwDirection = new Vector2(1, 0.2f);
            }
            else
            {
                throwDirection = new Vector2(-1, 1);
            }
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
        float force = Mathf.Lerp(minThrowForce, maxThrowForce, holdDownTimeNormalized);
        return force;
    }
}
