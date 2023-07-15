using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component used to account for user interaction and behaviour 
/// </summary>
public class User : MonoBehaviour
{
    public Character character;
    public UserCamera userCamera;
    public MapSpawn spawner;
    private bool _spawned = false;

    private void Spawn()
    {
        // UserGetter.CharacterData
        // UserGetter.PreviousLocation
        Vector3Int spawnCoordinates = spawner.GetSpawnPoint(); 
        character.Spawn(spawnCoordinates);
        
        userCamera.RefreshCamera();
    }

    // Initialize AFTER Start is called
    void Update()
    {
        if (!_spawned) 
        {
            _spawned = true;
            // Might simplify if redundant
            Spawn();
        }
    }

}
