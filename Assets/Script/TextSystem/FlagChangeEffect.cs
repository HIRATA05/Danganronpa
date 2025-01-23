using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagChangeEffect : MonoBehaviour
{
    //��b���ɃC�x���g�t���O��ω�������
    //UnityEvent�͈�����1�����ݒ�ł��Ȃ����ߒ���

    //�Q�[���}�l�[�W���[�̎擾
    private GameManager gameManager;
    private EventFlagData eventFlagData;

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        eventFlagData = gameManager.eventFlagData;
    }

    void Update()
    {
        
    }
    
    //���̊֐���ݒ肷��Ƌc�_�Ɉړ�����
    public void DiscussionModeSwitch()
    {
        gameManager.isDiscussionStart = true;
    }

    //�X�N���v�^�u���̃t���O��ω������ăC�x���g�𐧌䂷��
    public void GameStart_Change()
    {
        Debug.Log("GameStart��TRUE");
        eventFlagData.GameStart = true;
    }

    public void GameStart_WindowChange_True()
    {
        Debug.Log("GameStart_Window��TRUE");
        eventFlagData.GameStart_Window = true;
    }
    public void GameStart_MonitorChange_True()
    {
        Debug.Log("GameStart_Monitor��TRUE");
        eventFlagData.GameStart_Monitor = true;
    }
    public void GameStart_LackyChange_True()
    {
        Debug.Log("GameStart_Lacky��TRUE");
        eventFlagData.GameStart_Lacky = true;
    }
    public void GameStart_All_True()
    {
        if (eventFlagData.GameStart_Lacky && eventFlagData.GameStart_Monitor && eventFlagData.GameStart_Window)
        {
            eventFlagData.GameStart_All = true;
        }
    }

    public void GameStart_ClassRoom_Change_True()
    {
        Debug.Log("GameStart_ClassRoom��TRUE");
        eventFlagData.GameStart_ClassRoom_True();
    }
    public void GameStart_ClassRoom_Change_False()
    {
        Debug.Log("GameStart_ClassRoom��FALSE");
        eventFlagData.GameStart_ClassRoom_False();
    }

    public void RoomIn_Change_True()
    {
        Debug.Log("RoomIn��TRUE");
        eventFlagData.RoomIn_True();
    }
    public void RoomIn_Change_False()
    {
        Debug.Log("RoomIn��FALSE");
        eventFlagData.RoomIn_False();
    }

    //�R�g�_�}����
    public void TruesBulletGet_MonokumaInfo()
    {
        eventFlagData.itemDataBase.truthBullets[0].getFlag = true;
    }
    public void TruesBulletGet_Rope()
    {
        eventFlagData.itemDataBase.truthBullets[1].getFlag = true;
    }
    public void TruesBulletGet_ArcherySet()
    {
        eventFlagData.itemDataBase.truthBullets[2].getFlag = true;
    }
    public void TruesBulletGet_Battery()
    {
        eventFlagData.itemDataBase.truthBullets[3].getFlag = true;
    }
    public void TruesBulletGet_Claw()
    {
        eventFlagData.itemDataBase.truthBullets[5].getFlag = true;
    }
    public void TruesBulletGet_NotePc()
    {
        eventFlagData.itemDataBase.truthBullets[6].getFlag = true;
    }
    public void TruesBulletGet_Korosiai()
    {
        eventFlagData.itemDataBase.truthBullets[7].getFlag = true;
    }
    public void TruesBulletGet_Key()
    {
        eventFlagData.itemDataBase.truthBullets[8].getFlag = true;
    }

    //���ȏЉ�
    public void SelfIntoro_Archer_True()
    {
        eventFlagData.SelfIntoro_Archer = true;
    }
    public void SelfIntoro_Hacker_True()
    {
        eventFlagData.SelfIntoro_Hacker = true;
    }
    public void SelfIntoro_PhantomThief_True()
    {
        eventFlagData.SelfIntoro_PhantomThief = true;
    }
    public void SelfIntoro_All_True()
    {
        if(eventFlagData.SelfIntoro_Archer && eventFlagData.SelfIntoro_Hacker && eventFlagData.SelfIntoro_PhantomThief)
        {
            eventFlagData.SelfIntoro_All = true;
        }
    }

    //�H���ɓ��������̎���������b�t���O
    public void RoomIn_Dining_True()
    {
        eventFlagData.RoomIn_Dining = true;
    }
    //�S�i�q�̉����J�n
    public void IronBars_True()
    {
        eventFlagData.IronBars = true;
    }
    //�q�ɂ̎d�|�������̗v��
    public void WarehouseRequest_True()
    {
        eventFlagData.WarehouseRequest = true;
    }
    //�S�i�q�̉���
    public void IronBarsOpen_True()
    {
        eventFlagData.IronBarsOpen = true;
    }
    //�����2�K�J���C�x���g����
    public void F2Request_True()
    {
        eventFlagData.F2Request = true;
    }
    //2�K�J���C�x���g�B���\
    public void F2Open_True()
    {
        if(gameManager.eventFlagData.itemDataBase.truthBullets[1].getFlag && gameManager.eventFlagData.itemDataBase.truthBullets[2].getFlag &&
            gameManager.eventFlagData.itemDataBase.truthBullets[5].getFlag)
        {
            eventFlagData.F2Open = true;
        }
    }
    //2�K�����ʋC���T��
    public void ClassRoomF2_Vent_True()
    {
        eventFlagData.ClassRoomF2_Vent = true;
    }
    //2�K�����ʋC���T��
    public void ClassRoomF2_Window_True()
    {
        eventFlagData.ClassRoomF2_Window = true;
    }
    //�}�����̖{�I��T���A�@�B�H�쎺��
    public void MemoMachineWork_True()
    {
        eventFlagData.MemoMachineWork = true;
    }
    //�n�b�J�[�֋��͗v��

    //�v���X�Ń��m�N�}�j��

    
}
