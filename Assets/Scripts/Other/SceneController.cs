using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TECHC.Kamiyashiki
{
    //TODO: シーンを自動取得してインスペクタで選択できるようにしたい
    public enum SceneName
    {
        Title,
        InGame,
        Results,
    }
 
    public static class SceneController
    {
        public static void GetSceneName()
        {
            Debug.Log("シーン名再取得");
        }

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
}