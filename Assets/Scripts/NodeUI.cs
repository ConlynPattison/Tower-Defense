using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    
    private Node _target;

    public void SetTarget(Node target)
    {
        _target = target;

        transform.position = target.GetBuildPosition();
        
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }
}
