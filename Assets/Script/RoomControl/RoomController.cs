using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TECHC.Kamiyashiki
{
    public enum RoomName
    {
        教室1F,
        情報処理室,
        玄関ホール,
        食堂,
        中庭,
        体育館,
        倉庫,
        道場,
        生徒個室,
        教室2F,
        図書室,
        機械工作室,
        TestRoom,
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
        private RoomData roomData;
        [Header("フラグデータ")]
        [SerializeField]
        private EventFlagData eventFlagData;

        [SerializeField]
        private List<RoomButton> rooms = new List<RoomButton>();
        private Dictionary<RoomName, GameObject> buttonDictionary;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            // ボタンと部屋データをリンクする
            buttonDictionary = new Dictionary<RoomName, GameObject>();

            foreach (var room in rooms)
            {
                // ボタン初期設定
                room.roomButton.GetComponentInChildren<Text>().text = room.roomName.ToString();
                room.roomButton.GetComponent<Button>().onClick.AddListener(() => OnClickMoveRoom(room.roomName));

                buttonDictionary[room.roomName] = room.roomButton.gameObject;
            }
            LockRoom();
            UnlockRoom(eventFlagData.currentRoom);

            //フラグによって部屋を開放
            if (!eventFlagData.F2ClassRoom)
            {
                UnlockRoom(RoomName.教室1F);

                if (eventFlagData.GameStart_All_TalkStart)
                {
                    UnlockRoom(RoomName.玄関ホール);
                    UnlockRoom(RoomName.中庭);
                    UnlockRoom(RoomName.食堂);
                }
            }

            if (eventFlagData.SelfIntoro_Call && !eventFlagData.F2ClassRoom)
            {
                UnlockRoom(RoomName.体育館);
            }

            if (eventFlagData.AdventureStart && !eventFlagData.F2ClassRoom)
            {
                UnlockRoom(RoomName.倉庫);
                UnlockRoom(RoomName.道場);

            }

            if (eventFlagData.F2Intrusion)
            {
                UnlockRoom(RoomName.教室2F);
            }

            if (eventFlagData.F2Intrusion && !eventFlagData.F2ClassRoom)
            {
                UnlockRoom(RoomName.図書室);
                UnlockRoom(RoomName.機械工作室);
            }

            if (eventFlagData.PressMachineLock && eventFlagData.itemDataBase.truthBullets[6].getFlag)
            {
                UnlockRoom(RoomName.生徒個室);
            }

            if (eventFlagData.itemDataBase.truthBullets[8].getFlag)
            {
                UnlockRoom(RoomName.情報処理室);
            }

            UpdateRoomButtons();
        }

        void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Alpha0)) { LockRoom(); } // 全ての部屋をロックする
            if (Input.GetKeyDown(KeyCode.Alpha1)) { UnlockRoom(RoomName.教室1F); }
            if (Input.GetKeyDown(KeyCode.Alpha2)) { UnlockRoom(RoomName.情報処理室); }
            if (Input.GetKeyDown(KeyCode.Alpha3)) { UnlockRoom(RoomName.玄関ホール); }
            if (Input.GetKeyDown(KeyCode.Alpha4)) { UnlockRoom(RoomName.食堂); }
            if (Input.GetKeyDown(KeyCode.Alpha5)) { UnlockRoom(RoomName.中庭); }
            if (Input.GetKeyDown(KeyCode.Alpha6)) { UnlockRoom(RoomName.体育館); }
            if (Input.GetKeyDown(KeyCode.Alpha7)) { UnlockRoom(RoomName.倉庫); }
            if (Input.GetKeyDown(KeyCode.Alpha8)) { UnlockRoom(RoomName.道場); }
            if (Input.GetKeyDown(KeyCode.Alpha9)) { UnlockRoom(RoomName.生徒個室); }
            if (Input.GetKeyDown(KeyCode.LeftArrow)) { UnlockRoom(RoomName.教室2F); }
            if (Input.GetKeyDown(KeyCode.UpArrow)) { UnlockRoom(RoomName.図書室); }
            if (Input.GetKeyDown(KeyCode.DownArrow)) { UnlockRoom(RoomName.機械工作室); }
#endif
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
            if (eventFlagData.currentRoom == roomName) { return; }

            eventFlagData.currentRoom = roomName;
            SceneController.LoadScene(roomName);
        }
    }
}