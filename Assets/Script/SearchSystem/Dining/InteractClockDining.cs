using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractClockDining : MonoBehaviour, IReceiveSearch
{
    [SerializeField] private DialogueText DigitalClockNoBatteryText; //�f�W�^�����v�d�r����O
    [SerializeField] private DialogueText DigitalClockBatteryInText;//�f�W�^�����v�d�r�����
    [SerializeField] private DialogueText DigitalClockInAfter;//�f�W�^�����v�d�r���U��

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������
        //�d�r����ꂽ��
        if (gameManager.eventFlagData.Digitalclock)
        {
            gameManager.OpenTextWindow(DigitalClockInAfter);
        }
        //�d�r�������Ă��鎞
        else if(gameManager.eventFlagData.itemDataBase.truthBullets[3].getFlag)
        {
            gameManager.OpenTextWindow(DigitalClockBatteryInText);
        }
        //�d�r�������Ă��Ȃ���
        else
        {
            gameManager.OpenTextWindow(DigitalClockNoBatteryText);
        }
    }

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
    }

    void Update()
    {

    }
}
