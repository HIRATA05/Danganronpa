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
            public string roomNameString; // �����̃V�[����(�Q�Ƃɂ�Enum�𐄏�)
            public RoomName roomName; // ������Enum
            public GameObject roomButton; // �}�b�v�ɔz�u����{�^��
                                      //private bool isRoomActive;
        }

        [Header("�e�����ݒ�")]
        [SerializeField]
        private Room[] roomList;
        private Dictionary<RoomName, Button> roomButtonDictionary = new Dictionary<RoomName, Button>();

        [Header("�C�x���g�V�X�e��")]
        [SerializeField] private EventSystem eventSystem;

        private void Awake()
        {
            // �����ݒ�
            foreach (var room in roomList)
            {
                // �{�^���I�u�W�F�N�g�̖��O��RoomName�ƈ�v������
                Debug.Log(room.roomNameString);
                Debug.Log(room.roomName);
                Debug.Log(room.roomButton);

                room.roomButton.name = room.roomName.ToString();
                room.roomButton.GetComponentInChildren<Text>().text = room.roomName.ToString();

                var button = room.roomButton.GetComponent<Button>();
                Debug.Log(button);

                button.interactable = false;
                    
                // �}�b�v�̃{�^���ɃC�x���g��ǉ�
                button.onClick.AddListener(OnClickMoveRoom);

                roomButtonDictionary.Add(room.roomName, button);
            }
            roomButtonDictionary[GameDataManager.Instance.CurrentRoom].interactable = false;
        }

        public void CheckButtonAtive()
        {

        }

        /// <summary>
        /// �����ړ�����
        /// </summary>
        public void MoveRoom(RoomName _nextRoom)
        {
            GameDataManager.Instance.CurrentRoom = _nextRoom;

            SceneController.LoadScene(_nextRoom);
        }

        /// <summary>
        /// n�b�ԑ҂��Ă��畔���ړ�����
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
        /// �����ړ����J�n����(�{�^���p�֐�)
        /// </summary>
        public void OnClickMoveRoom()
        {
            string clickButtonName = eventSystem.currentSelectedGameObject.name;

            //// ���݂̕������������烊�^�[������
            //if (clickButtonName == GameDataManager.Instance.CurrentRoom.ToString())
            //{
            //    return;
            //}

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