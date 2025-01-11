using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Hirata : MonoBehaviour
{
    //�e�X�g�p�̊ȈՓI�ȃ^�C�g���X�N���v�g

    //�V�[����
    [SerializeField] private string NextScene;
    //�t���O
    [SerializeField, Header("�������p�t���O�f�[�^")] private EventFlagData InitFlag;
    [SerializeField, Header("�㏑���t���O�f�[�^")] private EventFlagData SaveFlag;

    //�V�[�����ړ�����
    public void SceneMove()
    {
        //�t���O��������
        InitFlag.itemDataBase = new ItemDataBase();
        SaveFlag = InitFlag;

        SceneManager.LoadScene(NextScene);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
