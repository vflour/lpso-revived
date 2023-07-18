using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class RestObjectClickable : ObjectClickable
{   

    public override void handle(Vector3 globalPosition, Vector3 mousePosition)
    {
        base.handle(globalPosition, mousePosition);
        if (_moveable) RestWhenReached();
    }
    
    private IEnumerator RestWhenReached()
    {
        yield return WaitForMoveable();
        if (_moveable.HasChangedDirection(endCoordinates)) yield break;
        PlaceMoveable(MovementState.Resting, characterPosition.position, characterCoordinates);
        PlayAnimation();
    }

}
