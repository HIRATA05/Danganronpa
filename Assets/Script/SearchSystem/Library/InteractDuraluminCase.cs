using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDuraluminCase : MonoBehaviour, IReceiveSearch
{
    //図書室のケース

    [SerializeField] private DialogueText NormalText; //図書室のケース調査前
    [SerializeField] private DialogueText AfterText; //図書室のケース調査後

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //場合によってはフラグによって条件分岐

        if (!gameManager.eventFlagData.DuraluminCaseInteract && !gameManager.eventFlagData.DuraluminCaseLogic)
        {
            gameManager.eventFlagData.DuraluminCaseInteract = true;
            gameManager.OpenTextWindow(NormalText);
        }
        else if(gameManager.eventFlagData.DuraluminCaseInteract && !gameManager.eventFlagData.DuraluminCaseLogic)
        {
            Debug.Log("数字入力処理を発生させる");
        }
        else
        {
            gameManager.OpenTextWindow(AfterText);
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
