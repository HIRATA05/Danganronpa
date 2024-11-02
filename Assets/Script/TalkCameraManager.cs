using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkCameraManager : MonoBehaviour
{
    //RoomManager����I�u�W�F�N�g���擾
    //�e�L�X�g�̔z��Ɠ����������J�����̒�����ƃJ���������ݒ�����߂�

    [System.Serializable] public class CameraSet
    {
        //�J���������̎��
        public enum CameraDivision
        {
            CenterOnly,//�����̂�
            CenterAndRight,//�����ƉE
            CenterAndLeft,//�����ƍ�
        }

        //�J�����ŕ\������I�u�W�F�N�g
        public GameObject cameraLookObjectCenter;//����
        public GameObject cameraLookObjectRight;//�E
        public GameObject cameraLookObjectLeft;//��

        //�J���������̌`
        public CameraDivision camDivision;
    }

    [System.Serializable] public class TalkSet
    {
        //��b�ԍ��@Scriptable�e�L�X�g�Ɠ������邽�߂̔ԍ�
        public int number;

        //��b�̐������쐬����
        public CameraSet[] cameraSet;
    }

    [Header("���̕����̉�b�C�x���g�̐������ݒ�")]
    public TalkSet[] talkSet;



    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
