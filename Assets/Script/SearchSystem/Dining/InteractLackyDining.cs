using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLackyDining : MonoBehaviour, IReceiveSearch
{
    [SerializeField] private DialogueText DiningText;
    [SerializeField] private DialogueText BatteryText;

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        Debug.Log("�K�^��b");
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������
        if (gameManager.eventFlagData.itemDataBase.truthBullets[3].getFlag)
        {
            gameManager.OpenTextWindow(BatteryText);
        }
        else
        {
            gameManager.OpenTextWindow(DiningText);
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
