using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    public TextMeshProUGUI upgradeCost;
    public Button upgradeButton;

    public TextMeshProUGUI sellAmount;

    private Node _target;

    public void SetTarget(Node target)
    {
        _target = target;

        transform.position = target.GetBuildPosition();


        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "MAXED";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();


        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        _target.UpgradeTurret();
        BuildManager.Instance.DeselectNode();
    }

    public void Sell()
    {
        _target.SellTurret();
        BuildManager.Instance.DeselectNode();
    }
}
