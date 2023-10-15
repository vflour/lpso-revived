using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Handles the positioning needed for map movement
/// </summary>
public class MapMovement : MonoBehaviour
{

    public MapNodeList mapNodes;
    public MapSolver solver;
    public Tilemap tilemap;

    /// <summary>
    /// Attempts to move a moveable to a new grid position via a solved path
    /// </summary>
    /// <param name="moveable"></param>
    /// <param name="position"></param>
    public void MoveTo(Moveable moveable, Vector3Int position)
    {
        List<MapNode> nodePath = solver.Solve(mapNodes.nodes[moveable.coordinates], mapNodes.nodes[position]);
        List<Vector3Int> path = nodePath.ConvertAll(node => node.coordinates);
        moveable.Navigate(path);
    }

    /// <summary>
    /// Gets the position of a coordinate
    /// </summary>
    /// <param name="coordinates"></param>
    /// <returns></returns>
    public Vector3 GetPosition(Vector3Int coordinates)
    {
        return tilemap.CellToWorld(coordinates) - tilemap.transform.position;
    }

    public bool CanMove(Vector3Int coordinates)
    {
        if (!tilemap.HasTile(coordinates)) 
        {
            return false;
        }
        return mapNodes.nodes[coordinates].nodeType != MapNode.MapNodeType.Collideable;
    }

}
