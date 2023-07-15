using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class for an algorithm that attempts to find the path between two nodes in a map's graph. 
/// </summary>

public abstract class MapSolver: MonoBehaviour
{
    /// <summary>
    /// Attempts to solve a path to the end Node.
    /// Throws an ArithmeticException if it can't solve the path.
    /// </summary>
    /// <exception cref="ArithmeticException">Thrown if it can't solve the path.</exception>
    /// <param name="start">The start node</param>
    /// <param name="end">The node it's trying to reach</param>
    /// <returns>A list of nodes that forms a path from the start node to the end node</returns>
    abstract public List<MapNode> Solve(MapNode start, MapNode end);
}
