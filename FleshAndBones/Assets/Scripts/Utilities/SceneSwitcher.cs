using System;
using System.Linq;
using UnityEngine.SceneManagement;

namespace App.Utilities
{
    public static class SceneSwitcher
    {
        public delegate void OnSceneSwitched();
        public static Action SwitchToNextLoadedScene;
        private static readonly string LoadingSceneName = "Loading";

        public static void SwitchToSceneLoadingScreen(string sceneName)
        {
            SwitchToNextLoadedScene = () => SwitchToScene(sceneName);
            SwitchToScene(LoadingSceneName);
        }

        public static void SwitchToScene(string sceneName)
        {
            //if (!DoesSceneExist(sceneName))
            //    throw new ArgumentException($"The scene {sceneName} does not exist in the build settings" +
            //        $" or is not enabled. Add the one to build settings or enable.");

            if (IsRunning(sceneName))
                throw new InvalidOperationException($"Cannot switch to scene {sceneName} that is currently running.");

            SceneManager.LoadScene(sceneName);
        }

        //public static bool DoesSceneExist(string sceneName)
        //    => EditorBuildSettings.scenes.Any
        //    (
        //        scene => scene.enabled && scene.path.Contains("/" + sceneName + ".unity")
        //    );

        public static bool IsRunning(string sceneName)
           => SceneManager.GetActiveScene().name == sceneName;
    }
}