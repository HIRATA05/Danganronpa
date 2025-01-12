using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHackerGarden : MonoBehaviour, IReceiveSearch
{
    //�n�b�J�[���ȏЉ�

    [SerializeField] private DialogueText SelfIntoroText; //���ȏЉ�
    [SerializeField] private DialogueText NormalText;//�ʏ�

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������
        //��܂���肵�Ă��Ȃ�������
        if (!gameManager.eventFlagData.SelfIntoro_Hacker)
        {
            gameManager.OpenTextWindow(SelfIntoroText);
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
