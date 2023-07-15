using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A clickable map item that may result in any type of behaviour.
/// </summary>
public abstract class Clickable: MonoBehaviour
{
    /// <summary>
    /// Handler for clicking on the object
    /// </summary>
    /// <param name="globalPosition">The global position of the mouse when the click occurred</param>
    /// <param name="mousePosition">The position of the pointer relative to the camera when the click occurred</param>
    abstract public void handle(Vector3 globalPosition, Vector3 mousePosition);
}
