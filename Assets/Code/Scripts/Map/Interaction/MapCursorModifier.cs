using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCursorModifier : MonoBehaviour
{
    public CursorManager cursorManager;
    public CursorType cursorType;
    public Moveable moveable;

    void Awake()
    {
        cursorManager = GamePlayers.LocalUser.cursorManager;
        moveable = GamePlayers.LocalUser.character.moveable;
    }

    void OnMouseOver()
    {
        if (moveable.State == MovementState.Busy)
            cursorManager.Type = CursorType.Deny;
        else
            cursorManager.Type = cursorType;
    }

    void OnMouseExit()
    {
        if (cursorManager.Type == cursorType)
            cursorManager.Type = CursorType.Idle;
    }

}
