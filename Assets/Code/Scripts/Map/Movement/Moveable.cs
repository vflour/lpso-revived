using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public enum MovementState {
    Idle,
    Moving,
    Resting,
    Busy
}

public class Moveable : MonoBehaviour
{
    public float movementSpeed = 1f;
    public MapMovement mapMovement;
    public MovementState State { set; get; }

    private Vector3Int _currentCoordinates;
    public Vector3Int NextCoordinates {private set; get;}
    public Vector3 NextPosition {private set; get;}

    public Vector3Int coordinates
    {
        get
        {
            return _currentCoordinates;
        }

        set
        {
            _currentCoordinates = value;
            NextCoordinates = value;
            NextPosition = mapMovement.GetPosition(value);
            FinalCoordinates = value;

            transform.position = NextPosition;
        }
    }

    public Vector3Int FinalCoordinates { private set; get; }
    public Vector3 FinalPosition => mapMovement.GetPosition(FinalCoordinates);

    public bool IsMoving => _currentCoordinates != NextCoordinates;
    public bool IsNavigating => _currentCoordinates != FinalCoordinates;

    public bool HasChangedDirection(Vector3Int coordinates)
    {
        return FinalCoordinates != coordinates;
    }
    
    private IEnumerator NavigateThroughList(List<Vector3Int> path)
    {
        // the final coords is the last node
        FinalCoordinates = path[path.Count-1];
        State = MovementState.Moving;

        // cycle through all nodes
        foreach (Vector3Int coordinate in path)
        {   
            // move to next tile
            NextPosition = mapMovement.GetPosition(coordinate);
            NextCoordinates = coordinate;
            yield return new WaitUntil(() => !IsMoving);

            // path has changed
            if (HasChangedDirection(path[path.Count - 1]))
                yield break;
        }
        State = MovementState.Idle;

    }


    public void Navigate(List<Vector3Int> path)
    {
        StartCoroutine(NavigateThroughList(path));
    }


    // Update the Moveable's position on update
    public void FixedUpdate()
    {

        if (IsMoving && State == MovementState.Moving)
        {

            float step = movementSpeed * Time.deltaTime;
            Vector3 position = Vector3.MoveTowards(transform.position, NextPosition, step);
            if((NextPosition - position).magnitude < 0.0001)
                _currentCoordinates = NextCoordinates;
            
            transform.position = position;

        }

    }
}
