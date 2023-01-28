using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerMover : MonoBehaviour
{
    public float speed = 1f;
    public List<Node> path = new List<Node>();
    float checkEverySec = 0f;
    float actualSpeed;

    Runner runner;
    GridManager gridManager;
    Pathfinder pathfinder;
    RunnerHealth runnerHealth;

    private void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void Awake()
    {
        runner = FindObjectOfType<Runner>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
        runnerHealth = FindObjectOfType<RunnerHealth>();
        actualSpeed = speed;
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates;

        if (resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    private void Update()
    {
        checkEverySec += Time.deltaTime;
        if(checkEverySec > 1f)
        {
            checkEverySec -= 1f;
            actualSpeed = speed * (1f-runnerHealth.applySlow());
            Debug.Log(actualSpeed);
        }
    }

    void FinishPath()
    {
        runner.PenaltyMoney();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        for(int i = 0; i < path.Count; i++)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;
            transform.LookAt(endPos);

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * actualSpeed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }

}
