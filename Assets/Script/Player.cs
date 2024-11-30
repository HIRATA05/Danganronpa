using UnityEngine;

namespace TECHC.Kamiyashiki
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private RoomController roomController;

        void Update()
        {
            // �}�b�v���J��
            if (Input.GetKeyDown(KeyCode.M))
            {
                roomController.OpenMapCanvas(!roomController.isOpenMap);
            }
        }
    }
}