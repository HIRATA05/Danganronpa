using System.Collections;
using System.Collections.Generic;
using TECHC.Kamiyashiki;
using UnityEngine;

public class InteractChestMachineWorkRoom : MonoBehaviour, IReceiveSearch
{
    //機械工作室の棚

    [SerializeField] private DialogueText NormalText;//棚の鍵確認
    [SerializeField] private DialogueText KeyUnlockText;//棚の鍵解除で鍵入手
    [SerializeField] private DialogueText AfterText;//鍵入手後

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //場合によってはフラグによって条件分岐

        //棚の鍵解除
        if (gameManager.eventFlagData.PressMachineShelfUnlock)
        {
            gameManager.OpenTextWindow(AfterText);
        }
        else if (!gameManager.eventFlagData.PressMachineShelfUnlock && gameManager.eventFlagData.PreesMoveDown)
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
