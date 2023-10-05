using UnityEngine;

namespace App.Utilities
{
    public class OnSceneSwitchedCallBack : MonoBehaviour
    {
        [SerializeField] private float minimalLoadingTime;
        private float currTime;

        private void Awake()
        {
            currTime = 0f;
            Time.timeScale = 1f;
        }

        private void FixedUpdate()
        {
            currTime += Time.fixedDeltaTime;

            if(currTime > minimalLoadingTime)
            {
                currTime = 0f;
                SceneSwitcher.SwitchToNextLoadedScene?.Invoke();
            }
        }
    }
}
