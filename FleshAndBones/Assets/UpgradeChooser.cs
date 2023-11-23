using App.Upgrades;
using App.World.Entity.Player.Events;
using App.World.Entity.Player.PlayerComponents;
using App.World.UI;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradeChooser : MonoBehaviour
{
    [SerializeField] private GameObject view;
    [SerializeField] private UpgradeChoiceItem item1;
    [SerializeField] private UpgradeChoiceItem item2;
    [SerializeField] private UpgradeChoiceItem item3;
    [SerializeField] private Pauser pauser;

    [SerializeField] private UpgradeManager playerUpgradeManager;
    [SerializeField] private Level playerLevel;

    [Space]
    [SerializeField] private List<BaseUpgradeScriptableObject<Player>> upgradesList;

    private float PreShownTimeScale { get; set; }

    private void Awake()
    {
        PreShownTimeScale = 0.0f;
        item1.PlayerUpgradeManager = playerUpgradeManager;
        item2.PlayerUpgradeManager = playerUpgradeManager;
        item3.PlayerUpgradeManager = playerUpgradeManager;
    }

    private void Start()
    {
        Appear();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    public void Appear()
    {
        pauser.enabled = false;
        SetNextUpgrades();
        Show(view);
        SubscribeEvents();
        StopTime();
    }

    public void Disappear()
    {
        pauser.enabled = true;
        Hide(view);
        RenewTime();
    }

    private void Show(GameObject go) => go.SetActive(true);

    private void Hide(GameObject go) => go.SetActive(false);

    private void StopTime()
    {
        PreShownTimeScale = Time.timeScale;
        Time.timeScale = 0f;
    }

    private void RenewTime()
    {
        Time.timeScale = 1f;
        PreShownTimeScale = 0.0f;
    }

    private (int, int, int) ChooseNextUpgradeIndices()
    {
        int max = upgradesList.Count;
        int random1 = Random.Range(0, max);
        int random2;
        int random3;

        do
        {
            random2 = Random.Range(0, max);
        } 
        while (random2 == random1);

        do
        {
            random3 = Random.Range(0, max);
        }
        while (random3 == random1 || random3 == random2);

        return (random1, random2, random3);
    }

    void SetNextUpgrades()
    {
        if (upgradesList.Count == 0)
        {
            return;
        }

        if (upgradesList.Count == 1)
        {
            item1.CurrentUpgrade = upgradesList[0];
            item2.CurrentUpgrade = null;
            item3.CurrentUpgrade = null;
        }

        if (upgradesList.Count == 2)
        {
            item1.CurrentUpgrade = upgradesList[0];
            item2.CurrentUpgrade = upgradesList[1];
            item3.CurrentUpgrade = null;
        }

        if (upgradesList.Count >= 3)
        {
            var (index1, index2, index3) = ChooseNextUpgradeIndices();
            var upgrade1 = upgradesList[index1];
            var upgrade2 = upgradesList[index2];
            var upgrade3 = upgradesList[index3];

            item1.CurrentUpgrade = upgrade1;
            item3.CurrentUpgrade = upgrade2;
            item2.CurrentUpgrade = upgrade3;
        }
    }

    private void OnUpgradeCompleted(BaseUpgradeScriptableObject<Player> upgrade)
    {
        upgradesList.Remove(upgrade);
    }

    private void OnLevelUp(ValueUpdateEvent ev, ValueUpdateEventArgs args)
    {
        Appear();
    }

    private void SubscribeEvents()
    {
        item1.OnUpgradeCompleted += OnUpgradeCompleted;
        item2.OnUpgradeCompleted += OnUpgradeCompleted;
        item3.OnUpgradeCompleted += OnUpgradeCompleted;

        item1.OnChosen?.AddListener(Disappear);
        item2.OnChosen?.AddListener(Disappear);
        item3.OnChosen?.AddListener(Disappear);

        playerLevel.OnLevelUp.OnValueUpdate += OnLevelUp;
    }

    private void UnsubscribeEvents()
    {
        item1.OnUpgradeCompleted -= OnUpgradeCompleted;
        item2.OnUpgradeCompleted -= OnUpgradeCompleted;
        item3.OnUpgradeCompleted -= OnUpgradeCompleted;

        item1.OnChosen?.RemoveListener(Disappear);
        item2.OnChosen?.RemoveListener(Disappear);
        item3.OnChosen?.RemoveListener(Disappear);

        playerLevel.OnLevelUp.OnValueUpdate -= OnLevelUp;
    }
}
