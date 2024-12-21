using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        [System.Serializable]
        public class Room
        {
            public string roomNameString; // 部屋のシーン名(参照にはEnumを推奨)
            public RoomName roomName; // 部屋のEnum
            public GameObject roomButton; // マップに配置するボタン
                                      //private bool isRoomActive;
        }

        [Header("各部屋設定")]
        [SerializeField]
        private Room[] roomList;
        private Dictionary<RoomName, Button> roomButtonDictionary = new Dictionary<RoomName, Button>();

        [Header("イベントシステム")]
        [SerializeField] private EventSystem eventSystem;

        private void Awake()
        {
            // 初期設定
            foreach (var room in roomList)
            {
                // ボタンオブジェクトの名前をRoomNameと一致させる
                Debug.Log(room.roomNameString);
                Debug.Log(room.roomName);
                Debug.Log(room.roomButton);

                room.roomButton.name = room.roomName.ToString();
                room.roomButton.GetComponentInChildren<Text>().text = room.roomName.ToString();

                var button = room.roomButton.GetComponent<Button>();
                Debug.Log(button);

                button.interactable = false;
                    
                // マップのボタンにイベントを追加
                button.onClick.AddListener(OnClickMoveRoom);

                roomButtonDictionary.Add(room.roomName, button);
            }
            roomButtonDictionary[GameDataManager.Instance.CurrentRoom].interactable = false;
        }

        public void CheckButtonAtive()
        {

        }

        /// <summary>
        /// 部屋移動する
        /// </summary>
        public void MoveRoom(RoomName _nextRoom)
        {
            GameDataManager.Instance.CurrentRoom = _nextRoom;

            SceneController.LoadScene(_nextRoom);
        }

        /// <summary>
        /// n秒間待ってから部屋移動する
        /// </summary>
        public IEnumerator WaitAndMoveRoom(RoomName _roomName, float _waitTime)
        {
            yield return new WaitForSecondsRealtime(_waitTime);
            MoveRoom(_roomName);
        }

        public void ButtonActiveSetting()
        {

        }

        /// <summary>
        /// 部屋移動を開始する(ボタン用関数)
        /// </summary>
        public void OnClickMoveRoom()
        {
            string clickButtonName = eventSystem.currentSelectedGameObject.name;

            //// 現在の部屋を押したらリターンする
            //if (clickButtonName == GameDataManager.Instance.CurrentRoom.ToString())
            //{
            //    return;
            //}

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