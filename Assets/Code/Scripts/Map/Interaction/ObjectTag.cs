using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTag : MonoBehaviour
{
    public ObjectMetadata metadata;

    // Initialize collider
    public void Start()
    {
        if (!GetComponent<PolygonCollider2D>())
        {
            // Copy the collider imported by tile
            var childCollider = gameObject.GetComponentInChildren<PolygonCollider2D>();
            var newCollider = gameObject.AddComponent<PolygonCollider2D>();

            newCollider.pathCount = childCollider.pathCount;
            newCollider.points = childCollider.points;

        }    
    }

    void OnMouseOver()
    {
        
    }
}
