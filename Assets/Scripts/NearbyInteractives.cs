using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NearbyInteractives : MonoBehaviour
{
    public float interactRadius = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject interactiveObject = findInteractive();
        if(interactiveObject)
        {
            //Debug.Log("Found interactive object: " + interactiveObject.name);
        }
    }

    void interactWithObject()
    {
        GameObject interactiveObject = findInteractive();
        if(interactiveObject)
        {
            interactiveObject.BroadcastMessage("interacted");
        }
    }

    GameObject findInteractive()
    {
        // Find all colliders within radius
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, interactRadius);

        // Sort by distance
        collisions = collisions.OrderBy(x => Vector2.Distance(this.transform.position, x.transform.position)).ToArray();

        // Check if collider is an interactive object
        foreach (var collision in collisions)
        {
            if (collision.gameObject.CompareTag("Interactive"))
            {
                return collision.gameObject;
            }
        }
        return null;
    }
}
