using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;

    private GameObject _turret;

    private Renderer _renderer;
    private Color _startColor;

    private BuildManager _buildManager;
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _startColor = _renderer.material.color;
        
        _buildManager = BuildManager.Instance;
    }

    private void OnMouseDown()
    {        
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (_buildManager.GetTurretToBuild() == null)
            return;

        if (_turret != null)
        {
            Debug.Log("Can't build there - TODO: Display on screen.");
            return;
        }
        
        // Build a turret
        GameObject turretToBuild = BuildManager.Instance.GetTurretToBuild();
        _turret = Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
            
        if (_buildManager.GetTurretToBuild() == null)
            return;
        
        _renderer.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _startColor;
    }
}
