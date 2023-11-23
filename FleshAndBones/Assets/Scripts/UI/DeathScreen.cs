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
        [SerializeField] private GameObject deathScreenCanvas;
        [SerializeField] private TextMeshProUGUI scoreTMP;
        [SerializeField] private Fader fader;
        [SerializeField] private DieEvent dieEvent;
        [SerializeField] private DeathScreenAppearedEvent onDeathScreenAppeared;
        [SerializeField] private CountUpdatedEvent scoreUpdatedEvent;
        private int score;
        #endregion

        #region Properties
        public int Score
        {
            get => score;
            private set
            {
                if (value < 0)
                    throw new System.InvalidOperationException("Score cannot be negative.");
                score = value;
                scoreTMP.text = "Score: " + ScoreToString(value);
            }
        }
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            Score = 0;
            fader.gameObject.SetActive(false);
        }

        private void Start() => HideDeathScreen();

        private void OnEnable() => AddAllOnEvent();

        private void OnDisable() => RemoveAllOnEvent();
        #endregion

        #region Main Behavioral Methods
        public void ShowDeathScreen() => StartCoroutine(ShowDeathScreenCoroutine(5f));

        public void HideDeathScreen() => deathScreenCanvas.SetActive(false);

        public IEnumerator ShowDeathScreenCoroutine(float seconds)
        {
            RemoveAllOnEvent();
            fader.gameObject.SetActive(true);
            float halfTime = seconds * 0.5f;
            yield return fader.FadeToSecondsCoroutine(1f, halfTime);
            deathScreenCanvas.SetActive(true);
            yield return fader.FadeToSecondsCoroutine(0f, halfTime);
            fader.gameObject.SetActive(false);
            onDeathScreenAppeared.CallDeathScreenAppearedEvent();
        }

        private string ScoreToString(int v)
            => v.ToString() + (score == 1 ? " day" : " days");
        #endregion

        #region EventFunctions
        private void SetScoreEvent(CountUpdatedEvent ev, CountUpdatedEventArgs args) => Score = args.newCount;

        private void ShowDeathScreenEvent(DieEvent ev) => ShowDeathScreen();
        #endregion

        #region ListenerAdders
        private void AddAllOnEvent()
        {
            dieEvent.OnDied += ShowDeathScreenEvent;
            scoreUpdatedEvent.OnCountUpdated += SetScoreEvent;
        }

        private void RemoveAllOnEvent()
        {
            dieEvent.OnDied -= ShowDeathScreenEvent;
            scoreUpdatedEvent.OnCountUpdated -= SetScoreEvent;
        }
        #endregion
    }

}
