using System.Collections;
using System.Collections.Generic;
using TECHC.Kamiyashiki;
using UnityEngine;

public class InteractJapaneseArcherDining : MonoBehaviour, IReceiveSearch
{
    [SerializeField] private DialogueText SelfIntoroText;
    [SerializeField] private DialogueText SelfIntoroAfterText;
    [SerializeField] private DialogueText RequestText;
    [SerializeField] private DialogueText RequestAfterText;

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������
        //�T���J�n��ɓS�i�q�𒲂׋|����ꎮ����肵��
        if (!gameManager.eventFlagData.WarehouseRequest && gameManager.eventFlagData.IronBars && gameManager.eventFlagData.itemDataBase.truthBullets[2].getFlag)
        {
            gameManager.OpenTextWindow(RequestText);
        }
        //�q�ɂ̎d�|�������v����
        else if (gameManager.eventFlagData.WarehouseRequest)
        {
            gameManager.OpenTextWindow(RequestAfterText);
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

        //�����ɓ��������̕\������
        if (gameManager.eventFlagData.WarehouseRequest)
        {
            //�q�ɂ̎d�|�������v����͕������������
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

    }

    void Update()
    {

    }
}
