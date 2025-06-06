using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // amount of spears stuck in them
    [SerializeField] private PrefabsList prefabsList; // reference to prefabs list as it will be needed for powerups & particle effects

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Throwable" && other.gameObject.GetComponent<ThrowableBase>().GetThrown()) // want to make it so only if collides with the tip --------------
        {
            Instantiate(prefabsList.particles[0], other.transform.position, Quaternion.identity);
        }
    }
    
}
