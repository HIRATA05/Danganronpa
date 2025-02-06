using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjModeSwitch;

public class ObjModeSwitch : MonoBehaviour
{
    //2�̃��[�h��؂�ւ��ĕ\�����郂�f����ύX

    //�\������2�̃��f��
    [SerializeField] private GameObject Befor;
    [SerializeField] private GameObject After;

    private GameManager gameManager;
    private EventFlagData eventFlagData;

    //�I�u�W�F�N�g�̎��
    public enum ObjType
    {
        GameStartGym,//�̈�ق̃L�����\��
        MonokumaGym,//�̈�ق̃��m�N�}�\��
        Digitalclock,//���v
        WarehouseArcher,//�q�ɂ̋|����
        RopeWindow,//����̑�
        GardenHacker,//����̃n�b�J�[
        GardenPhantomThief,//����̉���
        GardenArcher,//����̋|����
        ClassRoomF2PhantomThief,//2�K�����̉���
        ClassRoomF2Lacky,//2�K�����̍K�^
        MachineRoomHacker,//�@�B�H�쎺�̃n�b�J�[
        MonokuGreenMove,//���m�N�}�O���[���̈ړ�
        PressMachine,//�v���X�@
        DataProcessRoomLacky,//��񏈗����̍K�^
        DataProcessRoomChara,//��񏈗����̃L�����B
        Ending//�E�o
    }
    public ObjType objType;

    void Start()
    {
        //�Q�[���}�l�[�W���[�擾
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        eventFlagData = gameManager.eventFlagData;
        //�t���O�ɂ���ĕ\������I�u�W�F�N�g��ς���
        if (objType == ObjType.GameStartGym)
        {
            if (eventFlagData.AdventureStart)
            {
                Befor.SetActive(false);
                After.SetActive(true);
            }
            else
            {
                Befor.SetActive(true);
                After.SetActive(false);
            }
        }
        else if (objType == ObjType.MonokumaGym)
        {
            if (eventFlagData.AppMonokuma)
            {
                Befor.SetActive(false);
                After.SetActive(true);
            }
            else
            {
                Befor.SetActive(true);
                After.SetActive(false);
            }
        }
        else if (objType == ObjType.Digitalclock)
        {
            if (eventFlagData.Digitalclock)
            {
                Befor.SetActive(false);
                After.SetActive(true);
            }
            else
            {
                Befor.SetActive(true);
                After.SetActive(false);
            }
        }
        else if (objType == ObjType.WarehouseArcher)
        {
            if (eventFlagData.WarehouseArcher)
            {
                Befor.SetActive(false);
                After.SetActive(true);
            }
            else
            {
                Befor.SetActive(true);
                After.SetActive(false);
            }
        }
        else if (objType == ObjType.RopeWindow)
        {
            if (eventFlagData.RopeWindow)
            {
                Befor.SetActive(false);
                After.SetActive(true);
            }
            else
            {
                Befor.SetActive(true);
                After.SetActive(false);
            }
        }
        else if (objType == ObjType.GardenHacker)
        {
            if (eventFlagData.AdventureStart)
            {
                Befor.SetActive(false);
                After.SetActive(true);
            }
            else
            {
                Befor.SetActive(true);
                After.SetActive(false);
            }
        }
        else if (objType == ObjType.GardenPhantomThief)
        {
            if (eventFlagData.AdventureStart && !eventFlagData.F2OpenPhantomThief && !eventFlagData.F2Intrusion)//�T���J�n����2�K�N����̊ԕ\��
            {
                Befor.SetActive(false);
                After.SetActive(true);
            }
            else
            {
                Befor.SetActive(true);
                After.SetActive(false);
            }
        }
        else if (objType == ObjType.GardenArcher)
        {
            if (eventFlagData.F2OpenArcher && !eventFlagData.F2Intrusion)//2�K�N���C�x���g�̊ԕ\��
            {
                Befor.SetActive(false);
                After.SetActive(true);
            }
            else
            {
                Befor.SetActive(true);
                After.SetActive(false);
            }
        }
        else if (objType == ObjType.ClassRoomF2PhantomThief)
        {
            if (eventFlagData.ClassRoomF2StartPhantomThief)//2�K�N����ŏ������\��
            {
                Befor.SetActive(false);
                After.SetActive(true);
            }
            else
            {
                Befor.SetActive(true);
                After.SetActive(false);
            }
        }
        else if (objType == ObjType.ClassRoomF2Lacky)
        {
            if (eventFlagData.ClassRoomF2_All)//2�K�����T����\��
            {
                Befor.SetActive(false);
                After.SetActive(true);
            }
            else
            {
                Befor.SetActive(true);
                After.SetActive(false);
            }
        }
        else if (objType == ObjType.PressMachine)
        {
            if (eventFlagData.PressMachineShelfUnlock)//�v���X�@�̋N��
            {
                Befor.SetActive(false);
                After.SetActive(true);
            }
            else
            {
                Befor.SetActive(true);
                After.SetActive(false);
            }
        }
        else if (objType == ObjType.MachineRoomHacker)
        {
            if (eventFlagData.HackerPressMachineRequest)//�@�B�H�쎺�̃n�b�J�[
            {
                Befor.SetActive(false);
                After.SetActive(true);
            }
            else
            {
                Befor.SetActive(true);
                After.SetActive(false);
            }
        }
        else if (objType == ObjType.MonokuGreenMove)
        {
            if (eventFlagData.MonokumaGreenPreesMove)//���m�N�}�O���[��
            {
                Befor.SetActive(false);
                After.SetActive(true);
            }
            else
            {
                Befor.SetActive(true);
                After.SetActive(false);
            }
        }
        else if (objType == ObjType.DataProcessRoomLacky)
        {
            if (eventFlagData.Lacky_DataProcessingRoom)//��񏈗����̍K�^
            {
                Befor.SetActive(false);
                After.SetActive(true);
            }
            else
            {
                Befor.SetActive(true);
                After.SetActive(false);
            }
        }
        else if (objType == ObjType.DataProcessRoomChara)
        {
            if (eventFlagData.Chara_DataProcessingRoom)//��񏈗����̃L����
            {
                Befor.SetActive(false);
                After.SetActive(true);
            }
            else
            {
                Befor.SetActive(true);
                After.SetActive(false);
            }
        }

        else if (objType == ObjType.Ending)
        {
            if (eventFlagData.EscepeSwitch)//�E�o�C�x���g���\��
            {
                Befor.SetActive(false);
                After.SetActive(true);
            }
            else
            {
                Befor.SetActive(true);
                After.SetActive(false);
            }
        }
    }

    void Update()
    {
        
    }

    //�\������I�u�W�F�N�g��ω�
    public void DisplayObjSwitch()
    {
        //�ݒ肳����ނɂ���ĕύX����t���O��ω�
        if (objType == ObjType.GameStartGym)
        {
            eventFlagData.AdventureStart = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.MonokumaGym)
        {
            eventFlagData.AppMonokuma = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.Digitalclock)
        {
            eventFlagData.Digitalclock = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.WarehouseArcher)
        {
            eventFlagData.WarehouseArcher = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.RopeWindow)
        {
            eventFlagData.RopeWindow = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.GardenArcher)
        {
            eventFlagData.F2OpenArcher = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.ClassRoomF2PhantomThief)
        {
            eventFlagData.ClassRoomF2StartPhantomThief = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.ClassRoomF2Lacky && eventFlagData.ClassRoomF2_Vent && eventFlagData.ClassRoomF2_Window)
        {
            eventFlagData.ClassRoomF2_All = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.PressMachine)
        {
            eventFlagData.PressMachineShelfUnlock = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.MachineRoomHacker)
        {
            eventFlagData.HackerPressMachineRequest = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.MonokuGreenMove)
        {
            eventFlagData.MonokumaGreenPreesMove = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.DataProcessRoomLacky)
        {
            eventFlagData.Lacky_DataProcessingRoom = true;
            DisplayObjSwitch_After();
        }
        else if (objType == ObjType.DataProcessRoomChara)
        {
            eventFlagData.Chara_DataProcessingRoom = true;
            DisplayObjSwitch_After();
        }

    }

    public void DisplayObjSwitch_After()
    {
        Befor.SetActive(false);
        After.SetActive(true);
    }

    public void DisplayObjSwitch_Befor()
    {
        Befor.SetActive(true);
        After.SetActive(false);
    }
}
