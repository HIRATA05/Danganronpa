using System.Collections;
using System.Collections.Generic;
using TECHC.Kamiyashiki;
using UnityEngine;

public class InteractChestMachineWorkRoom : MonoBehaviour, IReceiveSearch
{
    //�@�B�H�쎺�̒I

    [SerializeField] private DialogueText NormalText;//�I�̌��m�F
    [SerializeField] private DialogueText KeyUnlockText;//�I�̌������Ō�����
    [SerializeField] private DialogueText AfterText;//�������

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //�ꍇ�ɂ���Ă̓t���O�ɂ���ď�������

        //�I�̌�����
        if (gameManager.eventFlagData.PressMachineShelfUnlock && gameManager.eventFlagData.itemDataBase.truthBullets[8].getFlag == true)
        {
            gameManager.OpenTextWindow(AfterText);
        }
        else if (gameManager.eventFlagData.PressMachineShelfUnlock && gameManager.eventFlagData.PreesMoveDown)
        {
            gameManager.eventFlagData.PressMachineShelfUnlock = true;
            gameManager.OpenTextWindow(KeyUnlockText);
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
}
