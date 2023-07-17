using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton-like class that allows for easy navigation between map components during runtime
public class GameMap : MonoBehaviour
{
    // Static refs to components
    public static MapSpawn Spawn { set; get;}
    public static MapMovement Movement { set; get;}

    void Awake()
    {
        Spawn = GetComponentInChildren<MapSpawn>();
        Movement = GetComponentInChildren<MapMovement>();
    }

    void OnDestroy()
    {
        Spawn = null;
        Movement = null;
    }
}
