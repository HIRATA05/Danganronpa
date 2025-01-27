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
        [Header("�����f�[�^")]
        [SerializeField]
        private RoomData roomData; // ScriptableObject���Q��

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
            // �������J������
            if (Input.GetKeyDown(KeyCode.Alpha0)) { LockRoom(); }
            if (Input.GetKeyDown(KeyCode.Alpha1)) { UnlockRoom(RoomName.ClassRoom); }
            if (Input.GetKeyDown(KeyCode.Alpha2)) { UnlockRoom(RoomName.ControlRoom); }
            if (Input.GetKeyDown(KeyCode.Alpha3)) { UnlockRoom(RoomName.Garden); }
#endif
        }

        /// <summary>
        /// �����ݒ�
        /// </summary>
        private void Init()
        {
            // �{�^���ƕ����f�[�^�������N����
            buttonDictionary = new Dictionary<RoomName, GameObject>();

            foreach (var room in rooms)
            {
                // �{�^�������ݒ�
                room.roomButton.GetComponentInChildren<TextMeshProUGUI>().text = room.roomName.ToString();
                room.roomButton.GetComponent<Button>().onClick.AddListener(() => OnClickMoveRoom(room.roomName));

                buttonDictionary[room.roomName] = room.roomButton.gameObject;
            }

            UnlockRoom(roomData.currentRoom);
            UpdateRoomButtons();
        }

        /// <summary>
        /// �����{�^���̏�Ԃ��X�V
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
        /// ���������b�N����
        /// </summary>
        public void LockRoom()
        {
            foreach (var room in roomData.roomStates)
            {
                room.isLocked = true;
            }

            UpdateRoomButtons(); // �{�^����Ԃ��X�V
        }

        /// <summary>
        /// �������A�����b�N����
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

            UpdateRoomButtons(); // �{�^����Ԃ��X�V
        }

        /// <summary>
        /// �����ړ����J�n����
        /// </summary>
        public void OnClickMoveRoom(RoomName roomName)
        {
            // ���݂̕����Ɠ����ꍇ�ړ����Ȃ�
            if (roomData.currentRoom == roomName) { return; }

            roomData.currentRoom = roomName;
            SceneController.LoadScene(roomName);
        }
    }
}
