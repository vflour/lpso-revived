using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class Moveable : MonoBehaviour
{
    public float movementSpeed = 1f;
    public MapMovement mapMovement;

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
    
    private IEnumerator NavigateThroughList(List<Vector3Int> path)
    {
        // the final coords is the last node
        FinalCoordinates = path[path.Count-1];
        // cycle through other nodes first
        foreach (Vector3Int coordinate in path)
        {
            // path has changed
            if (FinalCoordinates != path[path.Count-1])
                break;

            NextPosition = mapMovement.GetPosition(coordinate);
            NextCoordinates = coordinate;

            yield return new WaitUntil(() => !IsMoving);
        }

    }


    public void Navigate(List<Vector3Int> path)
    {
        StartCoroutine(NavigateThroughList(path));
    }


    // Update the Moveable's position on update
    public void FixedUpdate()
    {

        if (IsMoving)
        {

            float step = movementSpeed * Time.deltaTime;
            Vector3 position = Vector3.MoveTowards(transform.position, NextPosition, step);
            if((NextPosition - position).magnitude < 0.0001)
                _currentCoordinates = NextCoordinates;
            
            transform.position = position;

        }

    }
}
