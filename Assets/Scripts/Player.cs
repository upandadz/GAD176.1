using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    [SerializeField] private Animator animator;
    [SerializeField] private PrefabsList prefabsList; // reference to prefabs list as it will be needed for powerups & particle effects
    
    private int spearsStuckInMe; // does nothing yet
    private int nailsStoodOn; // also does nothing yet
    private float poisonedTime; // nothing yet either
    private bool poisoned = false;

    void Update()
    {
        if (poisoned == true)
        {
            poisonedTime += Time.deltaTime;
        }
    }

    public int CalculateFinalScore()
    {
        int finalScore = spearsStuckInMe + nailsStoodOn + (int)(poisonedTime/2);
        return finalScore;
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }
    private void OnCollisionEnter2D(Collision2D other) // ------------------------------- only working for jars of nails, not spears
    {
        // collision with throwables
        if (other.gameObject.GetComponent<ThrowableBase>() != null && other.gameObject.GetComponent<ThrowableBase>().thrown == true)
        {
            Instantiate(prefabsList.particles[0], other.transform.position, Quaternion.identity); // blood particles
            
            // play hit animation
            animator.SetTrigger("Hit");

            if (other.gameObject.GetComponent<PiercingProjectile>() != null) // stuck by spear or dart
            {
                GameEvents.OnHitByProjectileEvent?.Invoke();
                spearsStuckInMe++;
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PileOfNails>() != null)
        {
            Instantiate(prefabsList.particles[2], collision.transform.position, Quaternion.identity);
            nailsStoodOn++;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PileOfNails>() != null)
        {
            Instantiate(prefabsList.particles[2], other.transform.position, Quaternion.identity);
        }
    }
}
