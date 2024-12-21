using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TECHC.Kamiyashiki
{
    // TODO: �V�[���������擾���ăC���X�y�N�^�őI���ł���悤�ɂ�����
    public enum SceneName
    {
        Title,
        InGame,
        Results,
        Map,
    }

    public static class SceneController
    {
        public static void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public static void LoadScene(Enum sceneName)
        {
            SceneManager.LoadScene(sceneName.ToString());
        }

        public static IEnumerator WaitAndLoadScene(Enum sceneName, float waitTime)
        {
            yield return new WaitForSecondsRealtime(waitTime);
            SceneManager.LoadScene(sceneName.ToString());
        }
    }
}