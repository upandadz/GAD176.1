using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// thrown item, can be picked up and thrown
/// </summary>
public class JarOfNails : ThrowableBase
{
    private Quaternion rotationRight = Quaternion.Euler(68, 90, -90);
    private Quaternion rotationLeft = Quaternion.Euler(112, 90, -90);
    private PrefabsList prefabsList; // this is to spawn in particleFX when jar shatters

    void Start()
    {
        prefabsList = FindObjectOfType<PrefabsList>(); // only one PrefabsList, I do hate to use this but I can't drag it into inspector if prefabsList is Serializefield
    }

    public override void Pickup()
    {
        base.Pickup();
        boxCollider.enabled = false;
    }

    public override void Throw()
    {
        thrown = true; 
        boxCollider.enabled = true;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        // on collision with ground
        if (other.gameObject.GetComponent<Surface>() && thrown == true)
        {
            Smash();
        }
    }

    private void Smash()
    {
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
        GameEvents.OnNailJarSmashed?.Invoke(); // ?. checking if not null
    }

    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
