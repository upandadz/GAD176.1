using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarOfNails : ThrowableBase
{
    private Quaternion rotationRight = Quaternion.Euler(68, 90, -90);
    private Quaternion rotationLeft = Quaternion.Euler(112, 90, -90);
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

    public override void Pickup()
    {
        base.Pickup();
        boxCollider.enabled = false; // ------------------ still colliding with player when thrown
    }

    public override void Throw()
    {
        Wait(0.5f);
        boxCollider.enabled = true; // need to edit time perhaps ------------------
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
        if (wasThrownRight)
        {
            Instantiate(prefabsList.particles[1], transform.position, rotationRight);
        }
        else
        {
            Instantiate(prefabsList.particles[1], transform.position, rotationLeft);
        }
        // instantiate nails -- should have its own script, also depending on direction thrown should change where they spawn
        Instantiate(prefabsList.hazards[0], transform.position, Quaternion.identity);
        // destroy self
        Destroy(gameObject);
    }

    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
