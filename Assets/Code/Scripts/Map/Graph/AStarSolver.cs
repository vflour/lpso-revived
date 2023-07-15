using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AStarSolver : MapSolver
{
    private const int MAX_SOLVER_RETRIES = 1000;
    private const int NEIGHBOR_DIST = 1;

    private List<MapNode> ReconstructPath(Dictionary<MapNode, MapNode> cameFrom, MapNode current)
    {

        var path = new List<MapNode> { current };

        // the cameFrom tree serves as a linked list
        // and it will reconstruct the path until there are no more links
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Insert(0, current);
        }

        return path;

    }

    /// <summary>
    /// The heuristic guess attemps to get the manhattan distance of a node from the other node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="end"></param>
    /// <returns>The manhattan distance between node and end</returns>
    private float HeuristicGuess(MapNode node, MapNode end)
    {

        var hX = Mathf.Abs(node.coordinates.x - end.coordinates.x);
        var hY = Mathf.Abs(node.coordinates.y - end.coordinates.y);

        return hX + hY;

    }

    public override List<MapNode> Solve(MapNode start, MapNode end)
    {

        // Openset basically determines the list of possible nodes that the algorithm may traverse through
        var openSet = new List<MapNode>() { start };
        // Keep track of a list of nodes that originate from one another
        var cameFrom = new Dictionary<MapNode, MapNode>();

        // Gscore keeps track of the distance from the goal
        var gScore = new Dictionary<MapNode, float>();
        gScore[start] = 0;

        // fScore stores the distance with a heuristic guess
        // of the distance from the goal
        var fScore = new Dictionary<MapNode, float>();
        fScore[start] = HeuristicGuess(start, end);

        // Avoid an infinite loop
        int timeout = 0;
        while (openSet.Count > 0 && timeout < MAX_SOLVER_RETRIES)
        {
            timeout++;

            // Get the node with the lowest fScore value
            var current = openSet.First();
            foreach (var node in openSet)
            {
                float fValue = fScore.GetValueOrDefault(node, Mathf.Infinity);
                if (fValue < fScore[current])
                    current = node;
            }
            openSet.Remove(current);

            // If you've reached the goal then you can abort and reconstruct the path
            if (current == end)
                return ReconstructPath(cameFrom, current);

            // For each neighbor
            foreach (var node in current.neighbors)
            {
                if (node != null)
                {
                    // Try to get the distance
                    // And see if it's a shorter path
                    float neighborGValue = gScore.GetValueOrDefault(node, Mathf.Infinity);
                    float currentGValue = gScore.GetValueOrDefault(current, Mathf.Infinity);
                    var tentativeScore = currentGValue + (int) node.nodeType;

                    if (tentativeScore < neighborGValue)
                    {
                        // Tentative score stacks on top of eachother
                        // (current's tentative score + the new weight = higher score)
                        // So that it intrisically will try to get the shortest path
                        // When you set gScore and fScore
                        cameFrom[node] = current;
                        gScore[node] = tentativeScore;
                        fScore[node] = tentativeScore + HeuristicGuess(node, end);
                        if (!openSet.Contains(node))
                            openSet.Add(node);
                    }
                }

            }

        }
        
        // WTF? It timed out?!
        if (timeout >= MAX_SOLVER_RETRIES)
            throw new System.ArithmeticException("Solving the path with A* took too long, aborting...");

        // Yeah, this shouldn't happen either.
        throw new System.ArithmeticException("Unable to solve A* algorithm");

    }
}
