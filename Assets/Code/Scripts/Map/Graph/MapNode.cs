using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode 
{
    public enum MapNodeType
    {
        Collideable = 9999,
        NonCollideable = 1,
        Spawn = 2,
    }

    public MapNode[] neighbors = new MapNode[8];
    public Vector3Int coordinates;
    public MapNodeType nodeType;

    public MapNode(Vector3Int coordinates, MapNodeType nodeType)
    {
        this.coordinates = coordinates;
        this.nodeType = nodeType;
    }

}
