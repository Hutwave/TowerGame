using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{

    public Material yks;
    public Material kaks;
    private int towerSize = 50;
    public UnitMover unitMover;

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
                        var torni = _selection.parent.CompareTag("Tower") ? _selection.parent : selection.parent;
                        var unitti = _selection.parent.CompareTag("Unit") ? _selection.parent : selection.parent;
                        unitMover = unitti.GetComponent<UnitMover>();
                        unitMover.RecalculatePath(torni, unitti);
                        _selection = null;
                        selection = null;
                        return;
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
