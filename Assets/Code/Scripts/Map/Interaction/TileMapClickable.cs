using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapClickable : MapClickable
{
    public TilemapClickAnimation clickAnimation;


    public override void handle(Vector3 globalPosition, Vector3 mousePosition)
    {
        if (_moveable) 
        {
            if (!CanMove) return;
            Tilemap tilemap = mapMovement.tilemap;
            Vector3 hitPoint = globalPosition + tilemap.transform.position/2;

            Vector3Int cellPosition = tilemap.WorldToCell(hitPoint);
            if (mapMovement.CanMove(cellPosition))
            {
                MoveToCoordinates(cellPosition);
                clickAnimation.SpawnGraphic(cellPosition);
            }
        }
    }
}
