using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class RestObjectClickable : Clickable
{
    public Transform characterPosition;
    public Transform endPosition;
    
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
            StartCoroutine(RestWhenReached(moveable, characterCoordinates));
        }
    }
    
    private IEnumerator RestWhenReached(Moveable moveable, Vector3Int characterCoordinates)
    {
        yield return new WaitUntil(()=>moveable.IsNavigating == false);
            
        moveable.State = MovementState.Resting;
        moveable.coordinates = characterCoordinates;
        moveable.transform.position = characterPosition.position;
    }
}
