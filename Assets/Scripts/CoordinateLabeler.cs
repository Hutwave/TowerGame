using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{

    Color defaultColor = Color.cyan;
    Color blockedColor = Color.red;
    Color exploredColor = Color.yellow;
    Color pathColor = Color.green;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

    // Start is called before the first frame update
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        DisplayCoordinates();
    }

    private void Start()
    {
        label.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled = true;
        }
        SetLabelColor();
        ToggleLabels();
    }

    void SetLabelColor()
    {
        if(gridManager == null)
        {
            return;
        }

        Node node = gridManager.GetNode(coordinates);

        if(node == null)
        {
            return;
        }
        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }

    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    void DisplayCoordinates()
    {
        try
        {
            coordinates.x = Mathf.RoundToInt(transform.parent.position.x);
            coordinates.y = Mathf.RoundToInt(transform.parent.position.z);
            label.text = $"{coordinates.x},{coordinates.y}";
        }
        catch
        {
            coordinates.x = 0;
            coordinates.y = 0;
        }

       
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}   
