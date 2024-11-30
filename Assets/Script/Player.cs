using UnityEngine;

namespace TECHC.Kamiyashiki
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private RoomController roomController;

        void Update()
        {
            // マップを開閉
            if (Input.GetKeyDown(KeyCode.M))
            {
                roomController.OpenMapCanvas(!roomController.isOpenMap);
            }
        }
    }
}