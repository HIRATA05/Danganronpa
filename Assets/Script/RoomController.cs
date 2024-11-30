using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TECHC.Kamiyashiki
{
    [System.Serializable]
    // 部屋管理Enum(部屋を追加した際は随時ココに追記)
    public enum RoomName
    {
        ClassRoom,
        ControlRoom,
        Garden,
    }

    [System.Serializable]
    public class Room
    {
        public RoomName roomName; // 部屋の名前
        public GameObject mapImage; // マップでの表示画像
        public GameObject roomObject; // 部屋のオブジェクト
        public Camera playerCamera; // 部屋移動後映すカメラ
    }

    public class RoomController : MonoBehaviour
    {
        [Header("各部屋設定")]
        public List<Room> roomList = new List<Room>();
        private Dictionary<RoomName, Camera> cameraPositions = new Dictionary<RoomName, Camera>();

        [Header("イベントシステム")]
        [SerializeField] private EventSystem eventSystem;

        [Header("キャンバス")]
        [SerializeField] private Canvas inGameCanvas;
        [SerializeField] private Canvas mapCanvas;

        [Header("最初の部屋を設定")]
        [SerializeField] private RoomName firstRoom = RoomName.ClassRoom;

        [Header("カメラのカリングマスク")]
        [SerializeField] private LayerMask defaultLayer;
        [SerializeField] private LayerMask MapLayer;

        private RoomName currentRoom;
        private Camera currentCamera;

        [HideInInspector] public bool isOpenMap = false;

        private void Awake()
        {
            // 初期設定
            foreach (var room in roomList)
            {
                room.roomObject.name = room.roomName.ToString(); // 部屋のオブジェクトの名前をEnumからつける
                room.mapImage.name = room.roomName.ToString();　// マップ表示画像の名前をEnumからつける

                room.playerCamera.enabled = false;
                room.playerCamera.GetComponent<AudioListener>().enabled = false;

                room.mapImage.GetComponent<Button>().onClick.AddListener(OnClickMoveRoom);

                if (!cameraPositions.ContainsKey(room.roomName))
                {
                    cameraPositions.Add(room.roomName, room.playerCamera);
                }
            }

            // 現在の部屋とカメラを設定
            currentRoom = firstRoom;
            if(cameraPositions.TryGetValue(currentRoom, out Camera _currentCamera))
            {
                currentCamera = _currentCamera;
            }

            // 現在のカメラとAudioListenerのみオン
            currentCamera.enabled = true;
            currentCamera.GetComponent<AudioListener>().enabled = true;
            currentCamera.cullingMask = defaultLayer;

            // canvasのレンダーカメラを現在のカメラに変更
            ChangeRenderCamera();

            OpenMapCanvas(false); // マップを非表示に
        }

        /// <summary>
        /// 部屋移動する
        /// </summary>
        public void MoveRoom(RoomName _nextRoom)
        {
            // カメラを部屋名から取得
            cameraPositions.TryGetValue(currentRoom, out Camera currentCamera);
            cameraPositions.TryGetValue(_nextRoom, out Camera nextCamera);

            currentRoom = _nextRoom;
            switchCamera(currentCamera, nextCamera); // カメラを切り替える
        }

        /// <summary>
        /// 現在のカメラと次のカメラをを切り替える
        /// </summary>
        private void switchCamera(Camera _currentCamera, Camera _nextCamera)
        {
            // 現在のカメラとそのAudioListenerを無効化
            _currentCamera.enabled = false;
            _currentCamera.GetComponent<AudioListener>().enabled = false;

            // 次のカメラとそのAudioListenerを有効化
            _nextCamera.enabled = true;
            _nextCamera.GetComponent<AudioListener>().enabled = true;

            currentCamera = _nextCamera;

            OpenMapCanvas(false);
            ChangeRenderCamera();
        }

        /// <summary>
        /// マップCanvasとインゲームCanvasのオンオフを切り替える
        /// </summary>
        public void OpenMapCanvas(bool _isOpenMap)
        {
            inGameCanvas.enabled = !_isOpenMap;
            mapCanvas.enabled = _isOpenMap;

            // カリングマスクを切り替える
            currentCamera.cullingMask = mapCanvas.enabled ? MapLayer : defaultLayer;

            isOpenMap = _isOpenMap;
        }

        private void ChangeRenderCamera()
        {
            inGameCanvas.worldCamera = currentCamera;
            mapCanvas.worldCamera = currentCamera;
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