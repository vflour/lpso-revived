using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Orientable : MonoBehaviour
{
    public Moveable moveable;
    public UnityEvent<float> handleRotation;
    private const float RADIANS_CIRCLE = Mathf.PI*2;
    private const float OFFSET = RADIANS_CIRCLE*0.125f; 

    public Vector3Int DirectionVector => moveable.NextCoordinates - moveable.coordinates; 

    public void Update()
    {
        if (moveable.IsNavigating)
        {
            float linearDirection = Mathf.Repeat(Mathf.Atan2(DirectionVector.x, DirectionVector.y) + RADIANS_CIRCLE, RADIANS_CIRCLE); 
            float direction = linearDirection - OFFSET;
            handleRotation.Invoke(-direction);
        }
        else {
            handleRotation.Invoke(0);
        }

    }
}
