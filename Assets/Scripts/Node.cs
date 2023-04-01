using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [Header("Optional")]
    public GameObject turret;

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
        _buildManager.BuildTurretOn(this);
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
