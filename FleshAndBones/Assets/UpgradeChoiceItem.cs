using App.Upgrades;
using App.World.Entity.Player.PlayerComponents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeChoiceItem : MonoBehaviour
{
    [SerializeField] private Image upgradeImage;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Button chooseButton;

    [SerializeField] private BaseUpgradeScriptableObject<Player> upgrade;

    private void Start()
    {
        DisplayUpgrade(upgrade);
    }

    public void DisplayUpgrade(IDisplayableUpgrade upgrade)
    {
        upgradeImage.sprite = upgrade.Image;
        description.text = upgrade.Description;
    }
}
