using SuperTiled2Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapNodeList : MonoBehaviour
{
    public BoundsInt mapBounds;
    public Tilemap tilemap;
    public Dictionary<Vector3Int, MapNode> nodes = new Dictionary<Vector3Int, MapNode>();
    private static readonly Vector3Int[] neighborComputations =
    {
        new Vector3Int( 1,   0),    // right
        new Vector3Int(-1,   0),    // left
        new Vector3Int( 0,   1),    // top
        new Vector3Int( 0,  -1),    // bottom
        new Vector3Int( 1,   1),    // right top
        new Vector3Int(-1,   1),    // left top
        new Vector3Int( 1,  -1),    // right bottom
        new Vector3Int(-1,  -1),    // left bottom

    };

    void Awake()
    {

        mapBounds = tilemap.cellBounds;

        // get all of the valid coordinates first and generate a list of nodes
        var validCoordinates = new Dictionary<MapNode, Vector3Int[]>();

        // Go through bounds
        for (int x = mapBounds.xMin; x <= mapBounds.xMax; x++)
        {
            for (int y = mapBounds.yMin; y <= mapBounds.yMax; y++)
            {
                // Check if the tile exists with the coordinates & create a node
                Vector3Int coordinates = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(coordinates))
                {
                    // get collision data
                    SuperTile tile = tilemap.GetTile<SuperTile>(coordinates);

                    string layer = tile.m_CustomProperties.Find(prop => prop.m_Name == "collisionType").GetValueAsString();
                    MapNode.MapNodeType nodeType;
                    Enum.TryParse(layer, out nodeType);

                    MapNode node = new MapNode(coordinates, nodeType);
                    validCoordinates[node] = new Vector3Int[8];
                    nodes[coordinates] = node;

                    // Try to see if it has any neighbors
                    for (int i = 0; i < neighborComputations.Length; i++)
                    {
                        Vector3Int neighborCoordinates = coordinates + neighborComputations[i];
                        if (tilemap.HasTile(neighborCoordinates))
                            validCoordinates[node][i] = neighborCoordinates;
                    }
                }
            }
        }

        // Populate neighbors
        foreach (var kvp in validCoordinates)
        {
            var node = kvp.Key;
            for (int i = 0; i < kvp.Value.Length; i++)
            {
                var neighborCoordinates = kvp.Value[i];
                if (tilemap.HasTile(neighborCoordinates))
                    node.neighbors[i] = nodes[neighborCoordinates];
            }
        }

    }
}
