using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TECHC.Kamiyashiki
{
    //TODO: �V�[���������擾���ăC���X�y�N�^�őI���ł���悤�ɂ�����
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
            Debug.Log("�V�[�����Ď擾");
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