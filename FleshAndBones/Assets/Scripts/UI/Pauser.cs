using App.World.UI.Events;
using UnityEngine;

namespace App.World.UI
{
    public class Pauser : MonoBehaviour
    {
        [SerializeField] private PauseEvent pauseEvent;
        [SerializeField] private GameObject fade;
        [SerializeField] private GameObject planksWithButtons;
        [SerializeField] private DeathScreenAppearedEvent onDeathScreenAppeared;
        private Animator pausePlank;
        private Animator fadeAnimator;
        private bool isPaused;
        private float prepauseTimeScale;

        public bool IsPaused => isPaused;

        private void Awake()
        {
            pausePlank = planksWithButtons.GetComponent<Animator>();
            fadeAnimator = fade.GetComponent<Animator>();
            isPaused = false;
            prepauseTimeScale = Time.timeScale;
        }

        private void Start()
        {
            fade.SetActive(false);
        }

        private void OnEnable()
        {
            onDeathScreenAppeared.OnDeathScreenAppeared += StopGameEvent;
        }

        private void OnDisable()
        {
            onDeathScreenAppeared.OnDeathScreenAppeared -= StopGameEvent;
        }

        public void Pause()
        {
            Debug.Log("Pause");
            if (isPaused)
                throw new System.InvalidOperationException("Cannot pause an already paused game.");
            fade.SetActive(true);
            pausePlank.Play("Appear");
            fadeAnimator.Play("Fade");
            StopGame();
            pauseEvent.CallPauseEvent(true);
        }

        public void Unpause()
        {
            Debug.Log("Unpause");
            if (!isPaused)
                throw new System.InvalidOperationException("Cannot unpause a not paused game.");
            pausePlank.Play("Disappear");
            fadeAnimator.Play("Unfade");
            RenewGame();
            pauseEvent.CallPauseEvent(false);
        }

        private void StopGame()
        {
            isPaused = true;
            prepauseTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }

        private void RenewGame()
        {
            isPaused = false;
            Time.timeScale = prepauseTimeScale;
        }

        private void StopGameEvent(DeathScreenAppearedEvent ev) => StopGame();
    }
}