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
            if (eventFlagData.AdventureStart && eventFlagData.F2OpenPhantomThief && !eventFlagData.F2Intrusion)//�T���J�n����2�K�N����̊ԕ\��
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

        else if (objType == ObjType.Ending)
        {
            if (eventFlagData.itemDataBase.truthBullets[8].getFlag)//�E�o�C�x���g���\��
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
        }
        else if (objType == ObjType.MonokumaGym)
        {
            eventFlagData.AppMonokuma = true;
        }
        else if (objType == ObjType.Digitalclock)
        {
            eventFlagData.Digitalclock = true;
        }
        else if (objType == ObjType.WarehouseArcher)
        {
            eventFlagData.WarehouseArcher = true;
        }
        else if (objType == ObjType.RopeWindow)
        {
            eventFlagData.RopeWindow = true;
        }
        else if (objType == ObjType.GardenArcher)
        {
            eventFlagData.F2OpenArcher = true;
        }

        Befor.SetActive(false);
        After.SetActive(true);
    }

}
