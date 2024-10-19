using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class ReticleAim : MonoBehaviour
{
    //�G�C���̑���

    //�Ə�
    [SerializeField] private Image aimImage;

    //�Ə��̉摜
    [SerializeField] private Sprite OnCollisionAimImage;
    [SerializeField] private Sprite DisCollisionAimImage;
    //�Ə��̑傫��
    [SerializeField] private float OnCollisionAimSize;
    [SerializeField] private float DisCollisionAimSize;

    //��]���x
    private float RotSpeed = 0.1f;


    void Start()
    {
        
    }

    void Update()
    {
        //�}�E�X�̈ʒu�ƏƏ���̈ʒu�𓯊�������B
        transform.position = Input.mousePosition;

        RaycastHit hit;

        //MainCamera����}�E�X�̈ʒu��Ray���΂�
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit))
        {
            //�T���\�I�u�W�F�N�g�ɓ���������
            if (hit.transform.CompareTag("SearchObj"))
            {
                //�Ə���ω�������
                aimImage.sprite = OnCollisionAimImage;
                //�Ə��T�C�Y�ύX
                transform.localScale = new Vector3(OnCollisionAimSize, OnCollisionAimSize, OnCollisionAimSize);

            }
            else
            {
                //�I�u�W�F�N�g�ɓ������Ă��Ȃ���
                DisColliderAim();
            }
        }
        else
        {
            //�I�u�W�F�N�g�ɓ������Ă��Ȃ���
            DisColliderAim();
        }


    }

    //�T���\�I�u�W�F�N�g�ɓ������Ă��Ȃ���
    private void DisColliderAim()
    {
        //�Ə��̉�]
        //transform���擾
        Transform myTransform = this.transform;

        //���[���h���W����ɉ�]���擾
        Vector3 worldAngle = myTransform.eulerAngles;
        worldAngle.x = 0.0f;
        worldAngle.y = 0.0f;
        worldAngle.z += RotSpeed;//��]�����Z
        myTransform.eulerAngles = worldAngle;//��]�p�x��ݒ�

        //�Ə���ω�������
        aimImage.sprite = DisCollisionAimImage;
        //�Ə��T�C�Y�ύX
        transform.localScale = new Vector3(DisCollisionAimSize, DisCollisionAimSize, DisCollisionAimSize);

    }

}
