using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using App.World.UI.Events;

namespace App.World.UI
{
    public class VolumeBarFading : MonoBehaviour
    {
        [SerializeField] private PauseEvent pauseEvent;
        [SerializeField] private Button plusVolumeButton;
        [SerializeField] private Button minusVolumeButton;
        [SerializeField] private Fader fader;
        [SerializeField] private float unfadedStateTime;
        [SerializeField] private float fadeTime;
        private bool instantlyFade;

        private void Awake()
        {
            if (unfadedStateTime < 0f - Mathf.Epsilon)
                throw new System.InvalidOperationException("Unfaded state time cannot be less then 0.");
            if(fadeTime < 0f - Mathf.Epsilon)
                throw new System.InvalidOperationException("Fade time cannot be less then 0.");
            instantlyFade = false;
        }

        private void OnEnable()
        {
            plusVolumeButton.onClick.AddListener(ShowVolumeBar);
            minusVolumeButton.onClick.AddListener(ShowVolumeBar);
            if (pauseEvent is not null)
            {
                pauseEvent.OnPause += SetInstantFadeEvent;
                pauseEvent.OnPause += InstantlyFadeIfUnpaused;
            }
        }

        private void OnDisable()
        {
            plusVolumeButton.onClick.RemoveListener(ShowVolumeBar);
            minusVolumeButton.onClick.RemoveListener(ShowVolumeBar);
            if (pauseEvent is not null)
            {
                pauseEvent.OnPause -= SetInstantFadeEvent;
                pauseEvent.OnPause -= InstantlyFadeIfUnpaused;
            }
        }

        private void SetInstantFadeEvent(PauseEvent ev, PauseEventArgs args) => instantlyFade = true;


        private void InstantlyFadeIfUnpaused(PauseEvent ev, PauseEventArgs args)
        {
            if(!args.isPaused)
            {
                fader.SetAlpha(0f);
            }
        }

        public void ShowVolumeBar()
        {
            fader.SetAlpha(1f);

            if (!instantlyFade)
            {
                StopAllCoroutines();
                StartCoroutine(ShowVolumeBarCoroutine());
            }
        }

        public IEnumerator ShowVolumeBarCoroutine()
        {
            yield return new WaitForSeconds(unfadedStateTime);
            yield return fader.FadeToSecondsCoroutine(0f, fadeTime);
        }
    }
}
