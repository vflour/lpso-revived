using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CursorType
{
    Idle,
    Interact,
    Walk,
    Deny
}

[CreateAssetMenu]
public class CursorTypesPrefabs : ScriptableObject
{
    public List<GameObject> cursors;       
}
