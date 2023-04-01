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


    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < _turretToBuild.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= _turretToBuild.cost;
        
        GameObject turret = Instantiate(_turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        GameObject effect = Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        
        Debug.Log("Turret Built! Money Left: " + PlayerStats.Money);
    }

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
}
