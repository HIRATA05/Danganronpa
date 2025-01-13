using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLackyGarden : MonoBehaviour, IReceiveSearch
{
    //中庭の幸運

    [SerializeField] private DialogueText NormalText; //2階開放イベント以外
    [SerializeField] private DialogueText F2OpenEventText; //2階開放イベント中
    [SerializeField] private DialogueText F2OpenAfterText; //2階開放後会話

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //場合によってはフラグによって条件分岐
        //2階への移動前
        if (gameManager.eventFlagData.RopeWindow && !gameManager.eventFlagData.F2Intrusion)
        {
            gameManager.OpenTextWindow(F2OpenAfterText);
        }
        //2階開放前でロープ窓完成前
        else if (gameManager.eventFlagData.F2Request && !gameManager.eventFlagData.F2Open && !gameManager.eventFlagData.RopeWindow)
        {
            gameManager.OpenTextWindow(F2OpenEventText);
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
