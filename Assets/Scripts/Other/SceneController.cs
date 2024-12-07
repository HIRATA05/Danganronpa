using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName
{
    Title,
    InGame,
    Results,
}

public static class SceneController
{
    public static IEnumerator WaitAndLoadScene(SceneName sceneName, float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        SceneManager.LoadScene(sceneName.ToString());
    }
    public static void LoadScene(SceneName sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }
}