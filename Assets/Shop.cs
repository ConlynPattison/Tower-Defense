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
    
    public void PurchaseAnotherTurret()
    {
        Debug.Log("Another Turret Selected");
        BuildManager.Instance.SetTurretToBuild(_buildManager.anotherTurretPrefab);
    }
}
