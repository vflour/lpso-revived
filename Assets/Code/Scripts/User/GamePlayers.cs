using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton-like class that retrieves user data during runtime
public class GamePlayers : MonoBehaviour
{
    public static User LocalUser { set; get; }
    
    public void Awake()
    {
        LocalUser = GetComponentInChildren<User>();
    }
}
