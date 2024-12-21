using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class CameraMove : MonoBehaviour
{

    //�Q�[���}�l�[�W���[
    [SerializeField] private GameManager gameManager;

    //�J����
    [SerializeField] private GameObject moveCamera;

    //��]���鎞�̉�]��
    [SerializeField] private GameObject cameraShaft;

    //�J�����ړ��͈�
    [SerializeField] private float cameraRotLimit_Right = 30.0f;
    [SerializeField] private float cameraRotLimit_Left = -30.0f;

    //��]������X�s�[�h
    public float rotateSpeed = 1.0f;


    void Start()
    {
        
    }

    void Update()
    {
        if (gameManager.playerController == PlayerController.ReticleMode)
        {
            CameraRot();
        }
    }

    private void CameraRot()
    {
        //��]�����鑬�x
        float rot = Input.GetAxis("Horizontal") * rotateSpeed;

        //��]������
        var eularAngles = cameraShaft.transform.rotation.eulerAngles;
        var angle = eularAngles.y;
        if (angle > 180) angle -= 360;

        //��]�ɐ�����������
        angle = Math.Clamp(angle + rot, cameraRotLimit_Left, cameraRotLimit_Right);

        //���̊p�x��ς���
        eularAngles.y = angle;
        cameraShaft.transform.rotation = Quaternion.Euler(eularAngles);
    }
}
