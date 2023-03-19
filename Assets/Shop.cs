using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private BuildManager _buildManager;

    private void Start()
    {
        _buildManager = BuildManager.Instance;
    }

    public void PurchaseStandardTurret()
    {
        Debug.Log("Standard Turret Selected");
        BuildManager.Instance.SetTurretToBuild(_buildManager.standardTurretPrefab);
    }
    
    public void PurchaseMissileLauncher()
    {
        Debug.Log("Missile Launcher Selected");
        BuildManager.Instance.SetTurretToBuild(_buildManager.missileLauncherPrefab);
    }
}
