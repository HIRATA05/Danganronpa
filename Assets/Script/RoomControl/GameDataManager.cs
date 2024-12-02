using UnityEngine;

namespace TECHC.Kamiyashiki
{
    public class GameDataManager : MonoBehaviour
    {
        public static GameDataManager Instance { get; private set; }

        [Header("�Q�[���f�[�^")]
        private RoomName currentRoom;

        [Header("�f�o�b�O")]
        public bool isDebug;

        public RoomName CurrentRoom
        {
            get { return currentRoom; }
            set { currentRoom = value; }
        }

        //�V���O���g��
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            Init();
        }

        private void Init()
        {
            RoomName firstRoom = RoomName.ClassRoom;
            currentRoom = firstRoom;
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneController.ReloadScene();
            }
#endif
        }
    }
}