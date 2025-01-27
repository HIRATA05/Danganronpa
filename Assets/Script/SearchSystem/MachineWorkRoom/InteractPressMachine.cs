using System.Collections;
using System.Collections.Generic;
using TECHC.Kamiyashiki;
using UnityEngine;

public class InteractPressMachine : MonoBehaviour, IReceiveSearch
{
    //�@�B�H�쎺�̃v���X�@

    [SerializeField] private DialogueText NormalText;
    [SerializeField] private DialogueText HackerText;


    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������
        if (!gameManager.eventFlagData.PressMachineLock)
        {
            gameManager.eventFlagData.PressMachineLock = true;
            gameManager.OpenTextWindow(HackerText);
        }
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
