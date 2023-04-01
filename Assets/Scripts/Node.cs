using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector] public GameObject turret;
    [HideInInspector] public TurretBlueprint turretBlueprint;
    [HideInInspector] public bool isUpgraded = false;
    
    private Renderer _renderer;
    private Color _startColor;

    private BuildManager _buildManager;
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _startColor = _renderer.material.color;
        
        _buildManager = BuildManager.Instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {        
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (turret != null)
        {
            _buildManager.SelectNode(this);
            return;
        }
        
        if (!_buildManager.CanBuild)
            return;

        // Build a turret
        BuildTurret(_buildManager.GetTurretToBuild());
    }

    private void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;
        
        GameObject turretInstance = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = turretInstance;

        turretBlueprint = blueprint;

        GameObject effect = Instantiate(_buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that!");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;
        
        // Get rid of the old turret
        Destroy(turret);
        
        // Build a new turret (upgraded)
        GameObject turretInstance = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = turretInstance;

        GameObject effect = Instantiate(_buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;
        
        Debug.Log("Turret Upgraded!");
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
            
        if (!_buildManager.CanBuild)
            return;

        if (_buildManager.CanAfford)
            _renderer.material.color = hoverColor;
        else
            _renderer.material.color = notEnoughMoneyColor;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _startColor;
    }
}
