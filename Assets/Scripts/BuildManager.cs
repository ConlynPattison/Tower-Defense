using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one BuildManager in scene.");
            return;
        }

        Instance = this;
    }

    public GameObject buildEffect;

    private TurretBlueprint _turretToBuild;
    private Node _selectedNode;

    public NodeUI nodeUI;

    public bool CanBuild => _turretToBuild != null;
    public bool CanAfford => PlayerStats.Money >= _turretToBuild.cost;

    public void SelectNode(Node node)
    {
        if (node == _selectedNode)
        {
            DeselectNode();
            return;
        }
        
        _selectedNode = node;
        _turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        _selectedNode = null;
        nodeUI.Hide();
    }
    
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        _turretToBuild = turret;
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return _turretToBuild;
    }
}
