using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{

    public Material yks;
    public Material kaks;
    public UnitMover unitMover;

    public Material[] origMat;

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
                        var aa = joku.TryGetComponent<MeshRenderer>(out var aaa);
                        if (aa) aaa.material = kaks;
                    }
                }
                _selection = null;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("AAAAAAAAA");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Selectable"))
                {
                    var selection = hit.transform;

                    if (_selection != null && ((_selection.parent.CompareTag("Unit") && selection.parent.CompareTag("Tower")) || (_selection.parent.CompareTag("Tower") && selection.parent.CompareTag("Unit"))))
                    {
                        var gridManager = FindObjectOfType<GridManager>();
                        var torni = _selection.parent.CompareTag("Tower") ? _selection.parent : selection.parent;
                        var unitti = _selection.parent.CompareTag("Unit") ? _selection.parent : selection.parent;
                        Debug.Log("Täs");
                        Debug.Log(gridManager.GetCoordinatesFromPosition(unitti.position).ToString());
                        Debug.Log(gridManager.GetCoordinatesFromPosition(torni.position).ToString());
                        var towerSpace = torni.GetComponent<Tower>().getSpace();
                        var unitSize = unitti.GetComponent<UnitStats>().towerSize;
                        unitMover = unitti.GetComponent<UnitMover>();
                        if(gridManager.GetCoordinatesFromPosition(unitti.position).ToString() == gridManager.GetCoordinatesFromPosition(torni.position).ToString())
                        {
                            // Todo: Ilmotus, on jo tässä tornissa.
                            unSelect();
                            return;
                        }
                        if (towerSpace < unitSize)
                        {
                            // todo: Ilmoitus: Ei mahu
                            unSelect();
                            return;
                            
                        }
                        unitMover.RecalculatePath(torni, unitti);
                        unSelect();
                        return;
                    }
                    
                }
            }
            unSelect();
            return;
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
                    var selected = selection.parent.GetComponentsInChildren<Transform>();
                    
                    if (_selection != null && _selection.transform != selection.transform)
                    {
                        unSelect();
                    }

                    origMat = new Material[selected.Length];
                    var ii = 0;
                    foreach (var editOne in selected)
                    {
                        if(editOne.tag == "Untagged")
                        {
                            var foundOne = editOne.TryGetComponent<MeshRenderer>(out var foundMat);
                            if (foundOne)
                            {
                                origMat[ii] = foundMat.material;
                                ii++;
                                foundMat.material = yks;
                            }
                        }
                    }
                                       
                    _selection = selection;

                }
            }
        }
    }

    void unSelect()
    {
        if(_selection == null)
        {
            return;
        }
        var editable = _selection.parent.GetComponentsInChildren<Transform>();
        var ii = 0;
        foreach(var editOne in editable)
        {
            if(editOne.tag == "Untagged")
            {
                var didFind = editOne.TryGetComponent<MeshRenderer>(out var found);
                if (didFind)
                {
                    found.material = origMat[ii];
                    ii++;
                }
            }
        }
        Debug.LogError(origMat.Length);
        _selection = null;
        return;
    }
}
