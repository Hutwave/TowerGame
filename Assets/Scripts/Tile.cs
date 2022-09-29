using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates = new Vector2Int();

    public Tower towerPrefab;

    private void Awake()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        gridManager = FindObjectOfType<GridManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    void OnMouseDown()
    {

        if (1==2 && gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);
            if (isSuccessful)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

}

