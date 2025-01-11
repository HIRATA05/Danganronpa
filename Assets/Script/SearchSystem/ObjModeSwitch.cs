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
        Digitalclock,//���v
        RopeWindow,//����̑�
        GardenStatue,//����̐Α�
    }
    public ObjType objType;

    void Start()
    {
        //�Q�[���}�l�[�W���[�擾
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        eventFlagData = gameManager.eventFlagData;
        //�t���O�ɂ���ĕ\������I�u�W�F�N�g��ς���
        if (objType == ObjType.Digitalclock)
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
        
    }

    void Update()
    {
        
    }

    //�\������I�u�W�F�N�g��ω�
    public void DisplayObjSwitch()
    {
        //�ݒ肳����ނɂ���ĕύX����t���O��ω�
        if(objType == ObjType.Digitalclock)
        {
            eventFlagData.Digitalclock = true;
        }
        else if (objType == ObjType.RopeWindow)
        {
            eventFlagData.RopeWindow = true;
        }
        Befor.SetActive(false);
        After.SetActive(true);
    }

}
