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
    /*
    [SerializeField] private float cameraRotLimit_Top = 0.0f;
    [SerializeField] private float cameraRotLimit_Bottom = 0.0f;
    */
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
        float rotH = Input.GetAxis("Horizontal") * rotateSpeed;
        //float rotV = Input.GetAxis("Vertical") * rotateSpeed;

        //��]������
        var eularAngles = cameraShaft.transform.rotation.eulerAngles;
        var angle = eularAngles.y;
        if (angle > 180) angle -= 360;
        /*
        var eularAnglesV = cameraShaft.transform.rotation.eulerAngles;
        var angleV = eularAnglesV.x;
        if (angleV > 180) angleV -= 360;
        */
        //��]�ɐ�����������
        angle = Math.Clamp(angle + rotH, cameraRotLimit_Left, cameraRotLimit_Right);

        //angleV = Math.Clamp(angleV + rotV, cameraRotLimit_Bottom, cameraRotLimit_Top);
        
        //���̊p�x��ς���
        eularAngles.y = angle;

        //eularAnglesV.x = angleV;

        cameraShaft.transform.rotation = Quaternion.Euler(eularAngles);
        //moveCamera.transform.rotation = Quaternion.Euler(eularAnglesV);
    }
}
