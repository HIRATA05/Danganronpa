using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class InteractPhantomThiefGarden : MonoBehaviour, IReceiveSearch
{
    //����ŉ����Ƃ̉�b

    [SerializeField] private DialogueText RequestText; //2�K�ւ̈ړ�����
    [SerializeField] private DialogueText RequestClaerBeforText; //2�K�ւ̈ړ����Č�A�C�e������
    [SerializeField] private DialogueText RequestClaerAfterText; //2�K�ւ̈ړ����Č�A�C�e���L��
    [SerializeField] private DialogueText NormalText;//2�K�����

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������
        //2�K�����
        if (gameManager.eventFlagData.RopeWindow)
        {
            gameManager.OpenTextWindow(NormalText);
        }
        //2�K�ւ̈ړ����Č�A�C�e���L��@���[�v�A��܁A�|����Z�b�g�����125
        else if(gameManager.eventFlagData.itemDataBase.truthBullets[1].getFlag && gameManager.eventFlagData.itemDataBase.truthBullets[2].getFlag &&
            gameManager.eventFlagData.itemDataBase.truthBullets[5].getFlag && gameManager.eventFlagData.F2Open)
        {
            gameManager.OpenTextWindow(RequestClaerAfterText);
        }
        //2�K�ւ̈ړ����Č�A�C�e������
        else if (gameManager.eventFlagData.F2Request)
        {
            gameManager.OpenTextWindow(RequestClaerAfterText);
        }
        //2�K�ւ̈ړ�����
        else
        {
            gameManager.OpenTextWindow(RequestText);
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
