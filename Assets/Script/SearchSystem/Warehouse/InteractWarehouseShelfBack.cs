using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWarehouseShelfBack : MonoBehaviour, IReceiveSearch
{
    //�q�ɉ��̒I

    [SerializeField] private DialogueText ClawGetText; //��ܓ���
    [SerializeField] private DialogueText NormalText;//�ʏ�

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������
        //��܂���肵�Ă��Ȃ�������
        if (!gameManager.eventFlagData.itemDataBase.truthBullets[5].getFlag)
        {
            gameManager.OpenTextWindow(ClawGetText);
        }
        //�T����
        else
        {
            gameManager.OpenTextWindow(NormalText);
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
