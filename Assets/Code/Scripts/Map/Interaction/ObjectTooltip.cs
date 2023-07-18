using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTooltip : MonoBehaviour
{
    public ObjectMetadata metadata;

    void OnMouseOver()
    {
        var tooltipManager = GamePlayers.LocalUser.tooltipManager; 
        if (!tooltipManager.IsFocused)
            tooltipManager.Focus(metadata.tooltipData);
    }

    void OnMouseExit()
    {
        var tooltipManager = GamePlayers.LocalUser.tooltipManager; 

        if (tooltipManager.IsFocused)
            tooltipManager.Unfocus();
    }
}
