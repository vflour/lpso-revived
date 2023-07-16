using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTag : MonoBehaviour
{
    public ObjectMetadata metadata;

    // Initialize collider
    public void Start()
    {
        if (!GetComponent<CompositeCollider2D>())
        {
            // composite collider includes children colliders
        }    
    }
    
    void OnMouseOver()
    {
        var tooltipManager = GamePlayers.Instance.localUser.tooltipManager; 
        if (!tooltipManager.IsFocused)
            tooltipManager.Focus(metadata.text);
    }

    void OnMouseExit()
    {
        var tooltipManager = GamePlayers.Instance.localUser.tooltipManager; 
        if (tooltipManager.IsFocused)
            tooltipManager.Unfocus();
    }
}
