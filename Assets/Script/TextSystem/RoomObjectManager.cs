using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomObjectManager : MonoBehaviour
{
    [Header("�����̒��̒��ׂ���L�����╨��ݒ�")]
    //���ׂ���Ώە�
    [SerializeField] private GameObject[] roomObject;

    private const int priorityOn = 1;
    private const int priorityOff = 0;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //�I�u�W�F�N�g���r���ăv���C�I���e�B��ω�������
    public void RoomObjectPriorityChange(GameObject CameraObject, int CameraNum)
    {
        for (int i = 0; i < roomObject.Length; i++)
        {
            //null�ł���ꍇ�̓v���C�I���e�B���[��
            if (CameraObject == null || !CameraObject.activeSelf)
            {
                CameraNumberCheck(roomObject[i], CameraNum, priorityOff);

            }
            //null�łȂ��ꍇ
            else
            {
                //�I�u�W�F�N�g����v�����ꍇ�v���C�I���e�B��1
                if (roomObject[i] == CameraObject)
                {
                    CameraNumberCheck(roomObject[i], CameraNum, priorityOn);
                }
                //�I�u�W�F�N�g����v���Ȃ������ꍇ�v���C�I���e�B���[��
                else
                {
                    CameraNumberCheck(roomObject[i], CameraNum, priorityOff);
                }
            }   
        }
    }

    //CameraNum�Ԗڂ̎q�I�u�W�F�N�g���烔�@�[�`�����J�������擾
    private void CameraNumberCheck(GameObject CameraObject, int CameraNum, int prioritySet)
    {
        PriorityChange(CameraObject.transform.GetChild(CameraNum).gameObject.GetComponent<CinemachineVirtualCamera>(), prioritySet);   
    }
    //�v���C�I���e�B��ω�������
    private void PriorityChange(CinemachineVirtualCamera Camera, int prioritySet)
    {
        Camera.Priority = prioritySet;
    }
}
