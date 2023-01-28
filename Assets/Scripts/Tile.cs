using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }
    public bool isWalkable;

    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates = new();

    public Color hoverColor;
    private Color originalColor;

    private BuildManager buildManager;

    public Tower towerPrefab;

    private Transform editChild;

    private void Awake()
    {
        foreach(Transform child in transform)
        {
            if(child.name.ToLower().Contains("cube"))
                editChild = child;
        }
        pathfinder = FindObjectOfType<Pathfinder>();
        gridManager = FindObjectOfType<GridManager>();
        buildManager = BuildManager.instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //rend = GetComponent<Renderer>();
        originalColor = editChild.GetComponent<Renderer>().material.GetColor("_Color");
        if (gridManager != null)
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

        Debug.Log(coordinates.ToString());
        Debug.Log(gridManager.name);
        Debug.Log(pathfinder.name);
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            GameObject tempTower = buildManager.GetTurretToBuild();
            if(tempTower == null)
            {
                return;
            }
            var asd = tempTower.GetComponent<Tower>();
            bool isSuccessful = asd.CreateTower(asd, transform.position);
            //bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);
            if (isSuccessful)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    private void OnMouseEnter()
    {
        GameObject buildTower = buildManager.GetTurretToBuild();

        if (buildTower == null)
        {
            return;
        }
        if(editChild != null)
        {
            //Debug.Log(editChild.name);
            editChild.GetComponent<Renderer>().material.SetColor("_Color", hoverColor);
        }
    }

    private void OnMouseExit()
    {
        if (editChild != null)
        {
            editChild.GetComponent<Renderer>().material.SetColor("_Color", originalColor);
        }
    }


}

