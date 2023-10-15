using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton-like class that retrieves user data during runtime
public class GamePlayers : MonoBehaviour
{
    public static User LocalUser { set; get; }
    
    void Awake()
    {
        LocalUser = GetComponentInChildren<User>();
    }

    void OnDestroy()
    {
        LocalUser = null;
    }
}
