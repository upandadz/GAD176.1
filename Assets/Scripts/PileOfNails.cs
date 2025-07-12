using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileOfNails : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private SpriteRenderer view;

    private float raycastDistance = 10f;
    private bool fadingOut = false;
    private Color viewColor;
    
    // when instantiated do a raycast down, move position to there when it hits ground layer

    
    private void Start()
    {
        viewColor = view.color;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance,groundLayer);
        // Debug.Log(raycastHit2D.collider.name);
        transform.position = raycastHit2D.point;
        if (raycastHit2D.collider.gameObject.GetComponent<MovingPlatform>() != null)
        {
            transform.parent = raycastHit2D.collider.gameObject.transform;
        }

        StartCoroutine(DestroyAfterTime());
    }

    void Update()
    {
        if (fadingOut == true)
        {
            viewColor.a -= 0.5f * Time.deltaTime;
            view.color = viewColor;
        }
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(5f);
        // change transparency slowly
        fadingOut = true;
        // wait for the time is takes to do that
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
