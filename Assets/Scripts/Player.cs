using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    [SerializeField] private Animator animator;
    [SerializeField] private PrefabsList prefabsList; // reference to prefabs list as it will be needed for powerups & particle effects
    
    private int spearsStuckInMe;
    private int nailsStoodOn;
    private float poisonedTime;
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
        int finalScore = spearsStuckInMe + nailsStoodOn + (int)(poisonedTime/4);
        return finalScore;
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        // collision with spears
        if (other.gameObject.GetComponent<PiercingProjectile>() != null)
        {
            Instantiate(prefabsList.particles[0], other.transform.position, Quaternion.identity); // blood particles
            
            animator.SetTrigger("Hit"); // play hit animation
            
            GameEvents.OnHitByProjectileEvent?.Invoke();
            spearsStuckInMe++;
            if (other.gameObject.GetComponent<PoisonDart>() != null)
            {
                poisoned = true;
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
