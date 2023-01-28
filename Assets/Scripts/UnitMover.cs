using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    public float speed = 1f;
    public List<Node> path = new List<Node>();

    GridManager gridManager;
    UnitPathfinding pathfinder;
    TargetLocator targetLocator;

    Transform _tower;
    Transform _unit;


    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = gameObject.AddComponent<UnitPathfinding>();
        targetLocator = gameObject.GetComponent<TargetLocator>();
    }

    public void RecalculatePath(Transform tower, Transform unit)
    {
        _tower = tower;
        _unit = unit;
        pathfinder.startCoordinates = gridManager.GetCoordinatesFromPosition(unit.position);
        pathfinder.destinationCoordinates = gridManager.GetCoordinatesFromPosition(tower.position);
        Debug.Log(tower.position.x + " " + tower.position.z + " - " + pathfinder.startCoordinates.x + " " + pathfinder.startCoordinates.y + "-" + pathfinder.destinationCoordinates.x + " " + pathfinder.destinationCoordinates.y);
        StopAllCoroutines();
        path.Clear();
        Debug.Log(unit.position);
        Debug.Log(gridManager.name);
        path = pathfinder.GetUnitPath(gridManager.GetCoordinatesFromPosition(unit.position));
        StartCoroutine(FollowPath());
    }


    void FinishPath()
    {
        targetLocator.enabled = true;
        goToTower();
    //    gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        for (int i = 0; i < path.Count; i++)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;
            transform.LookAt(endPos);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }


    public void goToTower()
    {
        var joku3 = _tower.GetComponentsInChildren<Transform>();
        foreach (var joku in joku3)
        {
            if (joku.tag == "Untagged")
            {
                var aa = joku.TryGetComponent<MeshRenderer>(out var aaa);
                //aaa.TryGetComponent<Emissi>
                //aa.material = kaks;
            }
        }

        Vector3 tryPlace = _tower.transform.position;

        foreach (Transform child in _tower)
        {
            
            if (child.CompareTag("UnitHolder"))
            {
                var towerX = child.transform.lossyScale.x / 2.2f;
                var towerZ = child.transform.lossyScale.z / 2.2f;
                var towerNeeded = _unit.GetComponent<UnitStats>().towerSize;
                var towerSize = 50; // TEMP !!! ! !!! ! !!! ! !

                if (towerNeeded == towerSize)
                {
                    tryPlace = new Vector3(child.position.x, child.position.y + 0.5f, child.position.z);
                }
                else if (towerNeeded * 2 > towerSize)
                {
                    bool successfullyPlaced = false;
                    int a = 0;
                    while (a < 300 && !successfullyPlaced)
                    {
                        a++;
                        var multiplier = Random.Range(0.33f, 0.55f);
                        var minusX = Random.Range(0, 2) == 0 ? 1 : -1;
                        var minusZ = Random.Range(0, 2) == 0 ? 1 : -1;
                        tryPlace = new Vector3(child.position.x + (multiplier * towerX * minusX), child.position.y + 0.5f, child.position.z + (multiplier * towerZ * minusZ));
                        successfullyPlaced = !Physics.CheckSphere(tryPlace, 1.2f, LayerMask.GetMask("Unit"));
                    }
                }
                else if (towerNeeded * 4 > towerSize)
                {
                    bool successfullyPlaced = false;
                    int a = 0;
                    while (a < 400 && !successfullyPlaced)
                    {
                        a++;
                        var multiplier = Random.Range(0.25f, 0.65f);
                        var minusX = Random.Range(0, 2) == 0 ? 1 : -1;
                        var minusZ = Random.Range(0, 2) == 0 ? 1 : -1;
                        tryPlace = new Vector3(child.position.x + (multiplier * towerX * minusX), child.position.y + 0.5f, child.position.z + (multiplier * towerZ * minusZ));
                        successfullyPlaced = !Physics.CheckSphere(tryPlace, 1f, LayerMask.GetMask("Unit"));
                    }
                }
                else
                {
                    bool successfullyPlaced = false;
                    int a = 0;
                    while (a < 500 && !successfullyPlaced)
                    {
                        a++;
                        var placeX = Random.Range(child.position.x - towerX, child.position.x + towerX * 0.9f);
                        var placeZ = Random.Range(child.position.z - towerZ, child.position.z + towerZ * 0.9f);
                        tryPlace = new Vector3(placeX, child.position.y + 0.5f, placeZ);
                        successfullyPlaced = !Physics.CheckSphere(tryPlace, 0.8f, LayerMask.GetMask("Unit"));
                    }
                }
            }
        }

        _unit.gameObject.transform.position = tryPlace;
    }
}
