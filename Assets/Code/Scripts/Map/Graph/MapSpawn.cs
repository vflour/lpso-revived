using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapSpawn : MonoBehaviour
{
    public MapNodeList mapNodeList;

    private List<MapNode> _spawnList = new List<MapNode>();

    public UnityEvent SpawnReady;

    void Start()
    {
        foreach (KeyValuePair<Vector3Int, MapNode> kvp in mapNodeList.nodes)
        {
            MapNode node = kvp.Value;

            if (node.nodeType == MapNode.MapNodeType.Spawn)
            {
                _spawnList.Add(node);
            }
        }
        SpawnReady.Invoke();
    }

    public Vector3Int GetSpawnPoint()
    {
        return _spawnList[0].coordinates;
    }

}
