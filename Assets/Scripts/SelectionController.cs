using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{

    public Material yks;
    public Material kaks;
    private int towerSize = 50;

    private Transform _selection;
    // Update is called once per frame
    void Update()
    {

        if (1==2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var joku1 = _selection.parent.GetComponentsInChildren<Transform>();
                foreach (var joku in joku1)
                {
                    if (joku.tag == "Untagged")
                    {
                        Debug.Log(joku.name);
                        var aa = joku.TryGetComponent<MeshRenderer>(out var aaa);
                        if (aa) aaa.material = kaks;
                    }
                }
                _selection = null;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {

                if (hit.transform.CompareTag("Selectable"))
                {
                    var selection = hit.transform;
                    
                    if (_selection != null && ((_selection.parent.CompareTag("Unit") && selection.parent.CompareTag("Tower")) || (_selection.parent.CompareTag("Tower") && selection.parent.CompareTag("Unit"))))
                    {
                        var joku3 = _selection.parent.GetComponentsInChildren<Transform>();
                        foreach (var joku in joku3)
                        {
                            if (joku.tag == "Untagged")
                            {
                                Debug.Log(joku.name);
                                var aa = joku.TryGetComponent<MeshRenderer>(out var aaa);
                                if (aa) aaa.material = kaks;
                            }
                        }
                        var torni = _selection.parent.CompareTag("Tower") ? _selection.parent : selection.parent;
                        var unitti = _selection.parent.CompareTag("Unit") ? _selection.parent : selection.parent;
                        Vector3 tryPlace = torni.transform.position;

                        
                        foreach(Transform child in torni)
                        {
                            Debug.Log(child.name);
                            if (child.CompareTag("UnitHolder"))
                            {
                                var towerX = child.transform.lossyScale.x/2.2f;
                                var towerZ = child.transform.lossyScale.z/2.2f;
                                var towerNeeded = unitti.GetComponent<UnitStats>().towerSize;

                                if (towerNeeded == towerSize)
                                {
                                    tryPlace = new Vector3(child.position.x, child.position.y + 1.5f, child.position.z);
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
                                        tryPlace = new Vector3(child.position.x + (multiplier * towerX * minusX), child.position.y + 1.5f, child.position.z + (multiplier * towerZ * minusZ));
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
                                        tryPlace = new Vector3(child.position.x + (multiplier * towerX * minusX), child.position.y + 1.5f, child.position.z + (multiplier * towerZ * minusZ));
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
                                        var placeX = Random.Range(child.position.x - towerX, child.position.x + towerX*0.9f);
                                        var placeZ = Random.Range(child.position.z - towerZ, child.position.z + towerZ*0.9f);
                                        tryPlace = new Vector3(placeX, child.position.y + 1.5f, placeZ);
                                        successfullyPlaced = !Physics.CheckSphere(tryPlace, 0.8f, LayerMask.GetMask("Unit"));
                                    }
                                }
                            }
                        }

                        unitti.gameObject.transform.position = tryPlace;
                            
                        _selection = null;
                        selection = null;

                    }

                    var joku1 = selection.parent.GetComponentsInChildren<Transform>();
                    foreach (var joku in joku1)
                    {
                        if(joku.tag == "Untagged")
                        {
                            var aa = joku.TryGetComponent<MeshRenderer>(out var aaa);
                            if (aa) aaa.material = yks;
                        }
                    }
                    
                    if (_selection != null && _selection.transform != selection.transform)
                    {
                        var joku2 = _selection.parent.GetComponentsInChildren<Transform>();
                        foreach (var joku in joku2)
                        {
                            if (joku.tag == "Untagged")
                            {
                                var aa = joku.TryGetComponent<MeshRenderer>(out var aaa);
                                if (aa) aaa.material = kaks;
                            }
                        }                        
                    }
                    _selection = selection;

                }
            }
        }
    }
}
