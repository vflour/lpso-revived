using UnityEngine;

/// <summary>
/// Contains map movement methods for clickables in the map
/// </summary>
public abstract class MapClickable: Clickable
{

    public MapMovement mapMovement;
    internal Moveable _moveable;

    protected bool CanMove => _moveable.State != MovementState.Busy;

    public virtual void Awake()
    {
        mapMovement = GameMap.Movement;
    }

    public virtual void Start()
    {
        _moveable = GamePlayers.LocalUser.character.moveable;
    }

    protected void MoveToCoordinates(Vector3Int coordinates)
    {
        if (CanMove)
            mapMovement.MoveTo(_moveable, coordinates);
    }

}
