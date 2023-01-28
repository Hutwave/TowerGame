using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPathfinding: MonoBehaviour
{

    public Vector2Int startCoordinates { get; set; }
    public Vector2Int destinationCoordinates { get; set; }

    Node startNode;
    Node destinationNode;
    Node currentSearchNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.up, Vector2Int.down, Vector2Int.left };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager != null)
        {
            grid = gridManager.EmptyGrid();
        }
    }

    public List<Node> GetUnitPath(Vector2Int coordinates)
    {
        Debug.Log(coordinates);
        Debug.Log(grid.ToString());
        startNode = grid[coordinates];
        destinationNode = grid[destinationCoordinates];
        unitSearch(coordinates);
        return BuildPath();
    }

    void ExploreNeighbors(bool unit = false)
    {
        List<Node> neighbors = new List<Node>();
        foreach(Vector2Int dir in directions)
        {
            Vector2Int neighborCoords = currentSearchNode.coordinates + dir;

            if (grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
            }
        }

        foreach(Node neighbor in neighbors)
        {
            if(!reached.ContainsKey(neighbor.coordinates) && (unit || neighbor.isWalkable))
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }


    void unitSearch(Vector2Int coordinates)
    {
        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors(true);
            if (currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }


    public List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();
        return path;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}
