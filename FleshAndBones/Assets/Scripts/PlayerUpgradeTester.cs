using App.Upgrades;
using App.World.Entity.Player.PlayerComponents;
using UnityEngine;

[RequireComponent(typeof(IUpgradable))]
public class PlayerUpgradeTester : MonoBehaviour
{
    [SerializeField]
    private BaseUpgradeScriptableObject<Player> upgradeToTest;

    [SerializeField]
    private KeyCode enableUpgradeKey;

    [SerializeField]
    private KeyCode disableUpgradeKey;

    [SerializeField]
    private KeyCode levelUpKey;

    private IUpgradable upgradable;

    public void Awake()
    {
        upgradable = GetComponent<IUpgradable>();

        Debug.Log($"Upgrade Tester: enable upgrade key is set to {enableUpgradeKey}");
        Debug.Log($"Upgrade Tester: disable upgrade key is set to {disableUpgradeKey}");

        if (upgradeToTest == null)
        {
            Debug.Log("Upgrade Tester: upgrade to test is not set.");
        }
    }

    public void Update()
    {
        if (upgradeToTest == null)
        {
            return;
        }

        if (Input.GetKeyDown(enableUpgradeKey) && !upgradeToTest.IsEnabled)
        {
            Debug.Log($"Upgrade enabled: {upgradeToTest.GetType()}");
            upgradable.EnableUpgrade(upgradeToTest);
        }

        if (Input.GetKeyDown(levelUpKey) && !upgradeToTest.IsComplete)
        {
            Debug.Log($"Upgrade leveled up: {upgradeToTest.GetType()}");
            upgradeToTest.LevelUp();
        }
      

        if (Input.GetKeyDown(disableUpgradeKey) && upgradeToTest.IsEnabled)
        {
            Debug.Log($"Upgrade disabled: {upgradeToTest.GetType()}");
            upgradeToTest.Disable();
        }
    }
}
