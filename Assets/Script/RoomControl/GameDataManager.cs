using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace TECHC.Kamiyashiki
{
    class GameDataManager : MonoBehaviour
    {
        private static GameDataManager instance = null;

        [Header("ゲームデータ")]
        private RoomName currentRoom;

        [Header("デバッグ")]
        public bool isDebug;

        public static GameDataManager Instance
        {
            get
            {
                if (instance == null)
                {
                    // シーンに存在しない場合、自動生成
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

        // シングルトン
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
