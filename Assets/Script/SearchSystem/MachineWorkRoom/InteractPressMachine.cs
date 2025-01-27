using System.Collections;
using System.Collections.Generic;
using TECHC.Kamiyashiki;
using UnityEngine;

public class InteractPressMachine : MonoBehaviour, IReceiveSearch
{
    //機械工作室のプレス機

    [SerializeField] private DialogueText NormalText;
    [SerializeField] private DialogueText HackerText;


    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //場合によってはフラグによって条件分岐
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
