using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform cameraTransform;
    public float moveSpeed;
    public float moveTime;
    public Vector3 zoomAmount;
    public float rotationAmount;

    private Vector3 newPos;
    private Quaternion newRotation;
    private Vector3 newZoom;

    private Vector3 dragStart;
    private Vector3 dragCurrent;

    private BuildManager buildManager;

    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
        newPos = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        HandleMovementInput();
        HandleMouseInput();

    }

    void HandleMouseInput()
    {
        if(Input.mouseScrollDelta.y != 0)
        {
            if((newZoom.y < -40 && Input.mouseScrollDelta.y < 0) || (newZoom.y > -68 && Input.mouseScrollDelta.y > 0))
            {
                newZoom += Input.mouseScrollDelta.y * new Vector3(0, zoomAmount.y * 20, zoomAmount.z * 20);
            }
            
        }

        if (Input.GetMouseButtonDown(1))
        {
            buildManager.UnselectTower();
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if(plane.Raycast(ray, out entry))
            {
                dragStart = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(1))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrent = ray.GetPoint(entry);

                newPos = transform.position + dragStart - dragCurrent;
            }
        }
    }

    void HandleMovementInput()
    {

        if (Input.GetKey(KeyCode.W))
        {
            newPos += transform.forward * moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            newPos += transform.right * -moveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newPos += transform.forward * -moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newPos += transform.right * moveSpeed;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        if (Input.GetKey(KeyCode.R) && cameraTransform.position.y > 25)
        {
            newZoom += (zoomAmount);
        }
        if (Input.GetKey(KeyCode.F) && cameraTransform.position.y < 50)
        {
            newZoom -= zoomAmount;
        }

        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * moveTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * moveTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * moveTime*2f);
    }

}
