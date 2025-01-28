using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TECHC.Kamiyashiki
{
    public enum RoomName
    {
        ����1F,
        ��񏈗���,
        ���փz�[��,
        �H��,
        ����,
        �̈��,
        �q��,
        ����,
        ���k��,
        ����2F,
        �}����,
        �@�B�H�쎺,
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
        [Header("�����f�[�^")]
        [SerializeField]
        private RoomData roomData;
        [Header("�t���O�f�[�^")]
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
            // �{�^���ƕ����f�[�^�������N����
            buttonDictionary = new Dictionary<RoomName, GameObject>();

            foreach (var room in rooms)
            {
                // �{�^�������ݒ�
                room.roomButton.GetComponentInChildren<Text>().text = room.roomName.ToString();
                room.roomButton.GetComponent<Button>().onClick.AddListener(() => OnClickMoveRoom(room.roomName));

                buttonDictionary[room.roomName] = room.roomButton.gameObject;
            }
            LockRoom();
            UnlockRoom(eventFlagData.currentRoom);

            //�t���O�ɂ���ĕ������J��
            if (!eventFlagData.F2ClassRoom)
            {
                UnlockRoom(RoomName.����1F);

                if (eventFlagData.GameStart_All_TalkStart)
                {
                    UnlockRoom(RoomName.���փz�[��);
                    UnlockRoom(RoomName.����);
                    UnlockRoom(RoomName.�H��);
                }
            }

            if (eventFlagData.SelfIntoro_Call && !eventFlagData.F2ClassRoom)
            {
                UnlockRoom(RoomName.�̈��);
            }

            if (eventFlagData.AdventureStart && !eventFlagData.F2ClassRoom)
            {
                UnlockRoom(RoomName.�q��);
                UnlockRoom(RoomName.����);

            }

            if (eventFlagData.F2Intrusion)
            {
                UnlockRoom(RoomName.����2F);
            }

            if (eventFlagData.F2Intrusion && !eventFlagData.F2ClassRoom)
            {
                UnlockRoom(RoomName.�}����);
                UnlockRoom(RoomName.�@�B�H�쎺);
            }

            if (eventFlagData.PressMachineLock && eventFlagData.itemDataBase.truthBullets[6].getFlag)
            {
                UnlockRoom(RoomName.���k��);
            }

            if (eventFlagData.itemDataBase.truthBullets[8].getFlag)
            {
                UnlockRoom(RoomName.��񏈗���);
            }

            UpdateRoomButtons();
        }

        void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Alpha0)) { LockRoom(); } // �S�Ă̕��������b�N����
            if (Input.GetKeyDown(KeyCode.Alpha1)) { UnlockRoom(RoomName.����1F); }
            if (Input.GetKeyDown(KeyCode.Alpha2)) { UnlockRoom(RoomName.��񏈗���); }
            if (Input.GetKeyDown(KeyCode.Alpha3)) { UnlockRoom(RoomName.���փz�[��); }
            if (Input.GetKeyDown(KeyCode.Alpha4)) { UnlockRoom(RoomName.�H��); }
            if (Input.GetKeyDown(KeyCode.Alpha5)) { UnlockRoom(RoomName.����); }
            if (Input.GetKeyDown(KeyCode.Alpha6)) { UnlockRoom(RoomName.�̈��); }
            if (Input.GetKeyDown(KeyCode.Alpha7)) { UnlockRoom(RoomName.�q��); }
            if (Input.GetKeyDown(KeyCode.Alpha8)) { UnlockRoom(RoomName.����); }
            if (Input.GetKeyDown(KeyCode.Alpha9)) { UnlockRoom(RoomName.���k��); }
            if (Input.GetKeyDown(KeyCode.LeftArrow)) { UnlockRoom(RoomName.����2F); }
            if (Input.GetKeyDown(KeyCode.UpArrow)) { UnlockRoom(RoomName.�}����); }
            if (Input.GetKeyDown(KeyCode.DownArrow)) { UnlockRoom(RoomName.�@�B�H�쎺); }
#endif
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
            if (eventFlagData.currentRoom == roomName) { return; }

            eventFlagData.currentRoom = roomName;
            SceneController.LoadScene(roomName);
        }
    }
}