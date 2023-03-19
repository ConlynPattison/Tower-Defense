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

    public GameObject standardTurretPrefab;
    public GameObject missileLauncherPrefab;

    private GameObject _turretToBuild;

    public GameObject GetTurretToBuild()
    {
        return _turretToBuild;
    }

    public void SetTurretToBuild(GameObject turret)
    {
        _turretToBuild = turret;
    }
}
