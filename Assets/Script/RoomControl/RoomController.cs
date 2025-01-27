using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static TECHC.Kamiyashiki.RoomController;

namespace TECHC.Kamiyashiki
{
    public enum RoomName
    {
        ClassRoom,
        ControlRoom,
        Garden,
    }

    public class RoomController : MonoBehaviour
    {
        private static RoomController instance = null;

        [System.Serializable]
        public class Room
        {
            public string roomNameString; // 部屋のシーン名(参照にはEnumを推奨)
            public RoomName roomName; // 部屋のEnum
            public GameObject roomButton; // マップに配置するボタン
            public bool isLocked; // 部屋が解放されているか
        }

        [Header("各部屋設定")]
        [SerializeField]
        private Room[] roomList;
        private RoomName currentRoom;
        public GameObject MapPanel;

        [Header("イベントシステム")]
        [SerializeField] private EventSystem eventSystem;

        public static RoomController Instance
        {
            get
            {
                if (instance == null)
                {
                    // シーンに存在しない場合、自動生成
                    GameObject obj = new GameObject("GameDataManager");
                    instance = obj.AddComponent<RoomController>();
                }
                return instance;
            }
        }

        private void Awake()
        {
            // シングルトン
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
            // 初期設定
            foreach (var room in roomList)
            {
                // ボタンオブジェクトの名前をRoomNameと一致させる
                room.roomButton.name = room.roomName.ToString();
                room.roomButton.GetComponentInChildren<Text>().text = room.roomName.ToString();

                room.isLocked = true;
            }
            LockRoom();
            // 現在の部屋を開放する
            currentRoom = RoomName.ClassRoom;
            OpenRoom(currentRoom);
        }

        private void Update()
        {
            // マップを開く
            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneController.LoadScene(SceneName.Map);
                MapPanel.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Q)) { OpenRoom(RoomName.ClassRoom); }
            if (Input.GetKeyDown(KeyCode.W)) { OpenRoom(RoomName.ControlRoom); }
            if (Input.GetKeyDown(KeyCode.E)) { OpenRoom(RoomName.Garden); }
        }

        private void LockRoom()
        {
            List<Room> lockedRoom = new List<Room>();
            foreach (Room room in roomList)
            {
                if (room.isLocked)
                    lockedRoom.Add(room);
            }
            if (lockedRoom.Count == 0)
                return;

            foreach (Room room in lockedRoom)
            {
                var button = room.roomButton.GetComponent<Button>();
                button.interactable = false;
            }
        }

        private void UnlockRoom()
        {
            List<Room> unlockedRoom = new List<Room>();
            foreach (Room room in roomList)
            {
                if (!room.isLocked)
                    unlockedRoom.Add(room);
            }
            if (unlockedRoom.Count == 0)
                return;

            foreach (Room room in unlockedRoom)
            {
                var button = room.roomButton.GetComponent<Button>();
                button.interactable = true;

                // マップのボタンにイベントを追加
                button.onClick.AddListener(OnClickMoveRoom);
            }
        }

        public void OpenRoom(RoomName roomName)
        {
            foreach(Room room in roomList)
            {
                if(room.roomName == roomName)
                {
                    room.isLocked = false;
                }
            }
            UnlockRoom();
        }

        /// <summary>
        /// 部屋移動する
        /// </summary>
        public void MoveRoom(RoomName _nextRoom)
        {
            currentRoom = _nextRoom;

            SceneController.LoadScene(_nextRoom);
            MapPanel.SetActive(false);
        }

        /// <summary>
        /// n秒間待ってから部屋移動する
        /// </summary>
        public IEnumerator WaitAndMoveRoom(RoomName _roomName, float _waitTime)
        {
            yield return new WaitForSecondsRealtime(_waitTime);
            MoveRoom(_roomName);
        }

        /// <summary>
        /// 部屋移動を開始する(ボタン用関数)
        /// </summary>
        public void OnClickMoveRoom()
        {
            string clickButtonName = eventSystem.currentSelectedGameObject.name;

            // 現在の部屋を押したらリターンする
            if (clickButtonName == currentRoom.ToString())
            {
                return;
            }

            // 部屋名とクリックした画像の名前が一緒なら部屋移動
            foreach (RoomName room in Enum.GetValues(typeof(RoomName)))
            {
                if (clickButtonName == room.ToString())
                {
                    MoveRoom(room);
                }
            }
        }
    }
}