using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BusyObjectClickable : Clickable
{
    public Transform characterPosition;
    public Transform endPosition;
    public string animation;
    
    public override void handle(Vector3 globalPosition, Vector3 mousePosition)
    {
        MapMovement mapMovement = GameMap.Movement;
        Moveable moveable = GamePlayers.LocalUser.character.moveable;
        if (moveable) 
        {
            Tilemap tilemap = mapMovement.tilemap;
            Vector3 hitPoint = globalPosition + tilemap.transform.position/2;

            Vector3Int characterCoordinates = tilemap.WorldToCell(characterPosition.position);
            Vector3Int endCoordinates = tilemap.WorldToCell(endPosition.position);
            
            mapMovement.MoveTo(moveable, endCoordinates);
            StartCoroutine(AnimateWhenReached(moveable, characterCoordinates));
        }
    }
    
    private IEnumerator AnimateWhenReached(Moveable moveable, Vector3Int characterCoordinates)
    {
        yield return new WaitUntil(()=>moveable.IsNavigating == false);
        
        moveable.State = MovementState.Busy;
        moveable.coordinates = characterCoordinates;
        moveable.transform.position = characterPosition.position;

        var animator = GamePlayers.LocalUser.character.animator;
        animator.Play(animation);
        var animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);


        yield return new WaitUntil(()=>animatorStateInfo.length == animatorStateInfo.normalizedTime);
        Debug.Log("done");
        moveable.State = MovementState.Idle;
    }
}
