using System.Collections.Generic;
using UnityEngine;


using App.Upgrades.Old_ConcreteUpgrades;


namespace App.Upgrades
{
    public class Old_UpgradeManager : MonoBehaviour
    {
        #region Fields
        private List<Old_BaseUpgrade> upgrades;
        private Old_IUpgradable upgradableEntity;
        #endregion

        #region MonoBehaviour Methods
        private void Awake()
        {
            upgrades = new List<Old_BaseUpgrade>();
            upgradableEntity = GetComponent<Old_IUpgradable>();
        }

        private void Update()
        {
            UpdateAll();
        }
        #endregion

        #region Custom Methods
        public void EnableAll() => upgrades.ForEach(u => upgradableEntity.EnableUpgrade(u));

        public void UpdateAll() => upgrades.ForEach(u => upgradableEntity.UpdateUpgrade(u));

        public void DisableAll() => upgrades.ForEach(u => upgradableEntity.DisableUpgrade(u));

        public void AddUpgrade(Old_BaseUpgrade upgrade)
        {
            upgrades.Add(upgrade);
            upgradableEntity.EnableUpgrade(upgrade);
        }
        #endregion
    }
}
