using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractJapaneseArcherDining : MonoBehaviour, IReceiveSearch
{
    [SerializeField] private DialogueText SelfIntoroText;
    [SerializeField] private DialogueText SelfIntoroAfterText;
    [SerializeField] private DialogueText RequestText;

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������
        //�T���J�n��ɓS�i�q�𒲂׋|����ꎮ����肵��
        if (!gameManager.eventFlagData.DiscStart && gameManager.eventFlagData.IronBars && gameManager.eventFlagData.itemDataBase.truthBullets[2].getFlag)
        {
            gameManager.OpenTextWindow(RequestText);
        }
        //���ȏЉ�
        else if (!gameManager.eventFlagData.SelfIntoro_Archer)
        {
            gameManager.OpenTextWindow(SelfIntoroText);
        }
        //���ȏЉ�I����
        else
        {
            gameManager.OpenTextWindow(SelfIntoroAfterText);
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
