using UnityEngine;
using UnityEngine.SceneManagement;

namespace TECHC.Kamiyashiki
{
    public class Player : MonoBehaviour
    {
        void Update()
        {
            // マップを開く
            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneController.LoadScene(SceneName.Map);
            }
        }
    }
}