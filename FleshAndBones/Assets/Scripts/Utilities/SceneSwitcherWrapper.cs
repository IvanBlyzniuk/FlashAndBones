using UnityEngine;

namespace App.Utilities
{
    public class SceneSwitcherWrapper : MonoBehaviour
    {
        public void SwitchToSceneLoadingScreen(string sceneName)
            => SceneSwitcher.SwitchToSceneLoadingScreen(sceneName);

        public static void SwitchToScene(string sceneName)
            => SceneSwitcher.SwitchToScene(sceneName);

        //public static bool DoesSceneExist(string sceneName)
        //    => SceneSwitcher.DoesSceneExist(sceneName);

        public static bool IsRunning(string sceneName)
           => SceneSwitcher.IsRunning(sceneName);
    }
}
