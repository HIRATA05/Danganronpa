using UnityEngine;
using UnityEngine.SceneManagement;

namespace TECHC.Kamiyashiki
{
    public class Player : MonoBehaviour
    {
        void Update()
        {
            // �}�b�v���J��
            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneController.LoadScene(SceneName.Map);
            }
        }
    }
}