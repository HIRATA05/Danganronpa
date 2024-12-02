using UnityEngine;
using UnityEngine.SceneManagement;

namespace TECHC.Kamiyashiki
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private RoomController roomController;

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