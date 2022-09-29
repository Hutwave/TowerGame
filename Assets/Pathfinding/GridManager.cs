using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public Vector2Int gridSize;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    int UnityGridSize = 10;


    void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }
        return null;
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }

    public void ResetNodes()
    {
        foreach(KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        return new Vector2Int
        {
            x = Mathf.RoundToInt(position.x / 10),
            y = Mathf.RoundToInt(position.z / 10)
        };
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        return new Vector3
        {
            x = coordinates.x * 10,
            z = coordinates.y * 10,
        };
    }


    void CreateGrid()
    {
        for(int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }
    }
}
