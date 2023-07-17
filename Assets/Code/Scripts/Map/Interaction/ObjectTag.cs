using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTag : MonoBehaviour
{
    public ObjectMetadata metadata;

    void OnMouseOver()
    {
        var tooltipManager = GamePlayers.LocalUser.tooltipManager; 
        if (!tooltipManager.IsFocused)
            tooltipManager.Focus(metadata.tooltipData);
        
        var cursorManager = GamePlayers.LocalUser.cursorManager;
        cursorManager.Type = CursorType.Interact; 
    }

    void OnMouseExit()
    {
        var tooltipManager = GamePlayers.LocalUser.tooltipManager; 

        if (tooltipManager.IsFocused)
            tooltipManager.Unfocus();
        
        var cursorManager = GamePlayers.LocalUser.cursorManager;
        if (cursorManager.Type == CursorType.Interact)
            cursorManager.Type = CursorType.Idle;
    }
}
