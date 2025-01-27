using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TECHC.Kamiyashiki
{
    public enum RoomName
    {
        ClassRoom,
        ControlRoom,
        Garden,
    }

    [System.Serializable]
    public class RoomButton
    {
        public Button roomButton;
        public RoomName roomName;
    }

    public class RoomController : MonoBehaviour
    {
        [Header("部屋データ")]
        [SerializeField]
        private RoomData roomData; // ScriptableObjectを参照

        [SerializeField]
        private List<RoomButton> rooms = new List<RoomButton>();
        private Dictionary<RoomName, GameObject> buttonDictionary;

        private void Awake()
        {
            Init();
        }

        void Update()
        {
#if UNITY_EDITOR
            // 部屋を開放する
            if (Input.GetKeyDown(KeyCode.Alpha0)) { LockRoom(); }
            if (Input.GetKeyDown(KeyCode.Alpha1)) { UnlockRoom(RoomName.ClassRoom); }
            if (Input.GetKeyDown(KeyCode.Alpha2)) { UnlockRoom(RoomName.ControlRoom); }
            if (Input.GetKeyDown(KeyCode.Alpha3)) { UnlockRoom(RoomName.Garden); }
#endif
        }

        /// <summary>
        /// 初期設定
        /// </summary>
        private void Init()
        {
            // ボタンと部屋データをリンクする
            buttonDictionary = new Dictionary<RoomName, GameObject>();

            foreach (var room in rooms)
            {
                // ボタン初期設定
                room.roomButton.GetComponentInChildren<TextMeshProUGUI>().text = room.roomName.ToString();
                room.roomButton.GetComponent<Button>().onClick.AddListener(() => OnClickMoveRoom(room.roomName));

                buttonDictionary[room.roomName] = room.roomButton.gameObject;
            }

            UnlockRoom(roomData.currentRoom);
            UpdateRoomButtons();
        }

        /// <summary>
        /// 部屋ボタンの状態を更新
        /// </summary>
        private void UpdateRoomButtons()
        {
            foreach (var room in roomData.roomStates)
            {
                if (buttonDictionary.TryGetValue(room.roomName, out GameObject button))
                {
                    button.GetComponent<Button>().interactable = !room.isLocked;
                }
            }
        }

        /// <summary>
        /// 部屋をロックする
        /// </summary>
        public void LockRoom()
        {
            foreach (var room in roomData.roomStates)
            {
                room.isLocked = true;
            }

            UpdateRoomButtons(); // ボタン状態を更新
        }

        /// <summary>
        /// 部屋をアンロックする
        /// </summary>
        public void UnlockRoom(RoomName roomName)
        {
            foreach (var room in roomData.roomStates)
            {
                if (room.roomName == roomName)
                {
                    room.isLocked = false;
                }
            }

            UpdateRoomButtons(); // ボタン状態を更新
        }

        /// <summary>
        /// 部屋移動を開始する
        /// </summary>
        public void OnClickMoveRoom(RoomName roomName)
        {
            // 現在の部屋と同じ場合移動しない
            if (roomData.currentRoom == roomName) { return; }

            roomData.currentRoom = roomName;
            SceneController.LoadScene(roomName);
        }
    }
}
