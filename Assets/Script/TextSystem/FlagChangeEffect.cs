using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagChangeEffect : MonoBehaviour
{
    //��b���ɃC�x���g�t���O��ω�������
    //UnityEvent�͈�����1�����ݒ�ł��Ȃ����ߒ���

    //�Q�[���}�l�[�W���[�̎擾
    private GameManager gameManager;


    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
    }

    void Update()
    {
        
    }
    
    //�c�_�Ɉړ�
    public void DiscussionModeSwitch()
    {
        gameManager.isDiscussionStart = true;
    }

    //�X�N���v�^�u���̃t���O��ω������ăC�x���g�𐧌䂷��
    public void GameStart_Change()
    {
        Debug.Log("GameStart��TRUE");
        gameManager.eventFlagData.GameStart = true;
    }

    public void RoomIn_Change_True()
    {
        Debug.Log("RoomIn��TRUE");
        gameManager.eventFlagData.RoomIn_True();
    }
    public void RoomIn_Change_False()
    {
        Debug.Log("RoomIn��FALSE");
        gameManager.eventFlagData.RoomIn_False();
    }
}
