using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace TECHC.Kamiyashiki
{
    class GameDataManager : MonoBehaviour
    {
        private static GameDataManager instance = null;

        [Header("�Q�[���f�[�^")]
        private RoomName currentRoom;

        [Header("�f�o�b�O")]
        public bool isDebug;

        public static GameDataManager Instance
        {
            get
            {
                if (instance == null)
                {
                    // �V�[���ɑ��݂��Ȃ��ꍇ�A��������
                    GameObject obj = new GameObject("GameDataManager");
                    instance = obj.AddComponent<GameDataManager>();
                }
                return instance;
            }
        }

        public RoomName CurrentRoom
        {
            get { return currentRoom; }
            set { currentRoom = value; }
        }

        // �V���O���g��
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
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
            if (UnityEngine.Input.GetKeyDown(KeyCode.R))
            {
                SceneController.ReloadScene();
            }
#endif
        }
    }
}
