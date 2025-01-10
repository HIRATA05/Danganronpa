using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjModeSwitch : MonoBehaviour
{
    //2�̃��[�h��؂�ւ��ĕ\�����郂�f����ύX

    //�\������2�̃��f��
    [SerializeField] private GameObject Befor;
    [SerializeField] private GameObject After;

    private GameManager gameManager;
    private EventFlagData eventFlagData;

    void Start()
    {
        //�Q�[���}�l�[�W���[�擾
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        eventFlagData = gameManager.eventFlagData;
        //�t���O�ɂ���ĕ\������I�u�W�F�N�g��ς���
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

    void Update()
    {
        
    }

    //�\������I�u�W�F�N�g��ω�
    public void DisplayObjSwitch()
    {
        eventFlagData.Digitalclock = true;
        Befor.SetActive(false);
        After.SetActive(true);
    }

}
