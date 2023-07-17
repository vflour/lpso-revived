using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapCursorModifier : MonoBehaviour
{

    void OnMouseOver()
    {
        var cursorManager = GamePlayers.LocalUser.cursorManager;
        cursorManager.Type = CursorType.Walk; 
    }

    void OnMouseExit()
    {
        var cursorManager = GamePlayers.LocalUser.cursorManager;
        if (cursorManager.Type == CursorType.Walk)
            cursorManager.Type = CursorType.Idle;
    }

}
