using App.Upgrades;
using UnityEngine;

[RequireComponent(typeof(Old_IUpgradable))]
public class UpgradeTester : MonoBehaviour
{
    [SerializeField]
    private Old_BaseUpgrade _upgradeToTest;

    [SerializeField]
    private KeyCode _enableUpgradeKey;

    [SerializeField]
    private KeyCode _disableUpgradeKey;

    private Old_IUpgradable _upgradable;

    public void Awake()
    {
        _upgradable = GetComponent<Old_IUpgradable>();

        Debug.Log($"Upgrade Tester: enable upgrade key is set to {_enableUpgradeKey}");
        Debug.Log($"Upgrade Tester: disable upgrade key is set to {_disableUpgradeKey}");

        if (_upgradeToTest == null)
        {
            Debug.Log("Upgrade Tester: upgrade to test is not set.");
        }
    }

    public void Update()
    {
        if (_upgradeToTest == null)
        {
            return;
        }

        if (Input.GetKeyDown(_enableUpgradeKey) && !_upgradeToTest.IsEnabled)
        {
            Debug.Log($"Upgrade enabled: {_upgradeToTest.GetType()}");
            _upgradable.EnableUpgrade(_upgradeToTest);
        }

        if (Input.GetKeyDown(_disableUpgradeKey) && _upgradeToTest.IsEnabled)
        {
            Debug.Log($"Upgrade disabled: {_upgradeToTest.GetType()}");
            _upgradable.DisableUpgrade(_upgradeToTest);
        }
    }
}
