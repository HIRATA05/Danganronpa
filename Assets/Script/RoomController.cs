using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TECHC.Kamiyashiki
{
    [System.Serializable]
    // �����Ǘ�Enum(������ǉ������ۂ͐����R�R�ɒǋL)
    public enum RoomName
    {
        ClassRoom,
        ControlRoom,
        Garden,
    }

    [System.Serializable]
    public class Room
    {
        public RoomName roomName; // �����̖��O
        public GameObject mapImage; // �}�b�v�ł̕\���摜
        public GameObject roomObject; // �����̃I�u�W�F�N�g
        public Camera playerCamera; // �����ړ���f���J����
    }

    public class RoomController : MonoBehaviour
    {
        [Header("�e�����ݒ�")]
        public List<Room> roomList = new List<Room>();
        private Dictionary<RoomName, Camera> cameraPositions = new Dictionary<RoomName, Camera>();

        [Header("�C�x���g�V�X�e��")]
        [SerializeField] private EventSystem eventSystem;

        [Header("�L�����o�X")]
        [SerializeField] private Canvas inGameCanvas;
        [SerializeField] private Canvas mapCanvas;

        [Header("�ŏ��̕�����ݒ�")]
        [SerializeField] private RoomName firstRoom = RoomName.ClassRoom;

        [Header("�J�����̃J�����O�}�X�N")]
        [SerializeField] private LayerMask defaultLayer;
        [SerializeField] private LayerMask MapLayer;

        private RoomName currentRoom;
        private Camera currentCamera;

        [HideInInspector] public bool isOpenMap = false;

        private void Awake()
        {
            // �����ݒ�
            foreach (var room in roomList)
            {
                room.roomObject.name = room.roomName.ToString(); // �����̃I�u�W�F�N�g�̖��O��Enum�������
                room.mapImage.name = room.roomName.ToString();�@// �}�b�v�\���摜�̖��O��Enum�������

                room.playerCamera.enabled = false;
                room.playerCamera.GetComponent<AudioListener>().enabled = false;

                room.mapImage.GetComponent<Button>().onClick.AddListener(OnClickMoveRoom);

                if (!cameraPositions.ContainsKey(room.roomName))
                {
                    cameraPositions.Add(room.roomName, room.playerCamera);
                }
            }

            // ���݂̕����ƃJ������ݒ�
            currentRoom = firstRoom;
            if(cameraPositions.TryGetValue(currentRoom, out Camera _currentCamera))
            {
                currentCamera = _currentCamera;
            }

            // ���݂̃J������AudioListener�̂݃I��
            currentCamera.enabled = true;
            currentCamera.GetComponent<AudioListener>().enabled = true;
            currentCamera.cullingMask = defaultLayer;

            // canvas�̃����_�[�J���������݂̃J�����ɕύX
            ChangeRenderCamera();

            OpenMapCanvas(false); // �}�b�v���\����
        }

        /// <summary>
        /// �����ړ�����
        /// </summary>
        public void MoveRoom(RoomName _nextRoom)
        {
            // �J�����𕔉�������擾
            cameraPositions.TryGetValue(currentRoom, out Camera currentCamera);
            cameraPositions.TryGetValue(_nextRoom, out Camera nextCamera);

            currentRoom = _nextRoom;
            switchCamera(currentCamera, nextCamera); // �J������؂�ւ���
        }

        /// <summary>
        /// ���݂̃J�����Ǝ��̃J��������؂�ւ���
        /// </summary>
        private void switchCamera(Camera _currentCamera, Camera _nextCamera)
        {
            // ���݂̃J�����Ƃ���AudioListener�𖳌���
            _currentCamera.enabled = false;
            _currentCamera.GetComponent<AudioListener>().enabled = false;

            // ���̃J�����Ƃ���AudioListener��L����
            _nextCamera.enabled = true;
            _nextCamera.GetComponent<AudioListener>().enabled = true;

            currentCamera = _nextCamera;

            OpenMapCanvas(false);
            ChangeRenderCamera();
        }

        /// <summary>
        /// �}�b�vCanvas�ƃC���Q�[��Canvas�̃I���I�t��؂�ւ���
        /// </summary>
        public void OpenMapCanvas(bool _isOpenMap)
        {
            inGameCanvas.enabled = !_isOpenMap;
            mapCanvas.enabled = _isOpenMap;

            // �J�����O�}�X�N��؂�ւ���
            currentCamera.cullingMask = mapCanvas.enabled ? MapLayer : defaultLayer;

            isOpenMap = _isOpenMap;
        }

        private void ChangeRenderCamera()
        {
            inGameCanvas.worldCamera = currentCamera;
            mapCanvas.worldCamera = currentCamera;
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