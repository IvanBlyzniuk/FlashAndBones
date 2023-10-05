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
        private Animator planksWithButtonsAnimator;
        private Animator fadeAnimator;
        private bool isPaused;
        private float prepauseTimeScale;

        public bool IsPaused => isPaused;

        private void Awake()
        {
            planksWithButtonsAnimator = planksWithButtons.GetComponent<Animator>();
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
            if (isPaused)
                throw new System.InvalidOperationException("Cannot pause an already paused game.");
            fade.SetActive(true);
            planksWithButtonsAnimator.Play("AppearPlanksWithButtons");
            fadeAnimator.Play("Fade");
            StopGame();
            pauseEvent.CallPauseEvent(true);
        }

        public void Unpause()
        {
            if (!isPaused)
                throw new System.InvalidOperationException("Cannot unpause a not paused game.");
            planksWithButtonsAnimator.Play("DisappearPlanksWithButtons");
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