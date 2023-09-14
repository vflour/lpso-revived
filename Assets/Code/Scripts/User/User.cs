using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Component used to account for user interaction and behaviour 
/// </summary>
public class User : MonoBehaviour
{
    // References to external classes for easy navigation
    public Character character;
    public UserCamera userCamera;
    public MapSpawn spawner;
    public TooltipManager tooltipManager;
    public CursorManager cursorManager;
    public Tilemap tilemap;

    private bool _spawned = false;

    private void Spawn()
    {
        // UserGetter.CharacterData
        // UserGetter.PreviousLocation
        GameDataManager GameData = GameDataManager.Instance;
        Vector3Int spawnCoordinates = spawner.GetSpawnPoint();
        if(GameData.FreshSpawn == false){
            character.Spawn(tilemap.WorldToCell(new Vector3Int((int)GameData.OldLocation.x,(int)GameData.OldLocation.y,(int)GameData.OldLocation.z)));
        } else {
            character.Spawn(spawnCoordinates);
            GameData.FreshSpawn = false;
        }

        userCamera.RefreshCamera();
    }
    
    void Awake()
    {
        character = GetComponentInChildren<Character>();
        userCamera = GetComponentInChildren<UserCamera>();
        tooltipManager = GetComponentInChildren<TooltipManager>();
        cursorManager = GetComponentInChildren<CursorManager>();
    }

    void Start()
    {
        // Initialize external refs
        spawner = GameMap.Spawn;
        // the local user's movement should be the game map moveable
        character.moveable.mapMovement = GameMap.Movement;
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
