using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarOfNails : ThrowableBase
{
    private PrefabsList prefabsList; // this is to spawn in particleFX when jar shatters

    void Start()
    {
        prefabsList = FindObjectOfType<PrefabsList>(); // only one PrefabsList, I do hate to use this but I can't drag it into inspector if prefabsList is Serializefield
    }

    void FixedUpdate()
    {
        if (thrown == true)
        {
            
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        // on collision with player
        if (other.gameObject.GetComponent<Player>() && thrown == true)
        {
            // set off event maybe
            // destroy self
        }
        
        // on collision with ground
        else if (other.gameObject.GetComponent<Surface>() && thrown == true)
        {
            Smash();
            // start event to spawn in new throwable
        }
    }

    private void Smash()
    {
        // jar smashing sound
        // nails PFX
        Instantiate(prefabsList.particles[1], transform.position, Quaternion.identity);
        // instantiate nails -- should have its own script, also depending on direction thrown should change where they spawn
        // destroy self
        Destroy(gameObject);
    }
}
