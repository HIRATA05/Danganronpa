using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRope : MonoBehaviour, IReceiveSearch
{
    [SerializeField] private DialogueText RopeGetText; //���[�v����
    [SerializeField] private DialogueText NormalText;//�ʏ�

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������
        //�����`����肵�Ă��Ȃ�������
        if (!gameManager.eventFlagData.itemDataBase.truthBullets[1].getFlag)
        {
            gameManager.OpenTextWindow(RopeGetText);
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
