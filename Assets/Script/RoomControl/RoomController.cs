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
    }

    public class RoomController : MonoBehaviour
    {

        [Header("�t���O�f�[�^")]
        [SerializeField] private EventFlagData eventFlagData;

        private static RoomController instance = null;

        [System.Serializable]
        public class Room
        {
            public string roomNameString; // �����̃V�[����(�Q�Ƃɂ�Enum�𐄏�)
            public RoomName roomName; // ������Enum
            public GameObject roomButton; // �}�b�v�ɔz�u����{�^��
            public bool isLocked; // �������������Ă��邩
        }

        [Header("�e�����ݒ�")]
        [SerializeField]
        private Room[] roomList;
        private RoomName currentRoom;
        public GameObject MapPanel;

        [Header("�C�x���g�V�X�e��")]
        [SerializeField] private EventSystem eventSystem;

        public static RoomController Instance
        {
            get
            {
                if (instance == null)
                {
                    // �V�[���ɑ��݂��Ȃ��ꍇ�A��������
                    GameObject obj = new GameObject("GameDataManager");
                    instance = obj.AddComponent<RoomController>();
                }
                return instance;
            }
        }

        private void Awake()
        {
            // �V���O���g��
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
            // �����ݒ�
            foreach (var room in roomList)
            {
                // �{�^���I�u�W�F�N�g�̖��O��RoomName�ƈ�v������
                room.roomButton.name = room.roomName.ToString();
                room.roomButton.GetComponentInChildren<Text>().text = room.roomName.ToString();

                room.isLocked = true;
            }
            LockRoom();
            // ���݂̕������J������
            //currentRoom = RoomName.����1F;
            //OpenRoom(currentRoom);
            
            //�t���O�ɂ���ĕ������J��
            if (!eventFlagData.F2ClassRoom)
            {
                OpenRoom(RoomName.����1F);

                if (eventFlagData.GameStart_All_TalkStart)
                {
                    OpenRoom(RoomName.���փz�[��);
                    OpenRoom(RoomName.����);
                    OpenRoom(RoomName.�H��);
                } 
            }
            

            if (eventFlagData.SelfIntoro_Call && !eventFlagData.F2ClassRoom)
            {
                OpenRoom(RoomName.�̈��);
            }

            if (eventFlagData.AdventureStart && !eventFlagData.F2ClassRoom)
            {
                OpenRoom(RoomName.�q��);
                OpenRoom(RoomName.����);

            }

            if (eventFlagData.F2Intrusion)
            {
                OpenRoom(RoomName.����2F);
            }

            if (eventFlagData.F2Intrusion && !eventFlagData.F2ClassRoom)
            {
                OpenRoom(RoomName.�}����);
                OpenRoom(RoomName.�@�B�H�쎺);
            }

            if (eventFlagData.PressMachineLock && eventFlagData.itemDataBase.truthBullets[6].getFlag)
            {
                OpenRoom(RoomName.���k��);
            }

            if (eventFlagData.itemDataBase.truthBullets[8].getFlag)
            {
                OpenRoom(RoomName.��񏈗���);
            }
            
        }

        private void Update()
        {
            // �}�b�v���J��
            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneController.LoadScene(SceneName.Map);
                MapPanel.SetActive(true);
            }
            /*
            if (Input.GetKeyDown(KeyCode.Q)) { OpenRoom(RoomName.ClassRoom); }
            if (Input.GetKeyDown(KeyCode.W)) { OpenRoom(RoomName.ControlRoom); }
            if (Input.GetKeyDown(KeyCode.E)) { OpenRoom(RoomName.Garden); }*/

            
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

                // �}�b�v�̃{�^���ɃC�x���g��ǉ�
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
        /// �����ړ�����
        /// </summary>
        public void MoveRoom(RoomName _nextRoom)
        {
            currentRoom = _nextRoom;

            SceneController.LoadScene(_nextRoom);
            MapPanel.SetActive(false);
        }

        /// <summary>
        /// n�b�ԑ҂��Ă��畔���ړ�����
        /// </summary>
        public IEnumerator WaitAndMoveRoom(RoomName _roomName, float _waitTime)
        {
            yield return new WaitForSecondsRealtime(_waitTime);
            MoveRoom(_roomName);
        }

        /// <summary>
        /// �����ړ����J�n����(�{�^���p�֐�)
        /// </summary>
        public void OnClickMoveRoom()
        {
            string clickButtonName = eventSystem.currentSelectedGameObject.name;

            // ���݂̕������������烊�^�[������
            if (clickButtonName == currentRoom.ToString())
            {
                return;
            }

            // �������ƃN���b�N�����摜�̖��O���ꏏ�Ȃ畔���ړ�
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