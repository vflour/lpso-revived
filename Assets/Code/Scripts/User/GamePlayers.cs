using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayers : MonoBehaviour
{
    public User localUser;
    public static GamePlayers Instance { private set; get; }

    public void Start()
    {
        Instance = this;
    }
}
