using System.Collections;
using UnityEngine;
using TMPro;
using App.World.Entity.Player.Events;
using App.World.UI.Events;
using App.World.UI;

namespace App.World.UI
{
    public class DeathScreen : MonoBehaviour
    {
        #region Fields
        [SerializeField] private Animator deathGravestoneAnimator;
        [SerializeField] private Animator faderAnimator;
        [SerializeField] private GameObject pauser;
        [SerializeField] private DieEvent dieEvent;
        [SerializeField] private DeathScreenAppearedEvent onDeathScreenAppeared;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private Timer timer;

        #endregion

        #region MonoBehaviour

        private void OnEnable() => AddAllOnEvent();

        private void OnDisable() => RemoveAllOnEvent();
        #endregion

        #region Main Behavioral Methods

        public void ShowDeathScreen()
        {
            RemoveAllOnEvent();
            pauser.GetComponent<Pauser>().enabled = false;
            pauser.SetActive(false);
            timeText.text = timer.time;
            Time.timeScale = 0f;
            deathGravestoneAnimator.Play("Appear");
            faderAnimator.Play("Fade");
            onDeathScreenAppeared.CallDeathScreenAppearedEvent();
        }

        
        #endregion

        #region EventFunctions

        private void ShowDeathScreenEvent(DieEvent ev) => ShowDeathScreen();
        #endregion

        #region ListenerAdders
        private void AddAllOnEvent()
        {
            dieEvent.OnDied += ShowDeathScreenEvent;
        }

        private void RemoveAllOnEvent()
        {
            dieEvent.OnDied -= ShowDeathScreenEvent;
        }
        #endregion
    }

}
