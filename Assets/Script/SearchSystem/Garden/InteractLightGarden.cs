using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLightGarden : MonoBehaviour, IReceiveSearch
{
    //���C�g

    [SerializeField] private DialogueText GetText; //�d�r����
    [SerializeField] private DialogueText AfterText;//�����

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������
        //��܂���肵�Ă��Ȃ�������
        if (!gameManager.eventFlagData.itemDataBase.truthBullets[3].getFlag)
        {
            gameManager.OpenTextWindow(GetText);
        }
        //�T����
        else
        {
            gameManager.OpenTextWindow(AfterText);
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
