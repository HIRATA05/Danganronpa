using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private GameObject moveCamera;

    
    void Start()
    {
        
    }

    void Update()
    {
        //���͂ŃJ�����p�x�ύX
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //moveCamera.
            //pos.x += 1;
        }
        //�u���v���͂̏ꍇ�͕ϐ��upos�v��x���ɂ�������W�𖈃t���[�����ɂP��������
        if (Input.GetKey(KeyCode.LeftArrow))
        {
           // pos.x -= 1;
        }
    }
}
