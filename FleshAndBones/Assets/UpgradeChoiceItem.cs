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
    [SerializeField] private TMP_Text upgradeName;
    [SerializeField] private TMP_Text description;

    private UnityEvent onClick;

    private BaseUpgradeScriptableObject<Player> currentUpgrade;
    public event Action<BaseUpgradeScriptableObject<Player>> OnUpgradeCompleted;

    public UpgradeManager PlayerUpgradeManager 
    { 
        private get; 
        set; 
    }

    public UnityEvent OnChosen => onClick;

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
            }
            else
            {
                DisplayBlank();
            }
        }
    }

    private void Awake()
    {
        CurrentUpgrade = null;
    }

    private void OnDisable()
    {
        OnChosen?.RemoveAllListeners();
    }

    private void AttachToChooseButton(BaseUpgradeScriptableObject<Player> upgrade)
    {
        OnChosen?.RemoveAllListeners();
        OnChosen?.AddListener(() => ApplyUpgradeToPlayer(PlayerUpgradeManager));
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
        upgradeImage.gameObject.SetActive(true);
        upgradeImage.sprite = upgrade.Image;
        upgradeName.text = upgrade.Name;
        description.text = upgrade.Description;
        onClick = new UnityEvent();
    }

    private void DisplayBlank()
    {
        upgradeImage.gameObject.SetActive(false);
        upgradeName.text = "";
        description.text = "";
        onClick = null;
    }

    public void OnClickEvent() => OnChosen?.Invoke();
}
