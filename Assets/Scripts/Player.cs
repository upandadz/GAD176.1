using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PrefabsList prefabsList; // reference to prefabs list as it will be needed for powerups & particle effects
    
    private int spearsStuckInMe; // does nothing yet
    private int nailsStoodOn; // also does nothing yet
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        // collision with throwables
        if (other.gameObject.GetComponent<ThrowableBase>() != null) // could check for thrown bool, but it shouldn't matter as projectiles are triggers unless thrown
        {
            Instantiate(prefabsList.particles[0], other.transform.position, Quaternion.identity); // blood particles
            
            // play hit animation
            animator.SetTrigger("Hit");
            
            // play sound
        }
    }
}
