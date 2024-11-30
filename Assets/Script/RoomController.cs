using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TECHC.Kamiyashiki
{
    [System.Serializable]
    public class Room
    {
        public SceneName sceneName; // 遷移先のシーン
        public GameObject mapImage; // マップでの表示画像
    }

    public class RoomController : MonoBehaviour
    {
        // 部屋類
        public List<Room> roomList = new List<Room>();

        [SerializeField] private EventSystem eventSystem;

        [SerializeField] private Canvas ingameCanvas;
        [SerializeField] private Canvas mapCanvas;

        private void Awake()
        {
            foreach (var room in roomList)
            {
                //　初期設定
                room.mapImage.GetComponent<Button>().onClick.AddListener(BeginMoveRoom);
                room.mapImage.name = room.sceneName.ToString();
            }

            // キャンバス切り替え
            OpenMapCanvas(false);
        }

        public IEnumerator WaitAndMoveRoom(SceneName sceneName, float waitTime, Transform target)
        {
            yield return new WaitForSecondsRealtime(waitTime);
            MoveRoom(sceneName);
        }

        public void MoveRoom(SceneName sceneName)
        {
            SceneController.LoadScene(sceneName);
        }

        public void BeginMoveRoom()
        {
            foreach (SceneName scene in Enum.GetValues(typeof(SceneName)))
            {
                if (eventSystem.currentSelectedGameObject.name == scene.ToString())
                {
                    MoveRoom(scene);
                }
            }
        }

        public void OpenMapCanvas(bool isOn)
        {
            ingameCanvas.enabled = !isOn;
            mapCanvas.enabled = isOn;
        }
    }
}