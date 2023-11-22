using App.Upgrades;
using App.World.Entity.Player.PlayerComponents;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeChoiceItem : MonoBehaviour
{
    [SerializeField] private Image upgradeImage;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Button chooseButton;

    private BaseUpgradeScriptableObject<Player> currentUpgrade;
    public event Action<BaseUpgradeScriptableObject<Player>> OnUpgradeCompleted;

    public UpgradeManager PlayerUpgradeManager 
    { 
        private get; 
        set; 
    }

    public UnityEvent OnChosen => chooseButton.onClick;

    public BaseUpgradeScriptableObject<Player> CurrentUpgrade
    {
        get
        {
            return currentUpgrade;
        }
        set
        {
            currentUpgrade = value;

            if (currentUpgrade != null)
            {
                DisplayUpgrade(currentUpgrade);
                AttachToChooseButton(currentUpgrade);
                chooseButton.gameObject.SetActive(true);
            }
            else
            {
                DisplayBlank();
                chooseButton.gameObject.SetActive(false);
            }
        }
    }

    private void Awake()
    {
        CurrentUpgrade = null;
    }

    private void OnDisable()
    {
        chooseButton.onClick.RemoveAllListeners();
    }

    private void AttachToChooseButton(BaseUpgradeScriptableObject<Player> upgrade)
    {
        chooseButton.onClick.RemoveAllListeners();
        chooseButton.onClick.AddListener(() => ApplyUpgradeToPlayer(PlayerUpgradeManager));
    }

    private void ApplyUpgradeToPlayer(UpgradeManager manager)
    {
        var foundUpgrade = manager.FindUpgrade(CurrentUpgrade); 

        if (foundUpgrade == null) 
        {
            manager.AddUpgrade(currentUpgrade);
        }
        else
        {
            manager.LevelUpUpgrade(foundUpgrade);
        }

        if (currentUpgrade.IsComplete)
        {
            OnUpgradeCompleted?.Invoke(currentUpgrade);
        }
    }

    private void DisplayUpgrade(IDisplayableUpgrade upgrade)
    {
        upgradeImage.sprite = upgrade.Image;
        description.text = upgrade.Description;
    }

    private void DisplayBlank()
    {
        upgradeImage.sprite = null;
        description.text = "";
    }
}
