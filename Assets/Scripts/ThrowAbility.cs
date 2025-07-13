using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// The players ability to throw items
/// </summary>
public class ThrowAbility : MonoBehaviour
{
    [SerializeField] private PickupAbility pickupAbility;
    [SerializeField] private Controls controls;
    [SerializeField] private Animator animator;
    [SerializeField] private Movement movement;
    [SerializeField] private Transform powerSlider;

    private float minThrowForce = 4f;
    private float maxThrowForce = 9f;
    private float holdDownStartTime;
    private Vector2 throwDirection;
    private bool chargingThrow = false;
    private bool throwStraight;
    private KeyCode throwKey;
    private KeyCode throwStraightKey;
    private float maxHoldDownTime = 1.5f;

    void Start()
    {
        throwKey = controls.throwKey;
        throwStraightKey = controls.throwStraightKey;
    }

    void Update()
    {
        if (movement.frozen)
        {
            return;
        }
        // start charging throw
        if (Input.GetKeyDown(throwKey) && pickupAbility.itemHeld != null && chargingThrow == false) 
        {
            ChargeThrow(false);
        }
        else if (Input.GetKeyDown(throwStraightKey) && pickupAbility.itemHeld != null && chargingThrow == false)
        {
            ChargeThrow(true);
        }
        
        if ((Input.GetKeyUp(throwKey) || Input.GetKeyUp(throwStraightKey)) && pickupAbility.itemHeld != null && chargingThrow) // throw
        {
            float holdDownTime = Time.time - holdDownStartTime;
            animator.SetTrigger("Threw");
            ThrowItem(holdDownTime, throwStraight);
            chargingThrow = false;
            powerSlider.gameObject.SetActive(false);
        }
        
    }

    public float GetMaxHoldDownTime()
    {
        return maxHoldDownTime;
    }

    private void ChargeThrow(bool thrownStraight)
    {
        holdDownStartTime = Time.time;
        chargingThrow = true;
        throwStraight = thrownStraight;
        powerSlider.gameObject.SetActive(true);
    }

    private void ThrowItem(float holdTime, bool thrownStraight)
    {
        ThrowableBase throwable = pickupAbility.itemHeld.GetComponent<ThrowableBase>();
        if (movement.GetIsFacingRight())
        {
            if (thrownStraight == true)
            {
                throwDirection = new Vector2(1f, 0.2f);
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
                throwDirection = new Vector2(-1, 0.2f);
            }
            else
            {
                throwDirection = new Vector2(-1, 1);
            }
            throwable.wasThrownRight = false;
        }
        pickupAbility.itemHeld = null; // removing from scripts held item
        throwable.rb.simulated = true;
        throwable.transform.parent = null;
        throwable.thrown = true;
        throwable.rb.AddForce(throwDirection * CalculateThrowForce(holdTime), ForceMode2D.Impulse);
        throwable.Throw();
    }

    private float CalculateThrowForce(float holdTime)
    {
        float holdDownTimeNormalized = Mathf.Clamp01(holdTime / maxHoldDownTime); // keeps the value between 0 and 1
        float force = Mathf.Lerp(minThrowForce, maxThrowForce, holdDownTimeNormalized);
        return force;
    }
}
